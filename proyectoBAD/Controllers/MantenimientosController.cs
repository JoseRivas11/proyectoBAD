using proyectoBAD.Models;
using proyectoBAD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.Controllers
{
    public class MantenimientosController : Controller
    {
        //TODO: Poner las anotaciones con respecto a quien debe tener permisos para acceder
        private proyectoBADEntities db = new proyectoBADEntities();

        // GET: BitacoraDeMantenimiento
        [Authorize]
        public ActionResult Index()
        {
            List<MantenimientosViewModel> mantes = db.mantenimientos.Select(t => new MantenimientosViewModel()
            {
                idMan = t.id,                                
                fechaIniMant = t.fecha_ini,
                fechaFinMant = t.fecha_fin,
                tipoMant= t.tipo,
                equipoFisi = t.equipo_fisico,
                
            }).ToList();
            if (TempData["successMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["successMessage"];
            }
            return View(mantes);
        }


        // GET: BitacoraDeMantenimiento/Details/5
        /*public ActionResult Details(int id)
        {
            return View();
        }*/
        [Authorize]
        public ActionResult Edit(int id)
        {
            mantenimientos Man = db.mantenimientos.Find(id);
            ViewBag.Button = "Editar";
            ViewBag.Action = "Edit";

            //ViewBag.PageHeader = "Editar Bitacora de Instalacion";

            if (Man != null)
            {

                MantenimientosViewModel vmMan = new MantenimientosViewModel();

                List<SelectListItem> tipoMantenimiento = new List<SelectListItem>();

                tipoMantenimiento.Add(new SelectListItem()
                {
                    Text = "Preventivo",
                    Value = "1"
                });

                tipoMantenimiento.Add(new SelectListItem()
                {
                    Text = "Correctivo",
                    Value = "2"
                });
                IEnumerable<SelectListItem> list = db.equipos_fisicos.Select(t => new SelectListItem()
                {
                    Text = t.num_serial,
                    Value = t.num_serial.ToString()
                });
                vmMan.idMan = Man.id;
                vmMan.fechaIniMant = Man.fecha_ini;
                vmMan.fechaFinMant = Man.fecha_fin;
                vmMan.tipoMant = Man.tipo;
                vmMan.equipoFisi = Man.equipo_fisico;
                vmMan.equipos = list;
                vmMan.tipoMantenimiento = tipoMantenimiento;

                return View("Create", vmMan);
            }
            return View("Create", null);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MantenimientosViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                mantenimientos Mant = db.mantenimientos.Find(viewModel.idMan);

                if (Mant != null)
                {

                    Mant.fecha_ini = viewModel.fechaIniMant;
                    Mant.fecha_fin = viewModel.fechaFinMant;
                    Mant.tipo = viewModel.tipoMant;
                    Mant.equipo_fisico = viewModel.equipoFisi;

                    db.SaveChanges();
                    TempData["SusseceMessage"] = "El mantenimiento fue editada con exito";

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
            List<SelectListItem> tipoMantenimiento = new List<SelectListItem>();

            tipoMantenimiento.Add(new SelectListItem()
            {
                Text = "Preventivo",
                Value = "1"
            });

            tipoMantenimiento.Add(new SelectListItem()
            {
                Text = "Correctivo",
                Value = "2"
            });

            IEnumerable<SelectListItem> list = db.equipos_fisicos.Select(t => new SelectListItem()
            {
                Text = t.num_serial,
                Value = t.num_serial.ToString()
            });
            MantenimientosViewModel viewModel = new MantenimientosViewModel ()
            {
                equipos = list,
                fechaFinMant = DateTime.Now,
                fechaIniMant = DateTime.Now,
                tipoMantenimiento = tipoMantenimiento
            };

            ViewBag.Button = "Crear";
            ViewBag.Action = "Create";

            return View(viewModel);
        }

        // GET: BitacoraDeMantenimiento/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MantenimientosViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                mantenimientos mant = new mantenimientos();

                mant.fecha_ini = viewModel.fechaIniMant;
                mant.fecha_fin = viewModel.fechaFinMant;
                mant.tipo = viewModel.tipoMant;
                mant.equipo_fisico = viewModel.equipoFisi;
                //bitaInsta.proc_instalacion = viewModel.proc_instalacion1;
                

                db.mantenimientos.Add(mant);
                db.SaveChanges();

                TempData["successMessage"] = "El mantenimiento fue registrada exitosamente";

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
