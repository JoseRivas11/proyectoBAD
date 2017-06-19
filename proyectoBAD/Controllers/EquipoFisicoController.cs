using proyectoBAD.Models;
using proyectoBAD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.Controllers
{
    public class EquipoFisicoController : Controller
    {
        private proyectoBADEntities db = new proyectoBADEntities();

        // GET: EquipoFisico
        [Authorize]
        public ActionResult Index()
              {
            List<EquipoFisicoViewModel> equipoFisicos = db.equipos_fisicos.Select(t => new EquipoFisicoViewModel()
            {
                numserial = t.num_serial,
                fechafabricacion = t.fecha_fabricacion,
                tiempogarantia = t.tiempo_garantia,
                detallesgarantia = t.detalles_garantia,
                estadogarantia = t.estado_garantia,
                iddepartamento = t.departamentos,
                idEspequifi = t.esp_equipos
            }).ToList();

            if (TempData["successMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["successMessage"];
            }
            return View(equipoFisicos);
        }

        [Authorize]
        public ActionResult Create()
        {
            IEnumerable<SelectListItem> listEq = db.departamentos.Select(t => new SelectListItem()
            {
                Text = t.nombre,
                Value = t.id.ToString()
            });
            IEnumerable<SelectListItem> listEmp = db.esp_equipos.Select(t => new SelectListItem()
            {
                Text = t.marca,
                Value = t.id.ToString()
            });

            EquipoFisicoViewModel vModel = new EquipoFisicoViewModel
            {
                departamentoequipofisico = listEq,
                especificacionequipofisico = listEmp
            };

            ViewBag.Button = "Agregar";
            ViewBag.Action = "Create";
           // ViewBag.PageHeader = "Crear Equipo";

            return View(vModel);
        }

        // POST: EquipoFisico/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EquipoFisicoViewModel vModel)
        {
            if (ModelState.IsValid)
            {
                equipos_fisicos equipo_fisico = new equipos_fisicos();

                equipo_fisico.num_serial = vModel.numserial;
                equipo_fisico.fecha_fabricacion = vModel.fechafabricacion;
                equipo_fisico.tiempo_garantia = vModel.tiempogarantia;
                equipo_fisico.detalles_garantia = vModel.detallesgarantia;
                equipo_fisico.estado_garantia = vModel.estadogarantia;
                equipo_fisico.departamento = vModel.idDepEquiFisico;
                equipo_fisico.esp_equipo = vModel.idEspEquiFisico;
                
                db.equipos_fisicos.Add(equipo_fisico);
                db.SaveChanges();

                TempData["successMessage"] = "Equipo Físico creado exitosamente";

                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: EquipoFisico/Edit/5
        [Authorize]
        public ActionResult Edit(string numserial)
        {
            equipos_fisicos equipo_fisico = db.equipos_fisicos.Find(numserial);

            ViewBag.Button = "Editar";
            ViewBag.Action = "Edit";
            ViewBag.PageHeader = "Editar Equipo Físico";

            if (equipo_fisico != null)
            {
                EquipoFisicoViewModel vModel = new EquipoFisicoViewModel();

                IEnumerable<SelectListItem> listEq = db.departamentos.Select(t => new SelectListItem()
                {
                    Text = t.nombre,
                    Value = t.id.ToString()
                });
                IEnumerable<SelectListItem> listEmp = db.esp_equipos.Select(t => new SelectListItem()
                {
                    Text = t.marca,
                    Value = t.id.ToString()
                });

                vModel.numserial = equipo_fisico.num_serial;
                vModel.fechafabricacion = equipo_fisico.fecha_fabricacion;
                vModel.tiempogarantia = equipo_fisico.tiempo_garantia;
                vModel.detallesgarantia = equipo_fisico.detalles_garantia;
                vModel.estadogarantia = equipo_fisico.estado_garantia;
                vModel.idDepEquiFisico = equipo_fisico.departamento;
                vModel.departamentoequipofisico = listEq;
                vModel.idEspEquiFisico  = equipo_fisico.esp_equipo;
                vModel.especificacionequipofisico = listEmp;
                return View("Create", vModel);
            }
            return View("Create", null);
        }

        // POST: EquipoFisico/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EquipoFisicoViewModel viModel)
        {
            if (ModelState.IsValid)
            {
                equipos_fisicos equipo_fisico = db.equipos_fisicos.Find(viModel.numserial);

                if (equipo_fisico != null)
                {
                    //equipo_fisico.num_serial = viModel.numserial;
                    equipo_fisico.fecha_fabricacion = viModel.fechafabricacion;
                    equipo_fisico.tiempo_garantia = viModel.tiempogarantia;
                    equipo_fisico.detalles_garantia = viModel.detallesgarantia;
                    equipo_fisico.estado_garantia = viModel.estadogarantia;
                    equipo_fisico.departamento = viModel.idDepEquiFisico;
                    equipo_fisico.esp_equipo = viModel.idEspEquiFisico;
                    
                    db.SaveChanges();

                    TempData["successMessage"] = "Equipo Físico editado exitosamente";

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error a la hora de guardar los datos, por favor intente mas tarde");
                }
            }
            return View();
        }

        // GET: EquipoFisico/Delete/5
        [Authorize]
        public ActionResult Delete(string numserial)
        {
            try
            {
                equipos_fisicos equipoFic = db.equipos_fisicos.Find(numserial);
                db.equipos_fisicos.Remove(equipoFic);
                db.SaveChanges();
                TempData["successMessage"] = "Equipo Físico Eliminado";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["successMessage"] = "Error al eliminar proceso de mantenimiento pendiente";
                return RedirectToAction("Index");
            }


        }
    }
}
