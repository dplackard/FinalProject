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
    [Authorize(Roles = "Dealer, Admin")]
    public class DealershipsController : Controller
    {
        private FinalProjectEntities db = new FinalProjectEntities();

        // GET: Dealerships
        public ActionResult Index()
        {
            return View(db.Dealerships.ToList());
        }

        // GET: Dealerships/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dealership dealership = db.Dealerships.Find(id);
            if (dealership == null)
            {
                return HttpNotFound();
            }
            return View(dealership);
        }

        // GET: Dealerships/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dealerships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DealershipId,DealershipName,Address,City,State,ZipCode,PhoneNumber")] Dealership dealership)
        {
            if (ModelState.IsValid)
            {
                dealership.DealershipId = User.Identity.GetUserId();
                db.Dealerships.Add(dealership);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dealership);
        }

        // GET: Dealerships/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dealership dealership = db.Dealerships.Find(id);
            if (dealership == null)
            {
                return HttpNotFound();
            }
            return View(dealership);
        }

        // POST: Dealerships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DealershipId,DealershipName,Address,City,State,ZipCode,PhoneNumber")] Dealership dealership)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dealership).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dealership);
        }

        // GET: Dealerships/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dealership dealership = db.Dealerships.Find(id);
            if (dealership == null)
            {
                return HttpNotFound();
            }
            return View(dealership);
        }

        // POST: Dealerships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Dealership dealership = db.Dealerships.Find(id);
            db.Dealerships.Remove(dealership);
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
