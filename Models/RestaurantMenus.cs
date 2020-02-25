using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfpEat.Models
{
    public class RestaurantMenus
    {
        public string NomMenu { get; set; }
        public List<RestaurantProduits> restaurantProduits { get; set; }

        public RestaurantMenus()
        {
            restaurantProduits = new List<RestaurantProduits>();
        }
    }
}