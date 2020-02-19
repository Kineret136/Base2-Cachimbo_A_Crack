using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using cachimboCrack.Models;

namespace cachimboCrack.Controllers
{
    public class HomeController : Controller
    {
        cachimboEntities db = new cachimboEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult login(string usuarioAlumno, string contraseñaAlumno)
        {
            alumno alumno = db.alumno.Find(usuarioAlumno.Trim());
            if (alumno == null)
            {
                ViewBag.errorLogin = "El alumno no ha sido registrado";
            }
            else
            {
                if (alumno.contraseñaAlumno.Trim() == contraseñaAlumno.Trim())
                {
                    Session["usuarioAlumno"] = usuarioAlumno;
                    if (usuarioAlumno == "admin")
                    {
                        return RedirectToAction("IndexAdmin");
                    }
                    else
                    {
                        return RedirectToAction("menuAlumno");
                    }
                }
                else
                {
                    ViewBag.errorLogin = "Contraseña incorrecta";
                }
            }
            return View();
        }

        

public ActionResult cerrarSesion()
        {
            Session["usuarioAlumno"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult menuAlumno()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult getArea()
        {

            return View(db.area.ToList());
        }

        public ActionResult getCursoTotal()
        {

            return View(db.curso.ToList());
        }

        public ActionResult getCurso(int codigoArea, string nombreArea)
        {
            Session["codigoArea"] = codigoArea;
            Session["nombreArea"] = nombreArea;
            var list = from c in db.curso
                       where c.codigoArea == codigoArea
                       select c;
            return View(list.ToList());
        }

        public ActionResult getCita()
        {
            return View(db.cita.ToList());
        }

        public ActionResult getCitasUsuario()
        {
            string usuario = (string)Session["usuarioAlumno"];
            var list = from c in db.cita
                       where c.usuarioAlumno == usuario
                       select c;
            return View(list.ToList());
        }

        public ActionResult getPerfil()
        {
            string usuario = (string)Session["usuarioAlumno"];
            var list = from a in db.alumno
                       where a.usuarioAlumno == usuario
                       select a;
            return View(list.ToList());
        }

        public ActionResult IndexAdmin()
        {
            return View();
        }
    }
}