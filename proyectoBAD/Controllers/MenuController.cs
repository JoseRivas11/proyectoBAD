using proyectoBAD.Models;
using proyectoBAD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.Controllers
{
    public class MenuController : Controller
    {
        private proyectoBADEntities db = new proyectoBADEntities();
        
        // GET: Menu
        [Authorize]
        public ActionResult Index()
        {
            List<MenuViewModel> opcionesMenu = db.menus.Select(t => new MenuViewModel()
            {
                id = t.id,
                nombre_opcion = t.nombre_opcion,
                controlador = t.controlador,
                accion = t.accion,
                superOpcion = t.menus2,
                nombre_estado = t.estado == 1 ? "Activo" : "Inactivo"
            }).ToList();

            if (TempData["successMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["successMessage"];
            }

            return View(opcionesMenu);
        }

        [Authorize]
        public ActionResult Create()
        {
            MenuViewModel viewModel = new MenuViewModel();
            viewModel.superOpciones = db.menus.Where(m => m.super_opcion == null).Select(m => new SelectListItem()
            {
                Text = m.nombre_opcion,
                Value = m.id.ToString()
            }).ToList();

            ViewBag.Button = "Crear";
            ViewBag.Action = "Create";
            ViewBag.PageHeader = "Crear Opción de Menu";
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MenuViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                menus menu = new menus()
                {
                    nombre_opcion = viewModel.nombre_opcion,
                    estado = viewModel.estado != null ? (int)viewModel.estado : 0,
                    accion = viewModel.accion,
                    controlador = viewModel.controlador,
                    super_opcion = viewModel.super_opcion
                };

                db.menus.Add(menu);
                db.SaveChanges();
                TempData["successMessage"] = "Opción de Menú creada exitosamente";

                return RedirectToAction("Index");

            }
            return View(viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            menus menu = db.menus.Find(id);

            MenuViewModel viewModel = new MenuViewModel()
            {
                id = menu.id,
                nombre_opcion = menu.nombre_opcion,
                controlador = menu.controlador,
                accion = menu.accion,
                estado = menu.estado == 0 ? (int?)null : menu.estado,
                super_opcion = menu.super_opcion,
                superOpciones = db.menus.Where(m => m.super_opcion == null && m.id != id).Select(m => new SelectListItem()
                {
                    Text = m.nombre_opcion,
                    Value = m.id.ToString()
                }).ToList()
            };

            ViewBag.Button = "Editar";
            ViewBag.Action = "Edit";
            ViewBag.PageHeader = "Editar Opción de Menu";

            return View("Create" ,viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MenuViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                menus menu = db.menus.Find(viewModel.id);

                menu.nombre_opcion = viewModel.nombre_opcion;
                menu.controlador = viewModel.controlador;
                menu.accion = viewModel.accion;
                menu.super_opcion = viewModel.super_opcion;
                menu.estado = viewModel.estado == null ? 0 : 1;

                db.SaveChanges();
                TempData["successMessage"] = "Compra creada exitosamente";

                return RedirectToAction("Index");
            }

            return View("Create", viewModel);
        }
    }
}