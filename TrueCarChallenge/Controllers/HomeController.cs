using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TrueCarChallenge.Models;

namespace TrueCarChallenge.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;

        public HomeController()
        {
            db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var model = new HomeViewModel();

            var userGraph = db.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault();
            if (userGraph != null)
            {
                model.MyVehicles = userGraph.Vehicles;
            }

            var list = db.Makes.ToList();
            var dropDown = new List<SelectListItem>();

            foreach (var i in list)
            {
                dropDown.Add(new SelectListItem { Text = i.Name, Value = i.ID.ToString() });
            }

            model.AllMakes = dropDown;

            return View(model);
        }

        public ActionResult Create([Bind(Include = "ID,Make")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Vehicles.Add(vehicle);
                db.SaveChanges();

                var userGraph = db.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault();
                if (userGraph != null)
                {
                    userGraph.Vehicles.Add(vehicle);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        public ActionResult Edit(HomeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userGraph = db.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault();
                if (userGraph != null)
                {
                    foreach (var updatedVehicle in model.MyVehicles)
                    {
                        foreach (var v in userGraph.Vehicles)
                        {
                            if (v.ID == updatedVehicle.ID)
                                v.MPG = updatedVehicle.MPG;
                        }
                    }

                    db.SaveChanges();
                }
               
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vehicle = db.Vehicles.Where(v => v.ID == id).FirstOrDefault();
            if (vehicle != null)
            {
                db.Vehicles.Remove(vehicle);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Add(HomeViewModel model)
        {
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userGraph = db.Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault();
            if (userGraph != null)
            {
                var vehicleMake = db.Makes.Where(v => v.ID == model.SelectedVehicle).FirstOrDefault();
                var newVehicle = new Vehicle()
                {
                    Make = vehicleMake,
                    MPG = model.SelectedVehicleMPG
                };

                userGraph.Vehicles.Add(newVehicle);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}