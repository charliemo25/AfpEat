using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfpEat.Models
{
    public class RestaurantMenus
    {
        public string NomCategorie { get; set; }
        public List<Menu> Menus { get; set; }

        public RestaurantMenus()
        {
            Menus = new List<Menu>();
        }
    }
}