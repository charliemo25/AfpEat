using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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

                Utilisateur utilisateur = db.Utilisateurs.FirstOrDefault(p => p.Matricule == user.Matricule && p.Password == user.Password);

                if (utilisateur != null)
                {
                    utilisateur.IdSession = Session.SessionID;
                    db.SaveChanges();

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

        public ActionResult Historique()
        {
            //Passez l'hitorique de toutes les commandes d'un utilisateur
            PanierViewModel panier = (PanierViewModel)HttpContext.Application[Session.SessionID];

            return View(panier);
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

        // GET: Utilisateurs/Details/5
        public ActionResult Details(int? id)
        {
            var user = (Utilisateur)Session["Utilisateur"];

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = db.Utilisateurs.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
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
                utilisateur.Statut = true;
                
                db.Utilisateurs.Add(utilisateur);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(utilisateur);
        }

        // GET: Utilisateurs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = db.Utilisateurs.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // POST: Utilisateurs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUtilisateur,Nom,Prenom,Matricule,Password,Statut,Solde")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utilisateur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(utilisateur);
        }

        // GET: Utilisateurs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utilisateur utilisateur = db.Utilisateurs.Find(id);
            if (utilisateur == null)
            {
                return HttpNotFound();
            }
            return View(utilisateur);
        }

        // POST: Utilisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utilisateur utilisateur = db.Utilisateurs.Find(id);
            db.Utilisateurs.Remove(utilisateur);
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
