﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using AfpEat.Models;

namespace AfpEat.Controllers
{
    public class UtilisateursController : Controller
    {
        private AfpEatEntities db = new AfpEatEntities();

        // GET: Utilisateurs/Connexion
        public ActionResult Connexion()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Connexion([Bind(Include = "Matricule,Password")] Utilisateur user)
        {
            if (ModelState.IsValid)
            {
                Utilisateur utilisateur = db.Utilisateurs.FirstOrDefault(p => p.Matricule == user.Matricule);

                bool passwordValid = Crypto.VerifyHashedPassword(utilisateur.Password, user.Password);

                if (utilisateur != null && passwordValid)
                {
                    utilisateur.IdSession = Session.SessionID;
                    db.SaveChanges();

                    FormsAuthentication.SetAuthCookie(utilisateur.Matricule, false);

                    HttpContext.Session.Add("Utilisateur", utilisateur);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Erreur = "Erreur d'identification";
                }
            }

            return View();
        }

        public ActionResult Deconnexion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Connexion");
        }

        [Authorize(Roles = "Utilisateur")]
        public ActionResult Historique()
        {
            var identity = User.Identity as CustomIdentity;
            Utilisateur utilisateur = identity.Utilisateur;
            
            if (!User.Identity.IsAuthenticated)
            {
                return HttpNotFound();
            }

            //Liste de toutes les commandes d'un utilisateur
            List<Commande> commandes = db.Commandes.Where(c => c.IdUtilisateur == utilisateur.IdUtilisateur).ToList();

            //Liste de tout les produits dans la commande
            List<CommandeProduit> commandeProduits = new List<CommandeProduit>();

            //Liste de tout les produits dans la commande avec un menu rattaché
            List<CommandeProduit> commandeMenus = new List<CommandeProduit>();

            foreach(var commande in commandes)
            {
                foreach(var commandeProduit in commande.CommandeProduits)
                {
                    if (commandeProduit.Menus.Count() == 1)
                    {
                        commandeMenus.Add(commandeProduit);
                    }
                    else
                    {
                        commandeProduits.Add(commandeProduit);
                    }
                }
            }

            List<MenuPanier> menuPaniers = new List<MenuPanier>();
            
            List<Menu> menus = new List<Menu>();

            //Récupère les 
            foreach (var cProduitMenu in commandeMenus)
            {
                if (!menus.Contains(cProduitMenu.Menus.First()))
                {
                    //MenuPanier menuPanier = new MenuPanier()
                    //{
                    //    IdMenu = cProduitMenu.Menus.First().IdMenu,
                    //};
                    //menuPaniers.Add(menuPanier);
                    menus.Add(cProduitMenu.Menus.First());
                }
            }

            return View(commandes);
        }

        [Authorize(Roles = "Utilisateur")]
        public ActionResult Favoris()
        {

            var identity = User.Identity as CustomIdentity;
            Utilisateur utilisateur = identity.Utilisateur;

            if (utilisateur == null)
            {
                return HttpNotFound();
            }

            return View(db.Restaurants.ToList());
        }

        // GET: Utilisateurs
        public ActionResult Index()
        {
            return View(db.Utilisateurs.ToList());
        }

        public ActionResult ValidationCommande(int? id)
        {
            Utilisateur utilisateur = db.Utilisateurs.Find(id);
            Session["utilisateur"] = utilisateur;

            return RedirectToAction("Index");
        }

        public ActionResult RecapitulatifCommande(int? id)
        {
            return View();
        }

        // GET: Utilisateurs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Utilisateurs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUtilisateur,Nom,Prenom,Matricule,Password")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                utilisateur.Password = Crypto.HashPassword(utilisateur.Password);
                utilisateur.Statut = true;
                
                db.Utilisateurs.Add(utilisateur);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(utilisateur);
        }

        // GET: Utilisateurs/Edit/5
        [Authorize(Roles = "Utilisateur")]
        public ActionResult Profil()
        {
            var user = User.Identity as AfpEat.Models.CustomIdentity;

            Utilisateur utilisateur = user.Utilisateur;
            
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profil(FormCollection formCollection)
        {

            if (formCollection == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Utilisateur utilisateur = (Utilisateur)Session["utilisateur"];
            
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            
            //Récupère et compare les mdp
            string password = formCollection["Password1"] == formCollection["Password2"] ? Crypto.HashPassword(formCollection["Password1"]) : null;

            if(password == null)
            {
                //Ajouter un message d'erreur
                return View(utilisateur);
            }

            db.Utilisateurs.Find(utilisateur.IdUtilisateur).Password = password;
            
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
