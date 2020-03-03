using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfpEat.Models
{
    public class ItemPanier
    {
        public int IdRestaurant { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public decimal Prix { get; set; }
        public int Quantite { get; set; }
    }
}