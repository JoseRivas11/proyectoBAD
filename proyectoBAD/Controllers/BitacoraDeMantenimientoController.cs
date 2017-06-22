using proyectoBAD.Models;
using proyectoBAD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.Controllers
{
    public class BitacoraDeMantenimientoController : Controller
    {

        //TODO: Poner las anotaciones con respecto a quien debe tener permisos para acceder
        private proyectoBADEntities db = new proyectoBADEntities();

        // GET: BitacoraDeMantenimiento
        [Authorize]
        public ActionResult Index()
        {
            List<BitacoraMantenimientoViewModel> bitacoraMant = db.bitacora_mantenimiento.Select( t => new BitacoraMantenimientoViewModel()
            {
                idMa = t.id,
                detallesMa = t.detalles,
                mantenimientoMa = t.mantenimiento,
                fechaMa = t.fecha,

            }).ToList();
            if (TempData["successMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["successMessage"];
            }
            return View(bitacoraMant);
        }


        // GET: BitacoraDeMantenimiento/Details/5
        /*public ActionResult Details(int id)
        {
            return View();
        }*/
        [Authorize]
        public ActionResult Edit(int id)
        {
            bitacora_mantenimiento bitaMant = db.bitacora_mantenimiento.Find(id);
            ViewBag.Button = "Editar";
            ViewBag.Action = "Edit";

            //ViewBag.PageHeader = "Editar Bitacora de Instalacion";

            if (bitaMant != null)
            {

                BitacoraMantenimientoViewModel vmBitaMante = new BitacoraMantenimientoViewModel();
                IEnumerable<SelectListItem> list = db.mantenimientos.Select(t => new SelectListItem()
                {
                    Text = t.equipo_fisico,
                    Value = t.id.ToString()
                });
                vmBitaMante.idMa = bitaMant.id;
                vmBitaMante.detallesMa = bitaMant.detalles;
                vmBitaMante.mantenimientoMa = bitaMant.mantenimiento;
                vmBitaMante.fechaMa = bitaMant.fecha;
                vmBitaMante.mantenMa = list;

                return View("Create", vmBitaMante);
            }
            return View("Create", null);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BitacoraMantenimientoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bitacora_mantenimiento bitaMant = db.bitacora_mantenimiento.Find(viewModel.idMa);

                if (bitaMant != null)
                {

                    bitaMant.detalles = viewModel.detallesMa;
                    bitaMant.mantenimiento = viewModel.mantenimientoMa;
                    bitaMant.fecha = viewModel.fechaMa;

                    db.SaveChanges();
                    TempData["SusseceMessage"] = "La bitacora fue editada con exito";

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
            IEnumerable<SelectListItem> list = db.mantenimientos.Select(t => new SelectListItem()
            {
                Text = t.equipo_fisico,
                Value = t.id.ToString()
            });
            BitacoraMantenimientoViewModel viewModel = new BitacoraMantenimientoViewModel()
            {
                mantenMa = list,
                fechaMa = DateTime.Now
            };

            ViewBag.Button = "Crear";
            ViewBag.Action = "Create";

            return View(viewModel);
        }

        // GET: BitacoraDeMantenimiento/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BitacoraMantenimientoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bitacora_mantenimiento bitaMant = new bitacora_mantenimiento();

                bitaMant.detalles = viewModel.detallesMa;
                bitaMant.mantenimiento = viewModel.mantenimientoMa;
                bitaMant.fecha = viewModel.fechaMa;


                db.bitacora_mantenimiento.Add(bitaMant);
                db.SaveChanges();

                TempData["successMessage"] = "La bitacora fue registrada exitosamente";

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
