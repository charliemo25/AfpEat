using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AfpEat.Models;

namespace AfpEat.Controllers
{
    public class RestaurantsController : Controller
    {
        private AfpEatEntities db = new AfpEatEntities();


        // GET: Restaurants
        public ActionResult Index()
        {
            var restaurants = db.Restaurants.Include(r => r.TypeCuisine);
            return View(restaurants.ToList());
        }

        public List<RestaurantProduits> RestaurantProduits(Restaurant restaurant)
        {
            List<RestaurantProduits> listRestaurantProduits = new List<RestaurantProduits>();

            foreach (var idCategorie in restaurant.ProduitCategories.GroupBy(x => x.IdCategorie))
            {
                RestaurantProduits restaurantProduits = new RestaurantProduits();

                restaurantProduits.NomCategorie = idCategorie.First().Categorie.Nom;
                foreach (var categorie in idCategorie)
                {
                    restaurantProduits.Produits.Add(categorie.Produit);
                }

                listRestaurantProduits.Add(restaurantProduits);
            }

            return listRestaurantProduits;
        }

        // GET: Restaurants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }

            // Liste de produits par categorie
            List<RestaurantProduits> listRestaurantProduits = RestaurantProduits(restaurant);

            // Liste des Menus du restaurant
            List<RestaurantMenus> listRestaurantMenus = new List<RestaurantMenus>();

            // Liste des Menus
            List<Menu> menus = new List<Menu>();

            foreach (var menu in restaurant.MenuCategories)
            {
                //Ajoute un menu si il n'est pas présent dans la liste
                if (!menus.Contains(menu.Menu))
                {
                    menus.Add(menu.Menu);
                }
            }

            //Parcours des menu par categorie
            foreach (var idCategorie in restaurant.MenuCategories.GroupBy(m => m.IdCategorie))
            {
                // initialise un restaurantMenus
                RestaurantMenus restaurantMenus = new RestaurantMenus();

                foreach (var menu in idCategorie)
                {
                    
                    restaurantMenus.Menu = menu.Menu;
                    
                }
                //Verifier si le menu existe dans RestaurantMenus
                if (!listRestaurantMenus.Contains(restaurantMenus))
                {
                    //Ajout dans la liste de RestaurantMenus
                    listRestaurantMenus.Add(restaurantMenus);

                    //Ajout de la liste de Produits
                }
            }
            //Class à envoyer a la vue
            RestaurantsDetailsModel restaurantsDetailsModel = new RestaurantsDetailsModel()
            {
                Restaurant = restaurant,
                RestaurantProduits = listRestaurantProduits,
                Menus = menus
            };

            return View(restaurantsDetailsModel);
        }

        // GET: Restaurants/Create
        public ActionResult Create()
        {
            ViewBag.IdTypeCuisine = new SelectList(db.TypeCuisines, "IdTypeCuisine", "Nom");
            return View();
        }

        // POST: Restaurants/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRestaurant,Nom,IdTypeCuisine,Description,Adresse,Tel,Mobile,Email,CodePostal,Ville,Statut")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Restaurants.Add(restaurant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdTypeCuisine = new SelectList(db.TypeCuisines, "IdTypeCuisine", "Nom", restaurant.IdTypeCuisine);
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTypeCuisine = new SelectList(db.TypeCuisines, "IdTypeCuisine", "Nom", restaurant.IdTypeCuisine);
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRestaurant,Nom,IdTypeCuisine,Description,Adresse,Tel,Mobile,Email,CodePostal,Ville,Statut")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdTypeCuisine = new SelectList(db.TypeCuisines, "IdTypeCuisine", "Nom", restaurant.IdTypeCuisine);
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            db.Restaurants.Remove(restaurant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
