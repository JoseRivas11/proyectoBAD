using proyectoBAD.Models;
using proyectoBAD.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.Controllers
{
    public class SolicitudController : Controller
    {
        //TODO: Poner las anotaciones con respecto a quien debe tener permisos para acceder
        private proyectoBADEntities db = new proyectoBADEntities();
        // GET: Solicitud
        [Authorize]
        public ActionResult Index()
        {
            List<SolicitudesViewModel> solicitud = db.solicitudes.Select(t => new SolicitudesViewModel()
            {
                ID = t.id,
                FECHA_REGISTRO = t.fecha_registro,
                DEPARTAMENTO = t.departamentos,
                ESTADO_STRING = t.estado == 1 ? "Urgente" : "Normal"

            }).ToList();
            if (TempData["successMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["successMessage"];
            }
            return View(solicitud);
        }

        [Authorize]
        public ActionResult Create()
        {
            List<SelectListItem> tipo_estado = new List<SelectListItem>();

            tipo_estado.Add(new SelectListItem()
            {
                Text = "Urgente",
                Value = "1"
            });

            tipo_estado.Add(new SelectListItem()
            {
                Text = "Normal",
                Value = "2"
            });

            List<SelectListItem> instituciones = db.instituciones.Select(item => new SelectListItem()
            {
                Text = item.nombre,
                Value = item.id.ToString()
            }).ToList();

            List<SelectListItem> departamentos = db.departamentos.Select(item => new SelectListItem()
            {
                Text = item.nombre,
                Value = item.id.ToString()
            }).ToList();

            List<SelectListItem> equipos = db.equipos.Select(item => new SelectListItem()
            {
                Text = item.nombre,
                Value = item.id.ToString()
            }).ToList();

            SolicitudesViewModel vModel = new SolicitudesViewModel()
            {
                DEPARTAMENTOS = departamentos,
                INSTITUCIONES = instituciones,
                tipo_estado = tipo_estado,
                EQUIPOS = equipos,
                EQUIPOS_SOLICITUD = new List<equipos_solicitud>(),
                FECHA_REGISTRO = DateTime.Now

            };
            ViewBag.Button = "Crear";
            ViewBag.Action = "Create";
            ViewBag.PageHeader = "Crear Solicitud";

            return View(vModel);
        }

        [Authorize]
        [HttpPost]
        public JsonResult getInfoEquipo(int idEquipo)
        {
            /* var entity = db.equipos.Where(t => t.id == idEquipo).Select(
                 e => new
                 {
                     cat = e.categoria,
                     esp = e.especificaciones
                 }
                 );*/

            var entidad = (from e in db.equipos
                           join ce in db.categorias_equipo on e.categoria equals ce.id
                           where e.id == idEquipo
                           select new
                           {
                               cat = ce.categoria,
                               idequipo = e.id,
                               esp = e.especificaciones
                           }).Take(10);
            return Json(entidad);
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

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(SolicitudesViewModel vModel)
        {
            if (ModelState.IsValid)
            {
                solicitudes solicitud = new solicitudes()
                {
                    fecha_registro = vModel.FECHA_REGISTRO,
                    departamento = vModel.IDDEPARTAMENTO,
                    estado = vModel.ESTADO
                };

                for (int i = 0; i < vModel.EQUIPOS_SOLICITUD.Count(); i++)
                {
                    db.equipos.Attach(vModel.EQUIPOS_SOLICITUD[i].equipos);
                    solicitud.equipos_solicitud.Add(vModel.EQUIPOS_SOLICITUD[i]);
                }
                db.solicitudes.Add(solicitud);
                try
                {
                    db.SaveChanges();
                }
                catch (DataException ex)
                {
                    if (ex.InnerException.InnerException is SqlException)
                    {
                        SqlException e = (SqlException)ex.InnerException.InnerException;
                        if (e.Procedure == "max_equipos")
                        {
                            ModelState.AddModelError("", ex.InnerException.InnerException.Message);
                        }
                    }
                }
                TempData["successMessage"] = "Solicitud creada exitosamente";

                return RedirectToAction("Index");


            }
            ViewBag.Button = "Crear";
            ViewBag.Action = "Create";
            ViewBag.PageHeader = "Crear Solicitud";

            List<SelectListItem> tipo_estado = new List<SelectListItem>();

            tipo_estado.Add(new SelectListItem()
            {
                Text = "Urgente",
                Value = "1"
            });

            tipo_estado.Add(new SelectListItem()
            {
                Text = "Normal",
                Value = "2"
            });

            List<SelectListItem> departamentos = db.departamentos.Select(item => new SelectListItem()
            {
                Text = item.nombre,
                Value = item.id.ToString()
            }).ToList();

            List<SelectListItem> equipos = db.equipos.Select(item => new SelectListItem()
            {
                Text = item.nombre,
                Value = item.id.ToString()
            }).ToList();

            SolicitudesViewModel vmModel = new SolicitudesViewModel()
            {
                DEPARTAMENTOS = departamentos,
                tipo_estado = tipo_estado,
                EQUIPOS = equipos,
                EQUIPOS_SOLICITUD = new List<equipos_solicitud>(),
                FECHA_REGISTRO = DateTime.Now
            };
            return View(vmModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            solicitudes solicitud = db.solicitudes.Find(id);
            List<SelectListItem> tipo_estado = new List<SelectListItem>();

            tipo_estado.Add(new SelectListItem()
            {
                Text = "Urgente",
                Value = "1"
            });

            tipo_estado.Add(new SelectListItem()
            {
                Text = "Normal",
                Value = "2"
            });

            List<SelectListItem> instituciones = db.instituciones.Select(item => new SelectListItem()
            {
                Text = item.nombre,
                Value = item.id.ToString()
            }).ToList();

            List<SelectListItem> departamentos = db.departamentos.Select(item => new SelectListItem()
            {
                Text = item.nombre,
                Value = item.id.ToString()
            }).ToList();

            List<SelectListItem> equipos = db.equipos.Select(item => new SelectListItem()
            {
                Text = item.nombre,
                Value = item.id.ToString()
            }).ToList();

            SolicitudesViewModel vModel = new SolicitudesViewModel()
            {
                ID = solicitud.id,
                INSTITUCIONES = instituciones,
                DEPARTAMENTOS = departamentos,
                IDDEPARTAMENTO = solicitud.departamento,
                ESTADO = solicitud.estado,
                tipo_estado = tipo_estado,
                EQUIPOS = equipos,
                EQUIPOS_SOLICITUD = solicitud.equipos_solicitud.ToList(),
                FECHA_REGISTRO = solicitud.fecha_registro
            };
            ViewBag.Button = "Editar";
            ViewBag.Action = "Edit";
            ViewBag.PageHeader = "Editar Solitud";

            return View("Create", vModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SolicitudesViewModel vModel)
        {
            solicitudes solicitud = db.solicitudes.Find(vModel.ID);
            ViewBag.Button = "Editar";
            ViewBag.Action = "Edit";
            ViewBag.PageHeader = "Editar Solicitud";

            if (ModelState.IsValid)
            {
                solicitud.fecha_registro = vModel.FECHA_REGISTRO;
                solicitud.departamento = vModel.IDDEPARTAMENTO;
                solicitud.estado = vModel.ESTADO;

                foreach (var equipo_solicitud in solicitud.equipos_solicitud.ToList())
                {
                    if (!vModel.EQUIPOS_SOLICITUD.Any(es => es.cantidad == equipo_solicitud.cantidad && es.equipos.id == equipo_solicitud.equipos.id))
                    {
                        solicitud.equipos_solicitud.Remove(equipo_solicitud);
                    }
                }
                foreach (var equipo_solicitud in vModel.EQUIPOS_SOLICITUD)
                {
                    if (!solicitud.equipos_solicitud.Any(es => es.cantidad == equipo_solicitud.cantidad && es.equipos.id == equipo_solicitud.equipos.id))
                    {
                        db.equipos.Attach(equipo_solicitud.equipos);
                        solicitud.equipos_solicitud.Add(equipo_solicitud);
                    }
                }
                try
                {
                    db.SaveChanges();

                }
                catch (DataException ex)
                {
                    if (ex.InnerException.InnerException is SqlException)
                    {
                        SqlException e = (SqlException)ex.InnerException.InnerException;
                        if (e.Procedure == "max_equipos")
                        {
                            ModelState.AddModelError("", ex.InnerException.InnerException.Message);
                        }
                    }
                }
                TempData["successMessage"] = "solicitud editada exitosamente";

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Los datos no son validos");
            }

            ViewBag.Button = "Crear";
            ViewBag.Action = "Create";
            ViewBag.PageHeader = "Crear Solicitud";

            List<SelectListItem> tipo_estado = new List<SelectListItem>();

            tipo_estado.Add(new SelectListItem()
            {
                Text = "Urgente",
                Value = "1"
            });

            tipo_estado.Add(new SelectListItem()
            {
                Text = "Normal",
                Value = "2"
            });

            List<SelectListItem> departamentos = db.departamentos.Select(item => new SelectListItem()
            {
                Text = item.nombre,
                Value = item.id.ToString()
            }).ToList();

            List<SelectListItem> equipos = db.equipos.Select(item => new SelectListItem()
            {
                Text = item.nombre,
                Value = item.id.ToString()
            }).ToList();

            SolicitudesViewModel vmNew = new SolicitudesViewModel()
            {
                DEPARTAMENTOS = departamentos,
                EQUIPOS = equipos,
                EQUIPOS_SOLICITUD = new List<equipos_solicitud>(),
                FECHA_REGISTRO = DateTime.Now
            };
            return View(vmNew);
        }


        [Authorize]
        public ActionResult Details(int id)
        {
            solicitudes solicitud = db.solicitudes.Find(id);

            SolicitudesViewModel vModel = new SolicitudesViewModel()
            {
                FECHA_REGISTRO = solicitud.fecha_registro,
                DEPARTAMENTO = solicitud.departamentos,
                ESTADO_STRING = solicitud.estado == 1 ? "Urgente" : "Normal",
                EQUIPOS_SOLICITUD = solicitud.equipos_solicitud.ToList()
            };           
            
            return View(vModel);
        }
    }
}