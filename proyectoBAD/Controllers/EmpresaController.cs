using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyectoBAD.Models;
using proyectoBAD.ViewModels;

namespace proyectoBAD.Controllers
{
    public class EmpresaController : Controller
    {
        //TODO: Poner las anotaciones con respecto a quien debe tener permisos para acceder
        private proyectoBADEntities db = new proyectoBADEntities();

        // GET: Empresa
        [Authorize]
        public ActionResult Index()
        {
            List<EmpresaViewModel> empresas = db.empresas.Select(t => new EmpresaViewModel()
            {
                IDEMP = t.id,
                TIPOEMP = t.tipos_empresa,
                DIRECCIONEMP = t.direccion,
                EMAILCONTACTO = t.email_contacto,
                NOMBREEMP = t.nombre,
                NOMBRERESPEMP = t.nombre_responsable,
                TELEFONOCONTACTO = t.telefono

            }).ToList();
            if (TempData["successMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["successMessage"];
            }

            return View(empresas);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            empresas empresa = db.empresas.Find(id);

            ViewBag.Button = "Editar";
            ViewBag.Action = "Edit";

            if (empresa != null)
            {
                EmpresaViewModel vmEmpresa = new EmpresaViewModel();
                IEnumerable<SelectListItem> list = db.tipos_empresa.Select(t => new SelectListItem()
                {
                    Text = t.tipo,
                    Value = t.id.ToString()
                });

                vmEmpresa.IDEMP = empresa.id;
                vmEmpresa.NITEMP = empresa.nit;
                vmEmpresa.NOMBREEMP = empresa.nombre;
                vmEmpresa.NOMBRERESPEMP = empresa.nombre_responsable;
                vmEmpresa.TELEFONOCONTACTO = empresa.telefono;
                vmEmpresa.DUIRESPEMP = empresa.dui_responsable;
                vmEmpresa.DIRECCIONEMP = empresa.direccion;
                vmEmpresa.IDTIPO = empresa.tipo;
                vmEmpresa.EMAILCONTACTO = empresa.email_contacto;
                vmEmpresa.tiposEmpresa = list;

                return View("Create", vmEmpresa);
            }

            return View("Create", null);

        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmpresaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                empresas empresa = db.empresas.Find(viewModel.IDEMP);
                
                if (empresa != null)
                {
                    empresa.nombre = viewModel.NOMBREEMP;
                    empresa.direccion = viewModel.DIRECCIONEMP;
                    empresa.email_contacto = viewModel.EMAILCONTACTO;
                    empresa.telefono = viewModel.TELEFONOCONTACTO;
                    empresa.nit = viewModel.NITEMP;
                    empresa.tipo = viewModel.IDTIPO;
                    empresa.nombre_responsable = viewModel.NOMBRERESPEMP;
                    empresa.dui_responsable = viewModel.DUIRESPEMP;

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

        [Authorize]
        public ActionResult Create()
        {
            IEnumerable<SelectListItem> list = db.tipos_empresa.Select(t => new SelectListItem()
            {
                Text = t.tipo,
                Value = t.id.ToString()
            });
            EmpresaViewModel viewModel = new EmpresaViewModel
            {
                tiposEmpresa = list,
            };

            ViewBag.Button = "Agregar";
            ViewBag.Action = "Create";

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmpresaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                empresas empresa = new empresas();

                empresa.nombre = viewModel.NOMBREEMP;
                empresa.direccion = viewModel.DIRECCIONEMP;
                empresa.email_contacto = viewModel.EMAILCONTACTO;
                empresa.telefono = viewModel.TELEFONOCONTACTO;
                empresa.nit = viewModel.NITEMP;
                empresa.tipo = viewModel.IDTIPO;
                empresa.nombre_responsable = viewModel.NOMBRERESPEMP;
                empresa.dui_responsable = viewModel.DUIRESPEMP;

                db.empresas.Add(empresa);
                db.SaveChanges();

                TempData["successMessage"] = "Empresa creada exitosamente";

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