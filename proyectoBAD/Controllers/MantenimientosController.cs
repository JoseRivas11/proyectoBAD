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
                //fechaIniMant = t.fecha_ini,
                //fechaFinMant = t.fecha_fin,
                tipoMant= t.tipo,
                equipoFisi = t.equipo_fisico,
                
            }).ToList();
            if (TempData["successMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["successMessage"];
            }
            return View(mantes);
        }


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
                //vmMan.fechaIniMant = Man.fecha_ini;
                //vmMan.fechaFinMant = Man.fecha_fin;
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

            List<SelectListItem> instituciones = db.instituciones.Select(item => new SelectListItem()
            {
                Text = item.nombre,
                Value = item.id.ToString()
            }).ToList();

            IEnumerable<SelectListItem> list = db.equipos_fisicos.Select(t => new SelectListItem()
            {
                Text = t.num_serial,
                Value = t.num_serial.ToString()
            });
            MantenimientosViewModel viewModel = new MantenimientosViewModel ()
            {
                equipos = list,
                Instituciones = instituciones,
                fechaFinMant = DateTime.Now,
                fechaIniMant = DateTime.Now,
                tipoMantenimiento = tipoMantenimiento
            };

            ViewBag.Button = "Crear";
            ViewBag.Action = "Create";

            return View(viewModel);
        }

        public JsonResult getDepartamento(int idInt)
        {
            List<SelectListItem> depart = db.departamentos.Where(t => t.institucion == idInt).Select(
               t => new SelectListItem()
               {
                   Text = t.nombre,
                   Value = t.id.ToString()
               }).ToList();
            return Json(depart, JsonRequestBehavior.AllowGet);
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
