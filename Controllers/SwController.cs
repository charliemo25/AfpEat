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

        public JsonResult AddMenu(int idMenu, List<int> idProduits, string idSession)
        {
            //Récupère l'utilisateur à partir de son id de session
            SessionUtilisateur sessionUtilisateur = db.SessionUtilisateurs.Find(Session.SessionID);
            
            //On récupère les produits dans le panier
            PanierModel panier = (PanierModel)HttpContext.Application[idSession] ?? new PanierModel();

            if (sessionUtilisateur == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

            //Récupere le menu
            Menu menu = db.Menus.Find(idMenu);

            //Récupere les produits sélectionnés
            List<ProduitPanier> produitPaniers = new List<ProduitPanier>();

            foreach (var idProduit in idProduits)
            {
                Produit monProduit = db.Produits.Find(idProduit);
                ProduitPanier produitPanier = new ProduitPanier()
                {
                    IdProduit = monProduit.IdProduit,
                    Description = monProduit.Description,
                    Nom = monProduit.Nom,
                    Prix = monProduit.Prix,
                    Photo = monProduit.Photo.Nom,
                    Quantite = monProduit.Quantite
                };
                produitPaniers.Add(produitPanier);
            }

            //Ajout du menu dans menuPanier
            MenuPanier menuPanier = new MenuPanier()
            {
                IdMenu = menu.IdMenu,
                Produits = produitPaniers,
                IdRestaurant = menu.IdRestaurant,
                Nom = menu.Nom,
                Prix = menu.Prix,
                Quantite = 1,
                Photo = menu.Photo.Nom
            };

            //Verifier si le menu existe deja dans le panier
            if (panier.Where(m => m.GetIdMenu() == menuPanier.IdMenu).Count() > 0 )
            {

                //Liste de tout les menus existants
                List<MenuPanier> menuPaniers = new List<MenuPanier>();

                foreach(MenuPanier menuPanier1 in panier)
                {
                    menuPaniers.Add(menuPanier1);
                }

                bool addMenu = false;

                //Parcours des menuPanier
                for (int i = 0; i < menuPaniers.Count(); i++)
                {
                    //Compare les produits dans 2 menuPanier
                    if (menuPaniers[i].Equals(menuPanier))
                    {
                        menuPaniers[i].Quantite++;
                    }
                    else
                    {
                        addMenu = true;
                    }
                }

                //Permet d'ajouter le menu si il n'est pas contenu dans le panier avec les mêmes produits
                if (addMenu && panier.Where(m => m.Equals(menuPanier)).Count() == 0)
                {
                    panier.Add(menuPanier);
                }

            }
            else
            {
                //Si le menu panier n'existe pas
                panier.Add(menuPanier);
            }

            //Mise a jour de l'application
            HttpContext.Application[idSession] = panier;

            return Json(panier.Count, JsonRequestBehavior.AllowGet);

        }

        //public JsonResult RemoveMenu(int idMenu, List<int> idProduits, string idSession)
        //{
        //    SessionUtilisateur sessionUtilisateur = db.SessionUtilisateurs.Find(Session.SessionID);

        //    //On récupère les produits dans le panier
        //    PanierModel panier = (PanierModel)HttpContext.Application[idSession] ?? new PanierModel();

        //    if (sessionUtilisateur == null)
        //    {
        //        return Json(0, JsonRequestBehavior.AllowGet);
        //    }

        //    //Récupere le menu
        //    Menu menu = db.Menus.Find(idMenu);

        //    //Récupere les produits sélectionnés
        //    List<Produit> produits = new List<Produit>();

        //    foreach (var idProduit in idProduits)
        //    {
        //        produits.Add(db.Produits.Find(idProduit));
        //    }

        //    //Ajout de menu dans menuPanier
        //    MenuPanier menuPanier = new MenuPanier()
        //    {
        //        IdMenu = menu.IdMenu,
        //        Produits = produits,
        //        IdRestaurant = menu.IdRestaurant,
        //        Nom = menu.Nom,
        //        Prix = menu.Prix,
        //        Quantite = 1,
        //        Photo = menu.Photo.Nom
        //    };

        //    //Verifier si le menu existe dans le panier
        //    if (menuPaniers.Where(p => p.IdMenu == idMenu).Count() > 0)
        //    {
        //        MenuPanier monMenu = menuPaniers.Where(p => p.IdMenu == idMenu).First();
        //        monMenu.Quantite--;

        //        if (monMenu.Quantite <= 0)
        //        {
        //            menuPaniers.Remove(monMenu);
        //        }

        //    }

        //    panierViewModel.menuPaniers = menuPaniers.ToList();
        //    //Mise a jour de l'application
        //    HttpContext.Application[idSession] = panierViewModel;

        //    return Json(menuPaniers.Count, JsonRequestBehavior.AllowGet);

        //}

        public JsonResult AddProduit(int idProduit, string idSession)
        {
            SessionUtilisateur sessionUtilisateur = db.SessionUtilisateurs.Find(Session.SessionID);

            //On récupère les produits dans le panier
            PanierModel panier = (PanierModel)HttpContext.Application[idSession] ?? new PanierModel();

            if (sessionUtilisateur == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

            //Verifier si le produit existe deja dans le panier
            if (panier.Where(p => p.GetIdProduit() == idProduit).Count() > 0)
            {
                ItemPanier monProduit = panier.Where(p => p.GetIdProduit() == idProduit).First();
                monProduit.Quantite++;
            }
            else
            {
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

                panier.Add(produitPanier);
            }

            panier.GetQuantite();
            panier.GetMontant();
            
            //Mise a jour de l'application
            HttpContext.Application[idSession] = panier;

            return Json(new { statut = 1, message ="Le produit a bien été ajouté." ,idProduit = idProduit}, JsonRequestBehavior.AllowGet);

        }

        public JsonResult RemoveProduit(int idProduit, string idSession)
        {
            SessionUtilisateur sessionUtilisateur = db.SessionUtilisateurs.Find(Session.SessionID);

            //On récupère les produits dans le panier
            PanierModel panier = (PanierModel)HttpContext.Application[idSession] ?? new PanierModel();

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
            if (panier.Where(p => p.GetIdProduit() == idProduit).Count() > 0)
            {
                ItemPanier monProduit = panier.Where(p => p.GetIdProduit() == idProduit).First();
                monProduit.Quantite--;

                if (monProduit.Quantite <= 0)
                {
                    panier.Remove(monProduit);
                }

            }

            panier.GetQuantite();
            panier.GetMontant();

            //Mise a jour de l'application
            HttpContext.Application[idSession] = panier;

            return Json(panier.Count, JsonRequestBehavior.AllowGet);

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

        public JsonResult SaveCommande(string idSession)
        {

            //Récupere la session de l'utilisateur
            SessionUtilisateur sessionUtilisateur = db.SessionUtilisateurs.Find(Session.SessionID);
            //Récupere le panier
            PanierViewModel panier = (PanierViewModel)HttpContext.Application[idSession];

            decimal prixTotal = 0;

            Utilisateur utilisateur = db.Utilisateurs.FirstOrDefault(p => p.IdSession == idSession);

            if (utilisateur == null)
            {
                return Json(new { statut = 0, message = "Vous devez être connecté pour passer une commande" }, JsonRequestBehavior.AllowGet);
            }

            if (panier.MenuPaniers.Count() == 0 && panier.ProduitPaniers.Count() == 0)
            {
                return Json(new { statut = 0, message = "Votre panier est vide." }, JsonRequestBehavior.AllowGet);
            }

            //idRestaurant si le menu ou le produit panier contient une entrée
            int idRestaurant = panier.MenuPaniers.Count() > 0 ? panier.MenuPaniers.First().IdRestaurant : panier.ProduitPaniers.First().IdRestaurant;

            //Création de la commande
            Commande commande = new Commande()
            {
                IdUtilisateur = utilisateur.IdUtilisateur,
                IdRestaurant = idRestaurant,
                Date = DateTime.Now,
                IdEtatCommande = 1,
            };

            if (panier.ProduitPaniers != null && panier.ProduitPaniers.Count() > 0)
            {
                //On calcule le prix total des produits
                foreach (ItemPanier produitPanier in panier.ProduitPaniers)
                {
                    prixTotal += produitPanier.Prix * produitPanier.Quantite;
                }

                // Ajout des produits dans commandeProduit
                foreach (ItemPanier produitPanier in panier.ProduitPaniers)
                {
                    CommandeProduit commandeProduit = new CommandeProduit()
                    {
                        //IdCommande = commande.IdCommande,
                        IdProduit = produitPanier.GetIdProduit(),
                        Prix = produitPanier.Prix,
                        Quantite = produitPanier.Quantite,

                    };

                    //Ajout dans CommandeProduit
                    commande.CommandeProduits.Add(commandeProduit);
                }

            }

            //Ajout des produits lié a un menu
            if (panier.MenuPaniers != null && panier.MenuPaniers.Count() > 0)
            {
                //On calcule le prix total des menus
                foreach (MenuPanier menuPanier in panier.MenuPaniers)
                {
                    prixTotal += menuPanier.Prix * menuPanier.Quantite;

                    foreach (var produit in menuPanier.Produits)
                    {
                        CommandeProduit commandeMenuProduit = new CommandeProduit()
                        {
                            IdProduit = produit.IdProduit,
                            
                        };
                        //Ajout du menu correspondant au produit
                        commandeMenuProduit.Menus.Add(db.Menus.Find(menuPanier.IdMenu));
                        //Ajout dans la commande
                        commande.CommandeProduits.Add(commandeMenuProduit);
                    }
                }
            }

            if (prixTotal > utilisateur.Solde)
            {
                return Json(new { statut = 0, message = "Votre solde est insuffisant." }, JsonRequestBehavior.AllowGet);
            }

            commande.Prix = prixTotal;

            //Sauvegarde de la commande dans la bdd
            db.Commandes.Add(commande);

            //Changer le solde de l'utilisateur
            utilisateur.Solde -= prixTotal;

            db.SaveChanges();
            return Json(new { statut = 1, message = "Votre commande a été effectuer." }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHistoriqueUtilisateur(string idSession)
        {
            SessionUtilisateur sessionUtilisateur = db.SessionUtilisateurs.Find(Session.SessionID);


            if (sessionUtilisateur == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

            PanierViewModel historiqueUtilisateur = null;
            Utilisateur utilisateur = db.Utilisateurs.FirstOrDefault(p => p.IdSession == idSession);


            List<Commande> commande = db.Commandes.Where(c => c.IdUtilisateur == utilisateur.IdUtilisateur).ToList();

            return Json("", JsonRequestBehavior.AllowGet);
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