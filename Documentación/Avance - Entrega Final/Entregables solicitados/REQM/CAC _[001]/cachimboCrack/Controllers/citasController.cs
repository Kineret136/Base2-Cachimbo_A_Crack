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
    public class citasController : Controller
    {
        private cachimboEntities db = new cachimboEntities();

        // GET: citas
        public ActionResult Index()
        {
            var cita = db.cita.Include(c => c.alumno).Include(c => c.curso);
            return View(cita.ToList());
        }

        public ActionResult citaCurso(int codigoCurso, string nombreCurso)
        {
            Session["codigoCurso"] = codigoCurso;
            Session["nombreCurso"] = nombreCurso;
            var lista = from c in db.cita
                        where c.codigoCurso == codigoCurso
                        select c;
            return View(lista.ToList());
        }


        // GET: citas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cita cita = db.cita.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // GET: citas/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: citas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigoCita,usuarioAlumno,codigoCurso,tipoCita,diaCita,horaCita,codigoParticipante")] cita cita)
        {
            if (ModelState.IsValid)
            {
                //db.cita.Add(cita);
                //db.SaveChanges();
                db.registrarCita((string)Session["usuarioAlumno"], (int)Session["codigoCurso"], cita.tipoCita, cita.diaCita, cita.horaCita);
                return RedirectToAction("citaCurso", new { codigoCurso = Session["codigoCurso"], nombreCurso = Session["nombreCurso"] });
            }
            return View(cita);
        }

        // GET: citas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cita cita = db.cita.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            ViewBag.usuarioAlumno = new SelectList(db.alumno, "usuarioAlumno", "contraseñaAlumno", cita.usuarioAlumno);
            ViewBag.codigoCurso = new SelectList(db.curso, "codigoCurso", "nombreCurso", cita.codigoCurso);
            return View(cita);
        }

        // POST: citas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigoCita,usuarioAlumno,codigoCurso,tipoCita,diaCita,horaCita,codigoParticipante")] cita cita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.usuarioAlumno = new SelectList(db.alumno, "usuarioAlumno", "contraseñaAlumno", cita.usuarioAlumno);
            ViewBag.codigoCurso = new SelectList(db.curso, "codigoCurso", "nombreCurso", cita.codigoCurso);
            return View(cita);
        }

        // GET: citas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cita cita = db.cita.Find(id);
            if (cita == null)
            {
                return HttpNotFound();
            }
            return View(cita);
        }

        // POST: citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cita cita = db.cita.Find(id);
            db.cita.Remove(cita);
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

        public ActionResult inscribirCita(int codigoCita)
        {
            db.inscribirCita(codigoCita, (string)Session["usuarioAlumno"]);
            string codigoCurso = (string)Session["codigoCurso"];
            if (codigoCurso == null)
            {
                return RedirectToAction("../Home/getCita");
            }
            else
            {
                return RedirectToAction("citaCurso", new { codigoCurso = Session["codigoCurso"], nombreCurso = Session["nombreCurso"] });
            }

            
        }
    }
}
