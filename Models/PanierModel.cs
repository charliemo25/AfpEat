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

        public void AddItemPanier(ItemPanier itemPanier)
        {

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
            foreach(ItemPanier itemPanier in this)
            {
                Montant += itemPanier.Quantite * itemPanier.Prix;
                Quantite += itemPanier.Quantite;
                IdRestaurant = itemPanier.IdRestaurant;
            }
        }
    }
}