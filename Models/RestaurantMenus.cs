using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfpEat.Models
{
    public class RestaurantMenus
    {
        public Menu Menu { get; set; }
        public List<RestaurantProduits> RestaurantProduits { get; set; }

        public RestaurantMenus()
        {
            RestaurantProduits = new List<RestaurantProduits>();
        }
    }
}