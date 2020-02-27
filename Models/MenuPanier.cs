using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfpEat.Models
{
    public class MenuPanier
    {
        public int IdMenu { get; set; }
        public List<Produit> Produits { get; set; }
        public int IdRestaurant { get; set; }
        public string Nom { get; set; }
        public decimal Prix { get; set; }
        public int Quantite { get; set; }
        public string Photo { get; set; }

        public MenuPanier()
        {
            Produits = new List<Produit>();
        }
    }
}