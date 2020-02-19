using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cachimboCrack.Models;

namespace cachimboCrack.Controllers
{
    public class carrerasController : Controller
    {
        private cachimboEntities db = new cachimboEntities();

        // GET: carreras
        public ActionResult Index()
        {
            var carrera = db.carrera.Include(c => c.area);
            return View(carrera.ToList());
        }

        // GET: carreras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            carrera carrera = db.carrera.Find(id);
            if (carrera == null)
            {
                return HttpNotFound();
            }
            return View(carrera);
        }

        // GET: carreras/Create
        public ActionResult Create()
        {
            ViewBag.codigoArea = new SelectList(db.area, "codigoArea", "nombreArea");
            return View();
        }

        // POST: carreras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigoCarrera,codigoArea,nombreCarrea")] carrera carrera)
        {
            if (ModelState.IsValid)
            {
                db.carrera.Add(carrera);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.codigoArea = new SelectList(db.area, "codigoArea", "nombreArea", carrera.codigoArea);
            return View(carrera);
        }

        // GET: carreras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            carrera carrera = db.carrera.Find(id);
            if (carrera == null)
            {
                return HttpNotFound();
            }
            ViewBag.codigoArea = new SelectList(db.area, "codigoArea", "nombreArea", carrera.codigoArea);
            return View(carrera);
        }

        // POST: carreras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigoCarrera,codigoArea,nombreCarrea")] carrera carrera)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carrera).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codigoArea = new SelectList(db.area, "codigoArea", "nombreArea", carrera.codigoArea);
            return View(carrera);
        }

        // GET: carreras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            carrera carrera = db.carrera.Find(id);
            if (carrera == null)
            {
                return HttpNotFound();
            }
            return View(carrera);
        }

        // POST: carreras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            carrera carrera = db.carrera.Find(id);
            db.carrera.Remove(carrera);
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
