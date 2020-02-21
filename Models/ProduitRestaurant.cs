using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfpEat.Models
{
    public class ProduitRestaurant
    {
        public string Nom { get; set; }
        public List<Produit> Produits { get; set; }
    }
}