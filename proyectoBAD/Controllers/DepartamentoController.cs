using proyectoBAD.Models;
using proyectoBAD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.Controllers
{
    public class DepartamentoController : Controller
    {
        //TODO: Poner las anotaciones con respecto a quien debe tener permisos para acceder
        private proyectoBADEntities db = new proyectoBADEntities();

        // GET: Departamento
        [Authorize]
        public ActionResult Index()
        {
            List<DepartamentoViewModel> departamentos = db.departamentos.Select(t => new DepartamentoViewModel()
            {
               id = t.id,
               institucion = t.instituciones,
               nombre = t.nombre,
               telefono = t.telefono,
               email_contacto = t.email_contacto
            }).ToList();
            if (TempData["successMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["successMessage"];
            }

            return View(departamentos);
        }

        [Authorize]
        public ActionResult Create()
        {
            IEnumerable<SelectListItem> list = db.instituciones.Select(t => new SelectListItem()
            {
                Text = t.nombre,
                Value = t.id.ToString()
            });

            DepartamentoViewModel viewModel = new DepartamentoViewModel
            {
                instituciones = list
            };

            ViewBag.Button = "Agregar";
            ViewBag.Action = "Create";

            return View(viewModel);
        }
        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartamentoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                departamentos departamento = new departamentos();

                departamento.nombre = viewModel.nombre;
                departamento.telefono = viewModel.telefono;
                departamento.email_contacto = viewModel.email_contacto;
                departamento.institucion = viewModel.idInst;

                db.departamentos.Add(departamento);
                db.SaveChanges();

                TempData["successMessage"] = "Departamento creado exitosamente";

                return RedirectToAction("Index");
            }
            return View();
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            departamentos departamento = db.departamentos.Find(id);

            ViewBag.Button = "Editar";
            ViewBag.Action = "Edit";

            if (departamento != null)
            {
                DepartamentoViewModel viewModel = new DepartamentoViewModel()
                {
                    nombre = departamento.nombre,
                    id = departamento.id,
                    telefono = departamento.telefono,
                    email_contacto = departamento.email_contacto,
                    idInst = departamento.institucion,
                    instituciones = db.instituciones.Select(t => new SelectListItem()
                    {
                        Text = t.nombre,
                        Value = t.id.ToString()
                    })
                };

                return View("Create", viewModel);
            }

            return View("Create", null);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DepartamentoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                departamentos departamento = db.departamentos.Find(viewModel.id);

                if (departamento != null)
                {
                    departamento.nombre = viewModel.nombre;
                    departamento.telefono = viewModel.telefono;
                    departamento.email_contacto = viewModel.email_contacto;
                    departamento.institucion = viewModel.idInst;

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


    }
}