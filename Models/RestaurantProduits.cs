using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfpEat.Models
{
    public class RestaurantProduits
    {
        public int idCategorie { get; set; }
        public string NomCategorie { get; set; }
        public List<Produit> Produits { get; set; }

        public RestaurantProduits()
        {
            Produits = new List<Produit>();
        }
    }
}