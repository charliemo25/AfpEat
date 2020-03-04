using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfpEat.Models
{
    public class MenuPanier: ItemPanier, IEquatable<MenuPanier>
    {
        public int IdMenu { get; set; }
        public List<ProduitPanier> Produits { get; set; }

        public MenuPanier()
        {
            Produits = new List<ProduitPanier>();
        }

        public override int GetIdMenu()
        {
            return IdMenu;
        }

        public override List<ProduitPanier> GetProduitPaniers(List<int> idProduits)
        {
            List<ProduitPanier> produitPaniers = new List<ProduitPanier>();
            foreach(int id in idProduits)
            {
                produitPaniers.Add(Produits.Where(p => p.IdProduit == id).FirstOrDefault());
            }

            return produitPaniers;
        }

        public bool Equals(MenuPanier other)
        {
            //Pouvoir comparer des listes de produits
            return this.Produits.SequenceEqual(other.Produits);

        }
    }
}