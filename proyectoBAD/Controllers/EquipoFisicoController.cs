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
            List<SelectListItem> estados = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Activa",
                    Value = "1"
                },
                new SelectListItem()
                {
                    Text = "Caducada",
                    Value = "2"
                }
                
            };

            List<SelectListItem> instituciones = db.instituciones.Select(i => new SelectListItem()
            {
                Value = i.id.ToString(),
                Text = i.nombre
            }).ToList();

            List<SelectListItem> empresas = db.empresas.Where(e => e.tipos_empresa.tipo == "Venta" || e.tipos_empresa.tipo == "Venta e Instalacion")
                .Select(e => new SelectListItem()
                {
                    Value = e.id.ToString(),
                    Text = e.nombre,
                }).ToList();

            List<SelectListItem> categorias = db.categorias_equipo.Select(c => new SelectListItem()
            {
                Value = c.id.ToString(),
                Text = c.categoria
            }).ToList();

            EquipoFisicoViewModel vModel = new EquipoFisicoViewModel
            {
                instituciones = instituciones,
                estados_garantia = estados,
                empresas = empresas,
                categorias = categorias
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
        //[Authorize]
        //public ActionResult Edit(string numserial)
        //{
        //    equipos_fisicos equipo_fisico = db.equipos_fisicos.Find(numserial);

        //    ViewBag.Button = "Editar";
        //    ViewBag.Action = "Edit";
        //    ViewBag.PageHeader = "Editar Equipo Físico";

        //    if (equipo_fisico != null)
        //    {
        //        EquipoFisicoViewModel vModel = new EquipoFisicoViewModel();

        //        IEnumerable<SelectListItem> listEq = db.departamentos.Select(t => new SelectListItem()
        //        {
        //            Text = t.nombre,
        //            Value = t.id.ToString()
        //        });
        //        IEnumerable<SelectListItem> listEmp = db.esp_equipos.Select(t => new SelectListItem()
        //        {
        //            Text = t.marca,
        //            Value = t.id.ToString()
        //        });

        //        vModel.numserial = equipo_fisico.num_serial;
        //        vModel.fechafabricacion = equipo_fisico.fecha_fabricacion;
        //        vModel.tiempogarantia = equipo_fisico.tiempo_garantia;
        //        vModel.detallesgarantia = equipo_fisico.detalles_garantia;
        //        vModel.estadogarantia = equipo_fisico.estado_garantia;
        //        vModel.idDepEquiFisico = equipo_fisico.departamento;
        //        vModel.departamentoequipofisico = listEq;
        //        vModel.idEspEquiFisico  = equipo_fisico.esp_equipo;
        //        vModel.especificacionequipofisico = listEmp;
        //        return View("Create", vModel);
        //    }
        //    return View("Create", null);
        //}

        //// POST: EquipoFisico/Edit/5
        //[HttpPost]
        //[Authorize]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(EquipoFisicoViewModel viModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        equipos_fisicos equipo_fisico = db.equipos_fisicos.Find(viModel.numserial);

        //        if (equipo_fisico != null)
        //        {
        //            //equipo_fisico.num_serial = viModel.numserial;
        //            equipo_fisico.fecha_fabricacion = viModel.fechafabricacion;
        //            equipo_fisico.tiempo_garantia = viModel.tiempogarantia;
        //            equipo_fisico.detalles_garantia = viModel.detallesgarantia;
        //            equipo_fisico.estado_garantia = viModel.estadogarantia;
        //            equipo_fisico.departamento = viModel.idDepEquiFisico;
        //            equipo_fisico.esp_equipo = viModel.idEspEquiFisico;
                    
        //            db.SaveChanges();

        //            TempData["successMessage"] = "Equipo Físico editado exitosamente";

        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Error a la hora de guardar los datos, por favor intente mas tarde");
        //        }
        //    }
        //    return View();
        //}

        //// GET: EquipoFisico/Delete/5
        //[Authorize]
        //public ActionResult Delete(string numserial)
        //{
        //    try
        //    {
        //        equipos_fisicos equipoFic = db.equipos_fisicos.Find(numserial);
        //        db.equipos_fisicos.Remove(equipoFic);
        //        db.SaveChanges();
        //        TempData["successMessage"] = "Equipo Físico Eliminado";
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        TempData["successMessage"] = "Error al eliminar proceso de mantenimiento pendiente";
        //        return RedirectToAction("Index");
        //    }


        //}

        [Authorize]
        [HttpPost]
        public JsonResult getDepartamentos(int institucion)
        {
            List<SelectListItem> departamentos = db.departamentos.Where(d => d.institucion == institucion).Select(d => new SelectListItem()
            {
                Value = d.id.ToString(),
                Text = d.nombre
            }).ToList();

            return Json(departamentos);
        }

        [Authorize]
        [HttpPost]
        public JsonResult getEquipos(int categoria)
        {
            List<SelectListItem> equipos = db.equipos.Where(e => e.categoria == categoria).Select(d => new SelectListItem()
            {
                Value = d.id.ToString(),
                Text = d.nombre
            }).ToList();

            return Json(equipos);
        }

        [Authorize]
        [HttpPost]
        public JsonResult getEspEquipos(int empresa, int equipo)
        {
            List<SelectListItem> espEquipos = db.esp_equipos.Where(e => e.empresa == empresa && e.equipo == equipo).Select(d => new SelectListItem()
            {
                Value = d.id.ToString(),
                Text = d.marca + " " + d.modelo
            }).ToList();

            return Json(espEquipos);
        }
    }
}
