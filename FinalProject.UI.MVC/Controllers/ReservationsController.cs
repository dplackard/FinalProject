using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.DATA.EF;
using Microsoft.AspNet.Identity;

namespace FinalProject.UI.MVC.Controllers
{
    [Authorize(Roles = "Customer, Dealer, Admin")]
    public class ReservationsController : Controller
    {
        private FinalProjectEntities db = new FinalProjectEntities();

        // GET: Reservations
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var reservations = db.Reservations.Where(p => p.UserId == userId);
            if (User.IsInRole("Admin"))
            {
                reservations = db.Reservations;
            }
            else if (User.IsInRole("Dealer"))
            {
                reservations = db.Reservations.Where(p => p.Car.DealershipId == userId);
            }
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            if (User.IsInRole("Admin"))
            {
                ViewBag.CarId = new SelectList(db.Cars, "CarId", "CarSelection");
                ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName");
            }
            else if (User.IsInRole("Dealer"))
            {
                ViewBag.CarId = new SelectList(db.Cars, "CarId", "CarSelection");
                ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName");
            }
            else if (User.IsInRole("Customer"))
            {
                ViewBag.CarId = new SelectList(db.Cars.Where(p => p.IsBooked == false), "CarId", "CarSelection");
                ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName");
            }
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationId,CarId,UserId,ReservationDate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                if (User.IsInRole("Admin"))
                {
                    reservation.ReservationDate = DateTime.Now;
                    Car carsAdmin = db.Cars.Where(p => p.CarId == reservation.CarId).Single();
                    carsAdmin.IsBooked = true;
                    db.Entry(carsAdmin).State = EntityState.Modified;
                    db.Reservations.Add(reservation);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                if (User.IsInRole("Dealer"))
                {
                    reservation.ReservationDate = DateTime.Now;
                    Car carsDealer = db.Cars.Where(p => p.CarId == reservation.CarId).Single();
                    carsDealer.IsBooked = true;
                    db.Entry(carsDealer).State = EntityState.Modified;
                    db.Reservations.Add(reservation);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                if (User.IsInRole("Customer"))
                {
                    reservation.ReservationDate = DateTime.Now;
                    reservation.UserId = User.Identity.GetUserId();
                    Car car = db.Cars.Where(p => p.CarId == reservation.CarId).Single();
                    car.IsBooked = true;
                    db.Entry(car).State = EntityState.Modified;
                    db.Reservations.Add(reservation);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.CarId = new SelectList(db.Cars.Where(p => p.IsBooked == false), "CarId", "Make", reservation.CarId);
            if (User.IsInRole("Dealer"))
            {
                ViewBag.CarId = new SelectList(db.Cars.Where(p => p.DealershipId == User.Identity.GetUserId()), "CarId", "Make", reservation.CarId);
            }
            if (User.IsInRole("Admin"))
            {
                ViewBag.CarId = new SelectList(db.Cars, "CarId", "Make", reservation.CarId);
            }
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName", reservation.UserId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarId = new SelectList(db.Cars, "CarId", "Make", reservation.CarId);
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName", reservation.UserId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationId,CarId,UserId,ReservationDate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarId = new SelectList(db.Cars, "CarId", "Make", reservation.CarId);//Trying to only allow people to edit the one that they own .Where(p => p.IsBooked == true && p.Reservations.UserId
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "FirstName", reservation.UserId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            Car car = db.Cars.Where(p => p.CarId == reservation.CarId).Single();
            car.IsBooked = false;
            db.Entry(car).State = EntityState.Modified;
            db.Reservations.Remove(reservation);
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
