using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfpEat.Models
{
    public class PanierModel : List<ItemPanier>
    {
        public int IdRestaurant { get; set; }
        public int Quantite { get; set; }
        public decimal Montant { get; set; }

        private AfpEatEntities db = new AfpEatEntities();

        public bool AddItem(ItemPanier item, List<int> idProduits)
        {
            int idProduit = item.GetIdProduit();
            int idMenu = item.GetIdMenu();
            bool isAdded = true;

            //Ajout d'un produit ou d'un produit Compose
            if ((item is ProduitPanier || item is ProduitComposePanier) && idProduit > 0)
            {
                //Verifier si le produit existe deja dans le panier
                if (this.Where(p => p.GetIdProduit() == idProduit).Count() > 0)
                {
                    ItemPanier monProduit = this.Where(p => p.GetIdProduit() == idProduit).First();
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

                    this.Add(produitPanier);
                }
            }
            //Ajout d'un menu
            else if (item is MenuPanier && idMenu > 0)
            {

                //Récupere le menu
                Menu menu = db.Menus.Find(idMenu);

                //Récupere les produits sélectionnés
                List<ProduitPanier> produitPaniers = new List<ProduitPanier>();

                foreach (var id in idProduits)
                {
                    Produit monProduit = db.Produits.Find(id);
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
                if (this.Where(m => m.GetIdMenu() == menuPanier.IdMenu).Count() > 0)
                {

                    //Liste de tout les menus existants
                    List<MenuPanier> menuPaniers = new List<MenuPanier>();

                    foreach (MenuPanier menuPanier1 in this)
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
                    if (addMenu && this.Where(m => m.Equals(menuPanier)).Count() == 0)
                    {
                        this.Add(menuPanier);
                    }

                }
                else
                {
                    //Si le menu panier n'existe pas
                    this.Add(menuPanier);
                }
            }
            else
            {
                isAdded = false;
            }

            return isAdded;
        }

        public bool RemoveItem(int ? idProduit, int? idMenu)
        {
            bool isRemoved = false;
            ItemPanier itemPanier = null;

            if(idProduit != null && idProduit > 0)
            {
                itemPanier = this.FirstOrDefault(i => i.GetIdProduit() == idProduit);
                
                isRemoved = true;
            }
            else if(idMenu != null && idMenu > 0)
            {

            }

            if(itemPanier != null)
            {
                this.Remove(itemPanier);
            }

            return isRemoved;
        }

    public void GetQuantite()
    {
        Quantite = 0;
        parcourir();
    }

    public void GetMontant()
    {
        Montant = 0;
        parcourir();
    }

    private void parcourir()
    {
        foreach (ItemPanier itemPanier in this)
        {
            Montant += itemPanier.Quantite * itemPanier.Prix;
            Quantite += itemPanier.Quantite;
            IdRestaurant = itemPanier.IdRestaurant;
        }
    }
}
}