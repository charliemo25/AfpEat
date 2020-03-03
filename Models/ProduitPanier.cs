using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfpEat.Models
{
    public class ProduitPanier: ItemPanier ,IEquatable<ProduitPanier>
    {
        public int IdProduit { get; set; }

        public override int GetIdProduit()
        {
            return IdProduit;
        }

        public bool Equals(ProduitPanier other)
        {
            return this == other;
        }
    }
}