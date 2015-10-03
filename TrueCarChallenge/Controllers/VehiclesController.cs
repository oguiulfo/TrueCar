using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using TrueCarChallenge.CustomFilters;
using TrueCarChallenge.Models;

namespace TrueCarChallenge
{
    public class VehiclesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Vehicles
        [AuthLog(Roles = "Admin")]
        public ActionResult Index()
        {
            var listView = new List<VehiclesViewModel>();
            var listVehicles = new List<Vehicle>();

            // Convert joined db graph to a typed Vehicle list
            var dbVehicles = (from v in db.Vehicles
                              join m in db.Makes on v.Make.ID equals m.ID
                              select new { m.Name, v.MPG }).ToList();

            foreach (var v in dbVehicles)
            {
                var vehicle = new Vehicle()
                {
                    Make = new Make() { Name = v.Name },
                    MPG = v.MPG
                };

                listVehicles.Add(vehicle);
            }

            var makes = db.Makes.ToList();
            var masterList = new List<List<Vehicle>>();

            // Arrange Vehicles by Make 
            foreach (var m in makes)
            {
                var newList = new List<Vehicle>();

                foreach (var v in listVehicles)
                {
                    if (v.Make.Name == m.Name)
                        newList.Add(v);
                }

                masterList.Add(newList);
            }

            // Perform necessary operations and bind to the ViewModel
            foreach (var vehicleList in masterList)
            {
                if (vehicleList.Count != 0)
                {
                    var vm = new VehiclesViewModel()
                    {
                        Make = vehicleList.Select(c => c.Make.Name).FirstOrDefault(),
                        AvgMPG = (from v in vehicleList select v.MPG).Average(),
                        MaxMPG = (from v in vehicleList select v.MPG).Max(),
                        MinMPG = (from v in vehicleList select v.MPG).Min()
                    };

                    listView.Add(vm);
                }
            }

            return View(listView);
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
