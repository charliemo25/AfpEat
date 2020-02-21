using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfpEat.Models
{
    public class RestaurantCategorie
    {
        public Restaurant Restaurant { get; set; }
        public List<MenuRestaurant> MenusRestaurant{ get; set; }
        public List<MenuCategorie> ProduitsCategorie { get; set; }
    }
}