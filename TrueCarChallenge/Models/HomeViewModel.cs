
using System.Collections.Generic;
using System.Web.Mvc;

namespace TrueCarChallenge.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            MyVehicles = new List<Vehicle>();
            AllMakes = new List<SelectListItem>();
        }

        public int User { get; set; }
        public int SelectedVehicle { get; set; }
        public int SelectedVehicleMPG { get; set; }
        public IList<SelectListItem> AllMakes { get; set; }
        public IList<Vehicle> MyVehicles { get; set; }
    }
}