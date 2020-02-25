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
        public List<Menu> Menus { get; set; }
        public RestaurantsDetailsModel()
        {
            RestaurantProduits = new List<RestaurantProduits>();
            Menus = new List<Menu>();
        }
    }
}