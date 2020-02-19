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
    public class alumnoesController : Controller
    {
        private cachimboEntities db = new cachimboEntities();

        // GET: alumnoes
        public ActionResult Index()
        {
            var alumno = db.alumno.Include(a => a.carrera);
            return View(alumno.ToList());
        }

        // GET: alumnoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            alumno alumno = db.alumno.Find(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            return View(alumno);
        }

        // GET: alumnoes/Create
        public ActionResult Create()
        {
            ViewBag.codigoCarrera = new SelectList(db.carrera, "codigoCarrera", "nombreCarrea");
            return View();
        }

        // POST: alumnoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "usuarioAlumno,codigoCarrera,contraseñaAlumno,nombreAlumno,apellidoAlumno,celularAlumno,correoAlumno,nivelAlumno,creditoAlumno")] alumno alumno)
        {
            if (ModelState.IsValid)
            {
                //db.alumno.Add(alumno);
                //db.SaveChanges();

                alumno alumno1 = db.alumno.Find(alumno.usuarioAlumno.Trim());
                if (alumno1 != null)
                {
                    ViewBag.errorLogin = "¡El usuario está en uso crack! Piensa en otro, lo harás bien.";
                }
                else
                {
                    if (alumno.contraseñaAlumno != null)
                    {
                        db.registrarAlumno1(alumno.usuarioAlumno, (int)alumno.codigoCarrera, alumno.contraseñaAlumno, alumno.nombreAlumno, alumno.apellidoAlumno, alumno.celularAlumno, alumno.contraseñaAlumno);
                        return RedirectToAction("../Home/login");
                    }
                    else
                    {
                        ViewBag.errorLogin = "¡Ingrese una contraseña!";
                    }
                    
                }
            }

            ViewBag.codigoCarrera = new SelectList(db.carrera, "codigoCarrera", "nombreCarrea", alumno.codigoCarrera);
            return View();
        }

        // GET: alumnoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            alumno alumno = db.alumno.Find(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            ViewBag.codigoCarrera = new SelectList(db.carrera, "codigoCarrera", "nombreCarrea", alumno.codigoCarrera);
            return View(alumno);
        }

        // POST: alumnoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "usuarioAlumno,codigoCarrera,contraseñaAlumno,nombreAlumno,apellidoAlumno,celularAlumno,correoAlumno,nivelAlumno,creditoAlumno")] alumno alumno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alumno).State = EntityState.Modified;
                db.SaveChanges();
                string usuarioAlumno = (string)Session["usuarioAlumno"];
                if (usuarioAlumno == "admin")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("../Home/getPerfil");
                }
            }
            ViewBag.codigoCarrera = new SelectList(db.carrera, "codigoCarrera", "nombreCarrea", alumno.codigoCarrera);
            return View(alumno);
        }

        // GET: alumnoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            alumno alumno = db.alumno.Find(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            return View(alumno);
        }

        // POST: alumnoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            alumno alumno = db.alumno.Find(id);
            db.alumno.Remove(alumno);
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
