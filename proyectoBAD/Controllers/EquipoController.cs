using proyectoBAD.Models;
using proyectoBAD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.Controllers
{
    public class EquipoController : Controller
    {
        //TODO: Poner las anotaciones con respecto a quien debe tener permisos para acceder
        private proyectoBADEntities db = new proyectoBADEntities();

        // GET: Equipo
        [Authorize]
        public ActionResult Index()
        {
            List<EquipoViewModel> equipos = db.equipos.Select(t => new EquipoViewModel()
            {
                IDEQ = t.id,
                NOMEQ = t.nombre,
                ESPECEQ = t.especificaciones,
                CATEQUIPO = t.categorias_equipo,
            }).ToList();
            if (TempData["successMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["successMessage"];
            }
            return View(equipos);
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            equipos equipo = db.equipos.Find(id);

            ViewBag.Button = "Editar";
            ViewBag.Action = "Edit";

            if (equipo != null)
            {
                EquipoViewModel vmEquipo = new EquipoViewModel();
                IEnumerable<SelectListItem> list = db.categorias_equipo.Select(t => new SelectListItem()
                {
                    Text = t.categoria,
                    Value = t.id.ToString()
                });
                vmEquipo.IDEQ = equipo.id;
                vmEquipo.NOMEQ = equipo.nombre;
                vmEquipo.ESPECEQ = equipo.especificaciones;
                vmEquipo.IDCATEQ = equipo.categoria;
                vmEquipo.categoriaEquipo = list;

                return View("Create", vmEquipo);
            }
            return View("Crate", null);

        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EquipoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                equipos equipo = db.equipos.Find(viewModel.IDEQ);
                if (equipo != null)
                {
                    equipo.nombre = viewModel.NOMEQ;
                    equipo.especificaciones = viewModel.ESPECEQ;
                    equipo.categoria = viewModel.IDCATEQ;

                    db.SaveChanges();

                    TempData["successMessage"] = "Equipo editado exitosamente";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error a la hora de guardar los datos por favor intente mas tarde");
                }
            }

            return View();
        }
        [Authorize]
        public ActionResult Create()
        {
            IEnumerable<SelectListItem> list = db.categorias_equipo.Select(t => new SelectListItem()
            {
                Text = t.categoria,
                Value = t.id.ToString()
            });
            EquipoViewModel viewModel = new EquipoViewModel
            {
                categoriaEquipo = list,
            };
            ViewBag.Button = "Agregar";
            ViewBag.Action = "Create";

            return View(viewModel);
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EquipoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                equipos equipo = new equipos();

                equipo.nombre = viewModel.NOMEQ;
                equipo.especificaciones = viewModel.ESPECEQ;
                equipo.categoria = viewModel.IDCATEQ;

                db.equipos.Add(equipo);
                db.SaveChanges();

                TempData["seccessMessage"] = "Equipo creado exitosamente";
                return RedirectToAction("Index");
            }
            return View();
        }
        
        [HttpPost]
        public ActionResult createCat()
        {
            return View();
        }
        public JsonResult createCat(string cat)
        {
            if (ModelState.IsValid)
            {
                categorias_equipo categoria = new categorias_equipo();
                categoria.categoria = cat;
                db.categorias_equipo.Add(categoria);
                db.SaveChanges();
                return Json("Records added Successfully.");
            }
            return Json("Records not added,");
        }
        /*
        [HttpPost]
        [Authorize]
        public JsonResult createCat(EquipoViewModel vm) // Record Insert  
        {
            categorias_equipo cate = new categorias_equipo();
            cate.categoria = vm.CATEGORIA;
            db.categorias_equipo.Add(cate);
            db.SaveChanges();

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public JsonResult getCat(EquipoViewModel vModel)
        {


            return Json(vModel.CATEGORIA, JsonRequestBehavior.AllowGet);
        }
        */

        public ActionResult Delete(int id)
        {
            try
            {
                equipos equipo = db.equipos.Find(id);
                db.equipos.Remove(equipo);
                db.SaveChanges();
                TempData["successMessage"] = "equipo Eliminado correctamente";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["successMessage"] = "Error al eliminar equipo Revise la Adm. de Especificación de Equipo";
                return RedirectToAction("Index");
            }
           
        }

        public ActionResult ShowEquipo(int id)
        {
            return PartialView("viewEquipo");
        }

    }
}