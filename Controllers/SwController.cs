﻿using AfpEat.Models;
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
            List<ProduitPanier> produitPaniers = null;

            if (sessionUtilisateur != null)
            {

                if (HttpContext.Application[idSession] != null)
                {
                    produitPaniers = (List<ProduitPanier>)HttpContext.Application[idSession];
                }
                else
                {
                    produitPaniers = new List<ProduitPanier>();
                }

                Produit produit = db.Produits.Find(idProduit);

                ProduitPanier produitPanier = new ProduitPanier()
                {
                    IdProduit = produit.IdProduit,
                    Nom = produit.Nom,
                    Description = produit.Description,
                    Prix = produit.Prix,
                    Quantite = 1,
                    Photo = "Boulangerie.jpg"
                };

                produitPaniers.Add(produitPanier);
                HttpContext.Application[idSession] = produitPaniers;

            }
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

        public JsonResult SaveCommande(string idSession, int idRestaurant)
        {
            SessionUtilisateur sessionUtilisateur = db.SessionUtilisateurs.Find(Session.SessionID);
            List<ProduitPanier> panier = null;

            if (HttpContext.Application[idSession] != null && sessionUtilisateur != null)
            {
                panier = (List<ProduitPanier>)HttpContext.Application[idSession];
            }

            Utilisateur utilisateur = db.Utilisateurs.First(p => p.IdSession == idSession);

            if(utilisateur != null && utilisateur.Solde > 0 && panier != null && panier.Count() > 0)
            {
                decimal prixTotal = 0;
                foreach (ProduitPanier produitPanier in panier)
                {
                    prixTotal += produitPanier.Prix * produitPanier.Quantite;
                }
            }

            return Json(panier, JsonRequestBehavior.AllowGet);
        }

    }
}