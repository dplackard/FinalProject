using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.DATA.EF;
namespace FinalProject.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private FinalProjectEntities db = new FinalProjectEntities();
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.CarsCount = db.Cars.Count();
            ViewBag.UsersCount = db.Reservations.Count();
            ViewBag.DealersCount = db.Dealerships.Count();
            return View();
        }

        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }


    }
}
