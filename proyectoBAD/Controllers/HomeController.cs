using proyectoBAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.Controllers
{
    public class HomeController : Controller
    {
        //TODO: Poner las anotaciones con respecto a quien debe tener permisos para acceder
        private proyectoBADEntities db = new proyectoBADEntities();
        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            int compra = db.compras.Count();
            int equipo = db.equipos.Count();
            int bitacora = db.bitacora_mantenimiento.Count();
            int solicitud = db.solicitudes.Count();

            ViewBag.Dato1 = compra;
            ViewBag.Dato2 = equipo;
            ViewBag.Dato3 = bitacora;
            ViewBag.Dato4 = solicitud;

            return View();
        }
    }
}