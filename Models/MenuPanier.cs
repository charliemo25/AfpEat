using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfpEat.Models
{
    public class MenuPanier: ItemPanier, IEquatable<MenuPanier>
    {
        public int IdMenu { get; set; }
        public List<Produit> Produits { get; set; }

        public MenuPanier()
        {
            Produits = new List<Produit>();
        }

        public bool Equals(MenuPanier other)
        {

            return this.Produits.SequenceEqual(other.Produits);
        }
    }
}