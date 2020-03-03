using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfpEat.Models
{
    public class PanierModel : List<ItemPanier>
    {

        public int Quantite { get; set; }
        public decimal Montant { get; set; }

        public void GetQuantite()
        {
            int Quantite = 0;
            parcourir();
        }

        public void GetMontant()
        {
            decimal Montant = 0;
            parcourir();
        }

        private void parcourir()
        {
            foreach(ItemPanier itemPanier in this)
            {
                Montant += itemPanier.Quantite * itemPanier.Prix;
                Quantite += itemPanier.Quantite;
            }
        }
    }
}