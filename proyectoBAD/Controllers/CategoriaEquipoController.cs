using proyectoBAD.Models;
using proyectoBAD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.Controllers
{
    public class CategoriaEquipoController : Controller
    {
        //TODO: Poner las anotaciones con respecto a quien debe tener permisos para acceder
        private proyectoBADEntities db = new proyectoBADEntities();

        // GET: CategoriaEquipo
        [Authorize]
        public ActionResult Index()
        {
            List<CategoriaEquipoViewModel> categoriasEq = db.categorias_equipo.Select(t => new CategoriaEquipoViewModel()
            {
                ID = t.id,
                CATEGORIA = t.categoria
            }
                
                ).ToList();
            if (TempData["successMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["successMessage"];
            }

            return View(categoriasEq);
        }
       

        public ActionResult Create()
        {
            CategoriaEquipoViewModel vModel = new CategoriaEquipoViewModel();
            ViewBag.Button = "Agregar";
            ViewBag.Action = "Create";
            return View(vModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoriaEquipoViewModel vModel)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    categorias_equipo categoriasEq = new categorias_equipo();
                    categoriasEq.categoria = vModel.CATEGORIA;
                    db.categorias_equipo.Add(categoriasEq);
                    db.SaveChanges();
                    TempData["successMessage"] = "Categoria creada exitosamente";
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["successMessage"] = "Categoria no creada";
                }
            }
            return View();
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            categorias_equipo categoriasEq = db.categorias_equipo.Find(id);
            ViewBag.Button = "Editar";
            ViewBag.Action = "Edit";

            if (categoriasEq != null)
            {
                CategoriaEquipoViewModel vModel = new CategoriaEquipoViewModel();
                vModel.ID = categoriasEq.id;
                vModel.CATEGORIA = categoriasEq.categoria;

                return View("Create",vModel);
            }

            return View("Create", null);
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoriaEquipoViewModel vModel)
        {
            if (ModelState.IsValid)
            {
                categorias_equipo categoriasEq = db.categorias_equipo.Find(vModel.ID);
                if(categoriasEq != null)
                {
                    categoriasEq.categoria = vModel.CATEGORIA;
                    db.SaveChanges();

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

       
        public ActionResult Delete(int id)
        {
            try
            {
                categorias_equipo categoriasEq = db.categorias_equipo.Find(id);
                db.categorias_equipo.Remove(categoriasEq);
                db.SaveChanges();
                TempData["successMessage"] = "Categoria Eliminada";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["successMessage"] = "Error al eliminar la categoria Revise la Adm. de Equipo";
                return RedirectToAction("Index");
            }

            
        }
    }
}