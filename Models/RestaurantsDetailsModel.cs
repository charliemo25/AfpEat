﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AfpEat.Models
{
    public class RestaurantsDetailsModel
    {
        public Restaurant Restaurant { get; set; }
        public List<RestaurantProduits> RestaurantProduits { get; set; }
        public List<RestaurantMenus> RestaurantMenus { get; set; }
        public PanierModel Panier { get; set; }
        public RestaurantsDetailsModel()
        {
            RestaurantProduits = new List<RestaurantProduits>();
            RestaurantMenus = new List<RestaurantMenus>();
        }
    }
}