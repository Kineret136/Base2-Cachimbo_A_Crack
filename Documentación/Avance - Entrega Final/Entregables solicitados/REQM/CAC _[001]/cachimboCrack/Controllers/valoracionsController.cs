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
    public class valoracionsController : Controller
    {
        private cachimboEntities db = new cachimboEntities();

        // GET: valoracions
        public ActionResult Index()
        {
            var valoracion = db.valoracion.Include(v => v.alumno).Include(v => v.curso);
            return View(valoracion.ToList());
        }

        // GET: valoracions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            valoracion valoracion = db.valoracion.Find(id);
            if (valoracion == null)
            {
                return HttpNotFound();
            }
            return View(valoracion);
        }

        // GET: valoracions/Create
        public ActionResult Create()
        {
            ViewBag.usuarioAlumno = new SelectList(db.alumno, "usuarioAlumno", "contraseñaAlumno");
            ViewBag.codigoCurso = new SelectList(db.curso, "codigoCurso", "nombreCurso");
            return View();
        }

        // POST: valoracions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigoValoracion,usuarioAlumno,codigoCurso,enseñaValoracion,aprendeValoracion")] valoracion valoracion)
        {
            if (ModelState.IsValid)
            {
                db.valoracion.Add(valoracion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.usuarioAlumno = new SelectList(db.alumno, "usuarioAlumno", "contraseñaAlumno", valoracion.usuarioAlumno);
            ViewBag.codigoCurso = new SelectList(db.curso, "codigoCurso", "nombreCurso", valoracion.codigoCurso);
            return View(valoracion);
        }

        // GET: valoracions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            valoracion valoracion = db.valoracion.Find(id);
            if (valoracion == null)
            {
                return HttpNotFound();
            }
            ViewBag.usuarioAlumno = new SelectList(db.alumno, "usuarioAlumno", "contraseñaAlumno", valoracion.usuarioAlumno);
            ViewBag.codigoCurso = new SelectList(db.curso, "codigoCurso", "nombreCurso", valoracion.codigoCurso);
            return View(valoracion);
        }

        // POST: valoracions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigoValoracion,usuarioAlumno,codigoCurso,enseñaValoracion,aprendeValoracion")] valoracion valoracion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(valoracion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.usuarioAlumno = new SelectList(db.alumno, "usuarioAlumno", "contraseñaAlumno", valoracion.usuarioAlumno);
            ViewBag.codigoCurso = new SelectList(db.curso, "codigoCurso", "nombreCurso", valoracion.codigoCurso);
            return View(valoracion);
        }

        // GET: valoracions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            valoracion valoracion = db.valoracion.Find(id);
            if (valoracion == null)
            {
                return HttpNotFound();
            }
            return View(valoracion);
        }

        // POST: valoracions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            valoracion valoracion = db.valoracion.Find(id);
            db.valoracion.Remove(valoracion);
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
