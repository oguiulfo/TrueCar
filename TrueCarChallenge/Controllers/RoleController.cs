using System;
using System.Linq;
using System.Web.Mvc;

using TrueCarChallenge.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using TrueCarChallenge.CustomFilters;

namespace TrueCarChallenge.Controllers
{
    public class RoleController : Controller
    {
        ApplicationDbContext context;

        public RoleController()
        {
            context = new ApplicationDbContext();
        }
        
        public ActionResult Index()
        {
            var model = new List<IdentityRole>();

            if (context.Roles.Count() != 0)
            {
                var Roles = context.Roles.ToList();
                return View(Roles);
            }

            return View(model);
        }


        [AuthLog(Roles = "Admin")]
        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }
        
        [HttpPost]
        [AuthLog(Roles = "Admin")]
        public ActionResult Create(IdentityRole Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}