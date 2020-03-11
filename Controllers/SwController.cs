using AfpEat.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfpEat.Controllers
{
    public class SwController : Controller
    {
        private AfpEatEntities db = new AfpEatEntities();

        public JsonResult AddItemPanier(int idProduit, int idMenu, List<int> idProduits, string idSession)
        {
            //Récupère l'utilisateur à partir de son id de session
            SessionUtilisateur sessionUtilisateur = db.SessionUtilisateurs.Find(Session.SessionID);

            //On récupère les produits dans le panier
            PanierModel panier = (PanierModel)HttpContext.Application[idSession] ?? new PanierModel();

            if (sessionUtilisateur == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

            if (idProduit > 0)
            {

            }

            if (idMenu > 0)
            {

            }

            //Mise a jour de l'application
            HttpContext.Application[idSession] = panier;

            return Json(panier.Count, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddMenu(int idRestaurant ,int idMenu, List<int> idProduits, string idSession)
        {
            //Vérifie si l'utilisateur à bien selectionner des produits
            if (idProduits.Contains(0))
            {
                return Json(new { statut=0, message="Veuillez selectionner tout les produits."}, JsonRequestBehavior.AllowGet);
            }

            //Récupère l'utilisateur à partir de son id de session
            SessionUtilisateur sessionUtilisateur = db.SessionUtilisateurs.Find(Session.SessionID);

            //On récupère les produits dans le panier
            PanierModel panier = (PanierModel)HttpContext.Application[idSession] ?? new PanierModel();
            panier.IdRestaurant = idRestaurant;

            if (sessionUtilisateur == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

            //Récupere le menu
            Menu menu = db.Menus.Find(idMenu);

            //Récupere les produits sélectionnés dans ce menu
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

            //Creation d'un menuPanier pour comparer avec ceux contenu dans le panier
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

            //Initialise un ItemPanier qui contient tout les menuPaniers
            List<ItemPanier> itemPaniers = panier.Where(p => p is MenuPanier).Count() > 0 ? panier.Where(p => p is MenuPanier).ToList() : null;
            
            //si itemPaniers est nul et si elle contient plus d'un MenuPanier egal à celui qu'on rajoute 
            if (itemPaniers != null && itemPaniers.Where(i => i is MenuPanier iM && iM.Equals(menuPanier)).Count() > 0)
            {
                //Parcours des menuPaniers
                for (int i = 0; i < itemPaniers.Count(); i++)
                {
                    //Compare les produits dans 2 menuPanier
                    if (itemPaniers[i] is MenuPanier menuPanier1 &&  menuPanier1.Equals(menuPanier))
                    {
                        menuPanier1.Quantite++;
                    }
                    else
                    {
                        continue;
                    }
                }

                //Permet d'ajouter le menuPanier si il n'est pas contenu dans le panier avec les mêmes produits
                if (itemPaniers.Where(m => m is MenuPanier menu1 && menu1.Equals(menuPanier)).Count() == 0)
                {
                    panier.Add(menuPanier);
                }

            }
            else 
            {
                //Si le menu panier n'existe pas
                panier.Add(menuPanier);
            }

            panier.SetPanier();

            //Mise a jour de l'application
            HttpContext.Application[idSession] = panier;


            string montantTotal = String.Format(CultureInfo.GetCultureInfo("fr-FR"), "{0:C}", panier.Montant);
            int quantiteTotal = panier.Quantite;

            return Json(new { statut=1, message="Le menu a bien été ajouté.", montantTotal , quantiteTotal}, JsonRequestBehavior.AllowGet);

        }

        public JsonResult RemoveMenu(int idMenu, List<int> idProduits, string idSession)
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

            //Récupere les produits sélectionnés dans ce menu
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

            //Creation d'un menuPanier pour comparer avec ceux contenu dans le panier
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

            //Initialise un ItemPanier qui contient tout les menuPaniers
            List<ItemPanier> itemPaniers = panier.Where(p => p is MenuPanier).Count() > 0 ? panier.Where(p => p is MenuPanier).ToList() : null;

            //si itemPaniers est nul et si elle contient plus d'un MenuPanier egal à celui qu'on supprime 
            if (itemPaniers != null && itemPaniers.Where(i => i is MenuPanier iM && iM.Equals(menuPanier)).Count() > 0)
            {
                //Parcours des menuPaniers
                for (int i = 0; i < itemPaniers.Count(); i++)
                {
                    //Compare les produits dans 2 menuPanier
                    if (itemPaniers[i] is MenuPanier menuPanier1 && menuPanier1.Equals(menuPanier))
                    {
                        menuPanier1.Quantite--;
                        if(menuPanier1.Quantite <= 0)
                        {
                            panier.Remove(menuPanier1);
                        }
                    }
                }
            }
            panier.SetPanier();

            //Mise a jour de l'application
            HttpContext.Application[idSession] = panier;


            string montantTotal = String.Format(CultureInfo.GetCultureInfo("fr-FR"), "{0:C}", panier.Montant);
            int quantiteTotal = panier.Quantite;

            return Json(new { statut=1, message="Le menu à bien été supprimé.", montantTotal, quantiteTotal }, JsonRequestBehavior.AllowGet);


        }

        public JsonResult AddProduit(int idProduit, string idSession)
        {
            SessionUtilisateur sessionUtilisateur = db.SessionUtilisateurs.Find(Session.SessionID);

            //On récupère les produits dans le panier
            PanierModel panier = (PanierModel)HttpContext.Application[idSession] ?? new PanierModel();

            if (sessionUtilisateur == null)
            {
                return Json(new { statut = 0, message = "Erreur." }, JsonRequestBehavior.AllowGet);
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

            panier.SetPanier();

            //Mise a jour de l'application
            HttpContext.Application[idSession] = panier;


            string montantTotal = String.Format(CultureInfo.GetCultureInfo("fr-FR"), "{0:C}", panier.Montant);
            int quantiteTotal = panier.Quantite;

            return Json(new { statut = 1, message = "Le produit a bien été ajouté.", montantTotal, quantiteTotal }, JsonRequestBehavior.AllowGet);

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
                Photo = produit.Photo.Nom
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

            panier.SetPanier();

            //Mise a jour de l'application
            HttpContext.Application[idSession] = panier;

            string montantTotal = String.Format(CultureInfo.GetCultureInfo("fr-FR"), "{0:C}", panier.Montant);
            int quantiteTotal = panier.Quantite;

            return Json(new { statut = 1, message = "Le produit a bien été supprimé.", montantTotal, quantiteTotal }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SaveCommande(string idSession)
        {

            //Récupere la session de l'utilisateur
            SessionUtilisateur sessionUtilisateur = db.SessionUtilisateurs.Find(Session.SessionID);
            //Récupere le panier
            PanierModel panier = (PanierModel)HttpContext.Application[idSession];

            Utilisateur utilisateur = db.Utilisateurs.FirstOrDefault(p => p.IdSession == idSession);

            if (utilisateur == null)
            {
                return Json(new { statut = 0, message = "Vous devez être connecté pour passer une commande" }, JsonRequestBehavior.AllowGet);
            }

            if (panier != null && panier.Count() == 0)
            {
                return Json(new { statut = 0, message = "Votre panier est vide." }, JsonRequestBehavior.AllowGet);
            }

            //idRestaurant si le menu ou le produit panier contient une entrée
            int idRestaurant = panier.IdRestaurant;

            //Création de la commande
            Commande commande = new Commande()
            {
                IdUtilisateur = utilisateur.IdUtilisateur,
                IdRestaurant = idRestaurant,
                Date = DateTime.Now,
                IdEtatCommande = 1,
            };

            // Ajout des produits dans commandeProduit
            foreach (ItemPanier itemPanier in panier)
            {
                if (itemPanier is ProduitPanier produit)
                {
                    CommandeProduit commandeProduit = new CommandeProduit();

                    //Propriétés d'un ProduitPanier
                    commandeProduit.IdProduit = produit.IdProduit;
                    commandeProduit.Prix = produit.Prix;
                    commandeProduit.Quantite = produit.Quantite;

                    commande.CommandeProduits.Add(commandeProduit);
                }
                else if (itemPanier is MenuPanier menu)
                {
                    //ajouter les produits avec le menu correspondant
                    foreach (ProduitPanier produitPanier in menu.Produits)
                    {
                        CommandeProduit commandeProduit = new CommandeProduit();

                        //Ajout du produit contenu dans le menu
                        commandeProduit.IdProduit = produitPanier.IdProduit;
                        commandeProduit.Prix = menu.Prix;
                        commandeProduit.Quantite = menu.Quantite;

                        //Ajout du menu correspondant au produit
                        commandeProduit.Menus.Add(db.Menus.Find(menu.IdMenu));

                        //Ajout dans la commande
                        commande.CommandeProduits.Add(commandeProduit);
                    }
                }
            }

            //Calcul la quantité et le prixTotal du panier
            panier.SetPanier();

            if (panier.Montant > utilisateur.Solde)
            {
                return Json(new { statut = 0, message = "Votre solde est insuffisant." }, JsonRequestBehavior.AllowGet);
            }

            commande.Prix = panier.Montant;

            //Sauvegarde de la commande dans la bdd
            db.Commandes.Add(commande);

            //Changer le solde de l'utilisateur
            utilisateur.Solde -= commande.Prix;
            Session["utilisateur"] = utilisateur;

            //changer les quantite de produits
            foreach(CommandeProduit item in commande.CommandeProduits.ToList())
            {
                db.Produits.Find(item.IdProduit).Quantite -= item.Quantite;
            }
            
            db.SaveChanges();
            return Json(new { statut = 1, message = "Votre commande a été effectuer." }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHistoriqueUtilisateur(string idSession)
        {
            Utilisateur utilisateur = (Utilisateur)Session["utilisateur"];
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