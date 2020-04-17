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
    public class CarsController : Controller
    {
        private FinalProjectEntities db = new FinalProjectEntities();

        // GET: Cars
        public ActionResult Index()
        {
            var cars = db.Cars.Include(c => c.Dealership);
            return View(cars.ToList());
        }

        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
        [Authorize(Roles = "Dealer, Admin")]
        public ActionResult Create()
        {
            ViewBag.DealershipId = new SelectList(db.Dealerships, "DealershipId", "DealershipName");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Dealer, Admin")]
        public ActionResult Create([Bind(Include = "CarId,Make,Year,Model,Color,CarPhoto,Description,PricePerDay,IsBooked,IsAutomatic,IsDiesel,IsElectric,HasGPS,HasBluetooth,DealershipId")] Car car, HttpPostedFileBase fupThisImage)
        {
            #region FileUpload Create
            string imageName = "noImage.png";
            if (ModelState.IsValid)
            {
                if (fupThisImage != null)
                {
                    imageName = fupThisImage.FileName;
                    string extension = imageName.Substring(imageName.LastIndexOf("."));
                    string[] goodExtensions = new string[] { ".jpg", ".jfif", ".png", ".jpeg", ".gif" };
                    if (goodExtensions.Contains(extension.ToLower()))
                    {
                        imageName = Guid.NewGuid().ToString() + extension;
                        fupThisImage.SaveAs(Server.MapPath("~/Content/assets/img/car/" + imageName));
                    }
                    else
                    {
                        ViewBag.Alert = "There was an error creating a new car for rent.";
                        ViewBag.DealershipId = new SelectList(db.Dealerships, "DealershipId", "DealershipName", car.DealershipId);
                        return View(car);
                    }
                }
            }
            else
            {
                ViewBag.DealershipId = new SelectList(db.Dealerships, "DealershipId", "DealershipName", car.DealershipId);
                return View(car);
            }
            car.CarPhoto = imageName;
            #endregion
            db.Cars.Add(car);
            db.SaveChanges();
            ViewBag.DealershipId = new SelectList(db.Dealerships, "DealershipId", "DealershipName", car.DealershipId);
            return RedirectToAction("Index");
        }

        // GET: Cars/Edit/5
        [Authorize(Roles = "Dealer, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.DealershipId = new SelectList(db.Dealerships, "DealershipId", "DealershipName", car.DealershipId);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Dealer, Admin")]
        public ActionResult Edit([Bind(Include = "CarId,Make,Year,Model,Color,CarPhoto,Description,PricePerDay,IsBooked,IsAutomatic,IsDiesel,IsElectric,HasGPS,HasBluetooth,DealershipId")] Car car, HttpPostedFileBase fupThisImage)
        {
            if (ModelState.IsValid)
            {
                #region FileUpload Edit
                if (fupThisImage != null)
                {
                    string imageName = fupThisImage.FileName;
                    string extension = imageName.Substring(imageName.LastIndexOf("."));
                    string[] goodExtensions = new string[] { ".jpg", ".jfif", ".png", ".jpeg", ".gif" };
                    if (goodExtensions.Contains(extension.ToLower()))
                    {
                        imageName = Guid.NewGuid().ToString() + extension;
                        fupThisImage.SaveAs(Server.MapPath("~/Content/assets/img/car/" + imageName));
                        if (car.CarPhoto != null && car.CarPhoto != "noImage.png")
                        {
                            System.IO.File.Delete(Server.MapPath("~/Content/assets/img/car/" + car.CarPhoto));
                        }
                        car.CarPhoto = imageName;
                    }
                    else
                    {
                        ViewBag.Alert = "It went wrong bruh";
                        ViewBag.DealershipId = new SelectList(db.Dealerships, "DealershipId", "DealershipName", car.DealershipId);
                        return View(car);
                    }
                }
                #endregion
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DealershipId = new SelectList(db.Dealerships, "DealershipId", "DealershipName", car.DealershipId);
            return View(car);
        }

        // GET: Cars/Delete/5
        [Authorize(Roles = "Dealer, Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Dealer, Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
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
