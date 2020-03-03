using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfpEat.Models
{
    public class PanierViewModel
    {
        public List<ItemPanier> produitPaniers { get; set; }
        public List<MenuPanier> menuPaniers { get; set; }

        public PanierViewModel()
        {
            menuPaniers = new List<MenuPanier>();
            produitPaniers = new List<ItemPanier>();
        }

    }
}