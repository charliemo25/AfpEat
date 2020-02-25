using AfpEat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfpEat.Controllers
{
    public class SwController : Controller
    {
        private AfpEatEntities db = new AfpEatEntities();


        public JsonResult AddProduit(int idProduit, string idSession)
        {
            SessionUtilisateur sessionUtilisateur = db.SessionUtilisateurs.Find(Session.SessionID);
            List<ProduitPanier> produitPaniers = (List<ProduitPanier>)HttpContext.Application[idSession]?? new List<ProduitPanier>();

            if (sessionUtilisateur == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            
            //Récupere le produit
            Produit produit = db.Produits.Find(idProduit);

            //Ajout de produit dans produitPanier
            ProduitPanier produitPanier = new ProduitPanier()
            {
                IdProduit = produit.IdProduit,
                IdRestaurant = produit.ProduitCategories.First().IdRestaurant,
                Nom = produit.Nom,
                Description = produit.Description,
                Prix = produit.Prix,
                Quantite = 1,
                Photo = "Boulangerie.jpg"
            };

            //Verifier si le produit existe deja dans le panier
            if (produitPaniers.Where(p => p.IdProduit == idProduit).Count() > 0)
            {
                ProduitPanier monProduit = produitPaniers.Where(p => p.IdProduit == idProduit).First();
                monProduit.Quantite++;
                db.SaveChanges();
            }
            else
            {
                produitPaniers.Add(produitPanier);
            }
            
            //Mise a jour de l'application
            HttpContext.Application[idSession] = produitPaniers;

            return Json(produitPaniers.Count, JsonRequestBehavior.AllowGet);

        }

        public JsonResult RemoveProduit(int idProduit, string idSession)
        {
            SessionUtilisateur sessionUtilisateur = db.SessionUtilisateurs.Find(Session.SessionID);
            List<ProduitPanier> produitPaniers = (List<ProduitPanier>)HttpContext.Application[idSession] ?? new List<ProduitPanier>();

            if (sessionUtilisateur == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

            //Récupere le produit
            Produit produit = db.Produits.Find(idProduit);

            //Ajout de produit dans produitPanier
            ProduitPanier produitPanier = new ProduitPanier()
            {
                IdProduit = produit.IdProduit,
                IdRestaurant = produit.ProduitCategories.First().IdRestaurant,
                Nom = produit.Nom,
                Description = produit.Description,
                Prix = produit.Prix,
                Quantite = 1,
                Photo = "Boulangerie.jpg"
            };

            //Verifier si le produit existe deja dans le panier
            if (produitPaniers.Where(p => p.IdProduit == idProduit).Count() > 0)
            {
                ProduitPanier monProduit = produitPaniers.Where(p => p.IdProduit == idProduit).First();
                monProduit.Quantite--;

                if(monProduit.Quantite <= 0)
                {
                    produitPaniers.Remove(produitPanier);
                }

                db.SaveChanges();
            }

            //Mise a jour de l'application
            HttpContext.Application[idSession] = produitPaniers;

            return Json(produitPaniers.Count, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetProduits(string idSession)
        {
            SessionUtilisateur sessionUtilisateur = db.SessionUtilisateurs.Find(Session.SessionID);
            List<ProduitPanier> panier = null;

            if (HttpContext.Application[idSession] != null && sessionUtilisateur != null)
            {
                panier = (List<ProduitPanier>)HttpContext.Application[idSession];
            }
            return Json(panier, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SaveCommande(string idSession)
        {
            SessionUtilisateur sessionUtilisateur = db.SessionUtilisateurs.Find(Session.SessionID);
            List<ProduitPanier> panier = (List<ProduitPanier>)HttpContext.Application[idSession];
            int idRestaurant = 0;

            Utilisateur utilisateur = db.Utilisateurs.FirstOrDefault(p => p.IdSession == idSession);
            if (utilisateur == null)
            {
                return Json("Vous devez être connecté.", JsonRequestBehavior.AllowGet);
            }

            if (panier.Count() == 0)
            {
                return Json("Votre panier est vide.", JsonRequestBehavior.AllowGet);
            }

            //On calcule le prix total des produits
            decimal prixTotal = 0;
            foreach (ProduitPanier produitPanier in panier)
            {
                prixTotal += produitPanier.Prix * produitPanier.Quantite;
                idRestaurant = produitPanier.IdRestaurant;
            }

            if (prixTotal > utilisateur.Solde)
            {
                return Json("Votre solde est insuffisant.", JsonRequestBehavior.AllowGet);
            }

            //Création de la commande
            Commande commande = new Commande()
            {
                IdUtilisateur = utilisateur.IdUtilisateur,
                IdRestaurant = idRestaurant,
                Date = DateTime.Now,
                Prix = prixTotal,
                IdEtatCommande = 1,
            };

            //Ajout de la commande
            db.Commandes.Add(commande);
            //db.SaveChanges();

            // Ajout des produits dans commandeProduit
            foreach (ProduitPanier produitPanier in panier)
            {
                CommandeProduit commandeProduit = new CommandeProduit()
                {
                    //IdCommande = commande.IdCommande,
                    IdProduit = produitPanier.IdProduit,
                    Prix = produitPanier.Prix,
                    Quantite = produitPanier.Quantite
                };

                //Ajout dans CommandeProduit
                commande.CommandeProduits.Add(commandeProduit);
            }

            //Sauvegarde la de commande dans la bdd
            db.Commandes.Add(commande);

            //Changer le solde de l'utilisateur
            utilisateur.Solde -= prixTotal;

            db.SaveChanges();

            return Json(new { idUtilisateur = utilisateur.IdUtilisateur, message = "Votre commande a été effectuer." }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoginUtilisateur(string idSession, string matricule, string password)
        {
            Utilisateur utilisateur = db.Utilisateurs.FirstOrDefault(u => u.Matricule == matricule && u.Password == password);


            if (utilisateur != null)
            {
                utilisateur.IdSession = idSession;
                db.SaveChanges();

                return Json(new { error = 0, message = utilisateur.Solde }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { error = 1, message = "La connexion a echoué." }, JsonRequestBehavior.AllowGet);

        }

    }
}