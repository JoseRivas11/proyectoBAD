using proyectoBAD.Models;
using proyectoBAD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.Controllers
{
    public class BitacoraInstalacionController : Controller
    {
        //TODO: Poner las anotaciones con respecto a quien debe tener permisos para acceder
        private proyectoBADEntities db = new proyectoBADEntities();

        // GET: BitacoraDeMantenimiento
        [Authorize]
        public ActionResult Index()
        {
            List<BitacoraInstalacionViewModel> bitacoraInstalacion = db.bitacora_instalacion.Select(t => new BitacoraInstalacionViewModel()
            {
                id1 = t.id,                                
                fecha1 = t.fecha,
                detalles1 = t.detalles,
                proc_instalacion1= t.proc_instalacion,
                
            }).ToList();
            if (TempData["successMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["successMessage"];
            }
            return View(bitacoraInstalacion);
        }


        // GET: BitacoraDeMantenimiento/Details/5
        /*public ActionResult Details(int id)
        {
            return View();
        }*/
        [Authorize]
        public ActionResult Edit(int id)
        {
            bitacora_instalacion bitaInst = db.bitacora_instalacion.Find(id);
            ViewBag.Button = "Editar";
            ViewBag.Action = "Edit";

            //ViewBag.PageHeader = "Editar Bitacora de Instalacion";

            if (bitaInst != null)
            {

                BitacoraInstalacionViewModel vmBitaInsta = new BitacoraInstalacionViewModel();
                IEnumerable<SelectListItem> list = db.proc_instalacion.Select(t => new SelectListItem()
                {
                    Text = t.equipo,
                    Value = t.id.ToString()
                });
                vmBitaInsta.id1 = bitaInst.id;
                vmBitaInsta.fecha1 = bitaInst.fecha;
                vmBitaInsta.detalles1 = bitaInst.detalles;
                vmBitaInsta.proc_instalacion1 = bitaInst.proc_instalacion;
                vmBitaInsta.proc_insta = list;

                return View("Create", vmBitaInsta);
            }
            return View("Create", null);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BitacoraInstalacionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bitacora_instalacion bitaInst = db.bitacora_instalacion.Find(viewModel.id1);

                if (bitaInst != null)
                {

                    bitaInst.fecha = viewModel.fecha1;
                    bitaInst.detalles = viewModel.detalles1;
                    bitaInst.proc_instalacion = viewModel.proc_instalacion1;

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
            IEnumerable<SelectListItem> list = db.proc_instalacion.Select(t => new SelectListItem()
            {
                Text = t.equipo,
                Value = t.id.ToString()
            });
            BitacoraInstalacionViewModel viewModel = new BitacoraInstalacionViewModel ()
            {
                proc_insta = list
            };

            ViewBag.Button = "Crear";
            ViewBag.Action = "Create";

            return View(viewModel);
        }

        // GET: BitacoraDeMantenimiento/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BitacoraInstalacionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                bitacora_instalacion bitaInsta = new bitacora_instalacion();

                bitaInsta.fecha = viewModel.fecha1;
                bitaInsta.detalles = viewModel.detalles1;
                bitaInsta.proc_instalacion = viewModel.proc_instalacion1;
                

                db.bitacora_instalacion.Add(bitaInsta);
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
