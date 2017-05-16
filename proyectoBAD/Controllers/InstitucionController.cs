using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyectoBAD.Models;

namespace proyectoBAD.Controllers
{
    public class InstitucionController : Controller
    {
        //TODO: Poner la anotación con respecto a quien debe tener permisos para acceder

        // GET: Institucion
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(instituciones institucion)
        {
            return View();
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(instituciones institucion)
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}