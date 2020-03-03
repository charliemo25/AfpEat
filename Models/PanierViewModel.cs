using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfpEat.Models
{
    public class PanierViewModel
    {
        public List<ProduitPanier> ProduitPaniers { get; set; }
        public List<MenuPanier> MenuPaniers { get; set; }

        public PanierViewModel()
        {
            ProduitPaniers = new List<ProduitPanier>();
            MenuPaniers = new List<MenuPanier>();
        }

    }
}