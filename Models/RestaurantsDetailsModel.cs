using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfpEat.Models
{
    public class RestaurantsDetailsModel
    {
        public Restaurant Restaurant { get; set; }
        public List<RestaurantProduits> RestaurantProduits { get; set; }
        public List<RestaurantMenus> restaurantMenus { get; set; }
        public RestaurantsDetailsModel()
        {
            RestaurantProduits = new List<RestaurantProduits>();
            restaurantMenus= new List<RestaurantMenus>();
        }
    }
}