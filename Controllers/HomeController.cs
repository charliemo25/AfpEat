﻿using AfpEat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AfpEat.Controllers
{
    public class HomeController : Controller
    {

        private AfpEatEntities db = new AfpEatEntities();

        public ActionResult Index()
        {

            ViewBag.Restaurants = db.Restaurants.ToList();
            return View(db.TypeCuisines.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}