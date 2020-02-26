using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfpEat.Models
{
    public class PanierViewModel
    {

        public List<ProduitPanier> produitPaniers { get; set; }
        public List<MenuPanier> menuPaniers { get; set; }

        public PanierViewModel()
        {
            menuPaniers = new List<MenuPanier>();
        }

    }
}