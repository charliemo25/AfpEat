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
        private PanierViewModel panierViewModel = new PanierViewModel();

        public JsonResult AddMenu(int idMenu, List<int> idProduits, string idSession)
        {
            SessionUtilisateur sessionUtilisateur = db.SessionUtilisateurs.Find(Session.SessionID);
            List<MenuPanier> menuPaniers = (List<MenuPanier>)HttpContext.Application[idSession] ?? new List<MenuPanier>();

            if (sessionUtilisateur == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

            //Récupere le menu
            Menu menu = db.Menus.Find(idMenu);

            //Ajout de menu dans menuPanier
            MenuPanier menuPanier = new MenuPanier()
            {
                IdMenu = menu.IdMenu,
                IdRestaurant = menu.MenuCategories.First().IdRestaurant,
                Nom = menu.Nom,
                Prix = menu.Prix,
                Quantite = 1,
                Photo = menu.Photo.Nom
            };

            //Verifier si le menu existe deja dans le panier
            if (menuPaniers.Where(p => p.IdMenu == idMenu).Count() > 0)
            {
                MenuPanier monMenu = menuPaniers.Where(p => p.IdMenu == idMenu).First();
                monMenu.Quantite++;
                db.SaveChanges();
            }
            else
            {
                menuPaniers.Add(menuPanier);
            }
            panierViewModel.menuPaniers = menuPaniers; 
            //Mise a jour de l'application
            HttpContext.Application[idSession] = panierViewModel;

            return Json(menuPaniers.Count, JsonRequestBehavior.AllowGet);

        }

        public JsonResult RemoveMenu(int idMenu, List<int> idProduits, string idSession)
        {
            SessionUtilisateur sessionUtilisateur = db.SessionUtilisateurs.Find(Session.SessionID);
            List<MenuPanier> menuPaniers = (List<MenuPanier>)HttpContext.Application[idSession] ?? new List<MenuPanier>();

            if (sessionUtilisateur == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

            //Récupere le menu
            Menu menu = db.Menus.Find(idMenu);

            //Ajout de menu dans menuPanier
            MenuPanier menuPanier = new MenuPanier()
            {
                IdMenu = menu.IdMenu,
                IdRestaurant = menu.MenuCategories.First().IdRestaurant,
                Nom = menu.Nom,
                Prix = menu.Prix,
                Quantite = 1,
                Photo = menu.Photo.Nom
            };

            //Verifier si le menu existe dans le panier
            if (menuPaniers.Where(p => p.IdMenu == idMenu).Count() > 0)
            {
                MenuPanier monMenu = menuPaniers.Where(p => p.IdMenu == idMenu).First();
                monMenu.Quantite--;

                if (monMenu.Quantite <= 0)
                {
                    menuPaniers.Remove(monMenu);
                }

                db.SaveChanges();
            }

            panierViewModel.menuPaniers = menuPaniers;
            //Mise a jour de l'application
            HttpContext.Application[idSession] = panierViewModel;

            return Json(menuPaniers.Count, JsonRequestBehavior.AllowGet);

        }

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
                Photo = produit.Photo.Nom
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

            panierViewModel.produitPaniers = produitPaniers;

            //Mise a jour de l'application
            HttpContext.Application[idSession] = panierViewModel;

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
                    produitPaniers.Remove(monProduit);
                }

                db.SaveChanges();
            }

            panierViewModel.produitPaniers = produitPaniers;

            //Mise a jour de l'application
            HttpContext.Application[idSession] = panierViewModel;

            return Json(produitPaniers.Count, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetPanier(string idSession)
        {
            SessionUtilisateur sessionUtilisateur = db.SessionUtilisateurs.Find(Session.SessionID);
            PanierViewModel panier = null;

            if (HttpContext.Application[idSession] != null && sessionUtilisateur != null)
            {
                panier = (PanierViewModel)HttpContext.Application[idSession];
            }
            return Json(panier, JsonRequestBehavior.AllowGet);

        }

        //Modifier la sauvegarde de commande
        public JsonResult SaveCommande(string idSession)
        {
            //Récupere la session de l'utilisateur
            SessionUtilisateur sessionUtilisateur = db.SessionUtilisateurs.Find(Session.SessionID);
            //Récupere le panier
            PanierViewModel panier = (PanierViewModel)HttpContext.Application[idSession];
            
            int idRestaurant = 0;

            Utilisateur utilisateur = db.Utilisateurs.FirstOrDefault(p => p.IdSession == idSession);
            if (utilisateur == null)
            {
                return Json("Vous devez être connecté.", JsonRequestBehavior.AllowGet);
            }

            if (panier.menuPaniers.Count() == 0 && panier.produitPaniers.Count() == 0)
            {
                return Json("Votre panier est vide.", JsonRequestBehavior.AllowGet);
            }

            decimal prixTotal = 0;

            //On calcule le prix total des produits
            foreach (ProduitPanier produitPanier in panier.produitPaniers)
            {
                prixTotal += produitPanier.Prix * produitPanier.Quantite;
                idRestaurant = produitPanier.IdRestaurant;
            }

            if (prixTotal > utilisateur.Solde)
            {
                return Json("Votre solde est insuffisant.", JsonRequestBehavior.AllowGet);
            }

            //On calcule le prix total des menus
            foreach (MenuPanier menuPanier in panier.menuPaniers)
            {
                prixTotal += menuPanier.Prix * menuPanier.Quantite;
                idRestaurant = menuPanier.IdRestaurant;
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

            // Ajout des produits dans commandeProduit
            foreach (ProduitPanier produitPanier in panier.produitPaniers)
            {
                CommandeProduit commandeProduit = new CommandeProduit()
                {
                    //IdCommande = commande.IdCommande,
                    IdProduit = produitPanier.IdProduit,
                    Prix = produitPanier.Prix,
                    Quantite = produitPanier.Quantite,
                };

                //Ajout dans CommandeProduit
                commande.CommandeProduits.Add(commandeProduit);
            }
            //Ajout des menus
            foreach (Menu menu in db.Menus)
            {
                CommandeProduit commandeMenu = new CommandeProduit();

                foreach (MenuPanier menuPanier in panier.menuPaniers)
                {
                    if(menu.IdMenu == menuPanier.IdMenu)
                    {
                        commandeMenu.Menus.Add(menu);
                    }
                }
                //Ajout dans CommandeProduit
                commande.CommandeProduits.Add(commandeMenu);
            }


            //Sauvegarde de la commande dans la bdd
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