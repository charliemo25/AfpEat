using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfpEat.Models
{
    public class MenuRestaurant
    {
        public string Nom { get; set; }
        public List<Menu> menus { get; set; }
    }
}