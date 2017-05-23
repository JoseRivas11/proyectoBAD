using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyectoBAD.Models;
using proyectoBAD.ViewModels;

namespace proyectoBAD.Controllers
{
    public class InstitucionController : Controller
    {
        //TODO: Poner la anotación con respecto a quien debe tener permisos para acceder
        private proyectoBADEntities bd = new proyectoBADEntities();

        // GET: Institucion
        [Authorize]
        public ActionResult Index()
        {
            List<InstitucionViewModel> instituciones = bd.instituciones.Select(t => new InstitucionViewModel()
            {
                id = t.id,
                nombre = t.nombre,
                telefono = t.telefono,
                email_contacto = t.email_contacto,
                direccion = t.direccion,
                max_compras = (decimal)t.max_compras
            }).ToList();

            if (TempData["successMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["successMessage"];
            }

            return View(instituciones);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            instituciones institucion = bd.instituciones.Find(id);

            ViewBag.Button = "Editar";
            ViewBag.Action = "Edit";

            if (institucion != null)
            {
                InstitucionViewModel institucionViewModel = new InstitucionViewModel()
                {
                    id = institucion.id,
                    nombre = institucion.nombre,
                    telefono = institucion.telefono,
                    email_contacto = institucion.email_contacto,
                    direccion = institucion.direccion,
                    max_compras = (decimal)institucion.max_compras
                };
                return View("Create", institucionViewModel);
            }
            else
            {
                return View("Create", null);
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InstitucionViewModel institucionViewModel)
        {
            if (ModelState.IsValid)
            {
                instituciones institucion = bd.instituciones.Find(institucionViewModel.id);

                if (institucion != null)
                {
                    institucion.nombre = institucionViewModel.nombre;
                    institucion.telefono = institucionViewModel.telefono;
                    institucion.direccion = institucionViewModel.direccion;
                    institucion.email_contacto = institucionViewModel.email_contacto;
                    institucion.max_compras = institucionViewModel.max_compras;

                    bd.SaveChanges();

                    TempData["successMessage"] = "Empresa editada exitosamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error a la hora de guardar los datos, por favor intente mas tarde");
                }
            }
            return View();
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Button = "Agregar";
            ViewBag.Action = "Create";

            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InstitucionViewModel institucionViewModel)
        {
            if (ModelState.IsValid)
            {
                instituciones institucion = new instituciones();

                institucion.nombre = institucionViewModel.nombre;
                institucion.telefono = institucionViewModel.telefono;
                institucion.email_contacto = institucionViewModel.email_contacto;
                institucion.direccion = institucionViewModel.direccion;
                institucion.max_compras = institucionViewModel.max_compras;

                bd.instituciones.Add(institucion);
                bd.SaveChanges();

                TempData["successMessage"] = "Institución creada exitosamente";

                return RedirectToAction("Index");
            }

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