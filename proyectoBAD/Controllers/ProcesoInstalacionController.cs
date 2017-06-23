using proyectoBAD.Models;
using proyectoBAD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.Controllers
{
    public class ProcesoInstalacionController : Controller
    {
        //TODO: Poner las anotaciones con respecto a quien debe tener permisos para acceder
        private proyectoBADEntities db = new proyectoBADEntities();

        // GET: BitacoraDeMantenimiento
        [Authorize]
        public ActionResult Index()
        {
            List<ProcesoInstalacionViewModel> proce = db.proc_instalacion.Select(t => new ProcesoInstalacionViewModel()
            {
                idProIns = t.id,
                equipo = t.equipo,                                
                //fechaIniPro = t.fecha_ini,
                //fechaFinPro = t.fecha_fin,
                
                
            }).ToList();
            if (TempData["successMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["successMessage"];
            }
            return View(proce);
        }


        // GET: BitacoraDeMantenimiento/Details/5
        /*public ActionResult Details(int id)
        {
            return View();
        }*/
        [Authorize]
        public ActionResult Edit(int id)
        {
            proc_instalacion Pro = db.proc_instalacion.Find(id);
            ViewBag.Button = "Editar";
            ViewBag.Action = "Edit";

            //ViewBag.PageHeader = "Editar Bitacora de Instalacion";

            if (Pro != null)
            {

                ProcesoInstalacionViewModel vmPro = new ProcesoInstalacionViewModel();
                IEnumerable<SelectListItem> list = db.equipos_fisicos.Select(t => new SelectListItem()
                {
                    Text = t.num_serial,
                    Value = t.num_serial.ToString()
                });
                vmPro.idProIns = Pro.id;
                vmPro.equipo = Pro.equipo;
                //vmPro.fechaIniPro = Pro.fecha_ini;
                //vmPro.fechaFinPro = Pro.fecha_fin;
                vmPro.equipos = list;

                return View("Create", vmPro);
            }
            return View("Create", null);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProcesoInstalacionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                proc_instalacion Pro = db.proc_instalacion.Find(viewModel.idProIns);

                if (Pro != null)
                {

                    Pro.equipo = viewModel.equipo;
                    Pro.fecha_ini = viewModel.fechaIniPro;
                    Pro.fecha_fin = viewModel.fechaFinPro;

                    db.SaveChanges();
                    TempData["SusseceMessage"] = "El proceso de instalacion fue editada con exito";

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
            IEnumerable<SelectListItem> list = db.equipos.Select(t => new SelectListItem()
            {
                Text = t.nombre,
                Value = t.id.ToString()
            });
            ProcesoInstalacionViewModel viewModel = new ProcesoInstalacionViewModel ()
            {
                equipos = list
            };

            ViewBag.Button = "Crear";
            ViewBag.Action = "Create";

            return View(viewModel);
        }

        // GET: BitacoraDeMantenimiento/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProcesoInstalacionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                proc_instalacion Proc = new proc_instalacion();

                Proc.equipo = viewModel.equipo;
                Proc.fecha_ini = viewModel.fechaIniPro;
                Proc.fecha_fin = viewModel.fechaFinPro;
                
                //bitaInsta.proc_instalacion = viewModel.proc_instalacion1;
                

                db.proc_instalacion.Add(Proc);
                db.SaveChanges();

                TempData["successMessage"] = "El proceso de instalacion fue registrada exitosamente";

                return RedirectToAction("Index");
            }
            return View();
        }

        // POST: BitacoraDeMantenimiento/Create
        /*[HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/

        

        

        // POST: BitacoraDeMantenimiento/Edit/5
        /*[HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }*/

        // GET: BitacoraDeMantenimiento/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BitacoraDeMantenimiento/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
