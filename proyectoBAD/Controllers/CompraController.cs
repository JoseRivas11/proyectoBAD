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
    public class CompraController : Controller
    {
        private proyectoBADEntities db = new proyectoBADEntities();

        // GET: Compra
        [Authorize]
        public ActionResult Index()
        {
            List<ComprasViewModel> compras = db.compras.Select(t => new ComprasViewModel()
            {
                id = t.id,
                fecha = t.fecha,
                tipo_contratacion_string = t.tipo_contratacion == 1 ? "Licitación" : "Compra directa",
                institucion = t.instituciones,
                total_compra = t.linea_compra.Sum(lc => lc.cantidad * lc.esp_equipos.precio)
            }).ToList();

            if (TempData["successMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["successMessage"];
            }

            return View(compras);
        }

        [Authorize]
        public ActionResult Create()
        {
            List<SelectListItem> tipos_compra = new List<SelectListItem>();

            tipos_compra.Add(new SelectListItem()
            {
                Text = "Licitación",
                Value = "1"
            });

            tipos_compra.Add(new SelectListItem()
            {
                Text = "Compra Directa",
                Value = "2"
            });

            List<SelectListItem> instituciones = db.instituciones.Select(item => new SelectListItem()
            {
                Text = item.nombre,
                Value = item.id.ToString()
            }).ToList();

            List<SelectListItem> cat_equipos = db.categorias_equipo.Select(item => new SelectListItem()
            {
                Text = item.categoria,
                Value = item.id.ToString()
            }).ToList();

            List<SelectListItem> proveedores = db.empresas.Where(r => r.tipos_empresa.tipo == "Venta" ||
               r.tipos_empresa.tipo == "Venta e Instalación").Select(r => new SelectListItem()
               {
                   Text = r.nombre,
                   Value = r.id.ToString()
               }).ToList();

            ComprasViewModel viewModel = new ComprasViewModel()
            {
                instituciones = instituciones,
                tipos_compra = tipos_compra,
                lineas_compra = new List<linea_compra>(),
                tipos_equipo = cat_equipos,
                proveedores = proveedores,
                fecha = DateTime.Now
            };

            ViewBag.Button = "Crear";
            ViewBag.Action = "Create";
            ViewBag.PageHeader = "Crear Compra";

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public JsonResult getEquipos(int idEmpresa, int idCat)
        {
            List<SelectListItem> esp_equipos = db.esp_equipos.Where(t => t.empresas.id == idEmpresa
            && t.equipos.categorias_equipo.id == idCat).Select(t => new SelectListItem()
            {
                Text = t.equipos.nombre,
                Value = t.id.ToString()
            }).ToList();

            return Json(esp_equipos);
        }
        

        [Authorize]
        [HttpPost]
        public JsonResult getInfoEquipo(int idEquipo)
        {
            var entity = db.esp_equipos.Where(t => t.id == idEquipo).Select(
                e => new
                {
                    precio = e.precio,
                    marca = e.marca,
                    modelo = e.modelo
                }
                );   
            return Json(entity);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(ComprasViewModel viewModel)
        {
            if (viewModel.lineas_compra != null && viewModel.lineas_compra.Count > 0)
            {
                if (ModelState.IsValid)
                {
                    compras compra = new compras()
                    {
                        fecha = viewModel.fecha,
                        institucion = viewModel.idInstitucion,
                        tipo_contratacion = viewModel.tipo_contratacion,
                        total_compra = 0.00M
                    };

                    for (int i = 0; i < viewModel.lineas_compra.Count(); i++)
                    {
                        db.esp_equipos.Attach(viewModel.lineas_compra[i].esp_equipos);
                        compra.linea_compra.Add(viewModel.lineas_compra[i]);
                    }
                    db.compras.Add(compra);

                    try
                    {
                        db.SaveChanges();

                    }
                    catch (DataException ex)
                    {
                        if (ex.InnerException.InnerException is SqlException)
                        {
                            SqlException e = (SqlException)ex.InnerException.InnerException;
                            if (e.Procedure == "max_compras")
                            {
                                ModelState.AddModelError("", ex.InnerException.InnerException.Message);
                            }
                        }
                    }
                    TempData["successMessage"] = "Compra creada exitosamente";

                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Por favor ingrese al menos una linea de compra");
            }
            ViewBag.Button = "Crear";
            ViewBag.Action = "Create";
            ViewBag.PageHeader = "Crear Compra";

            List<SelectListItem> tipos_compra = new List<SelectListItem>();

            tipos_compra.Add(new SelectListItem()
            {
                Text = "Licitación",
                Value = "1"
            });

            tipos_compra.Add(new SelectListItem()
            {
                Text = "Compra Directa",
                Value = "2"
            });

            List<SelectListItem> instituciones = db.instituciones.Select(item => new SelectListItem()
            {
                Text = item.nombre,
                Value = item.id.ToString()
            }).ToList();

            List<SelectListItem> cat_equipos = db.categorias_equipo.Select(item => new SelectListItem()
            {
                Text = item.categoria,
                Value = item.id.ToString()
            }).ToList();

            List<SelectListItem> proveedores = db.empresas.Where(r => r.tipos_empresa.tipo == "Venta" ||
               r.tipos_empresa.tipo == "Venta e Instalación").Select(r => new SelectListItem()
               {
                   Text = r.nombre,
                   Value = r.id.ToString()
               }).ToList();

            ComprasViewModel vmNew = new ComprasViewModel()
            {
                instituciones = instituciones,
                tipos_compra = tipos_compra,
                lineas_compra = new List<linea_compra>(),
                tipos_equipo = cat_equipos,
                proveedores = proveedores,
                fecha = DateTime.Now
            };
            return View(vmNew);
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            compras compra = db.compras.Find(id);

            ComprasViewModel viewModel = new ComprasViewModel()
            {
               fecha = compra.fecha,
               tipo_contratacion_string = compra.tipo_contratacion == 1 ? "Licitación" : "Compra Directa",
               institucion = compra.instituciones,
               total_compra = compra.linea_compra.Sum(lc => lc.cantidad * lc.esp_equipos.precio),
               lineas_compra = compra.linea_compra.ToList()
            };
            return View(viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            compras compra = db.compras.Find(id);
            List<SelectListItem> tipos_compra = new List<SelectListItem>();

            tipos_compra.Add(new SelectListItem()
            {
                Text = "Licitación",
                Value = "1"
            });

            tipos_compra.Add(new SelectListItem()
            {
                Text = "Compra Directa",
                Value = "2"
            });

            List<SelectListItem> instituciones = db.instituciones.Select(item => new SelectListItem()
            {
                Text = item.nombre,
                Value = item.id.ToString()
            }).ToList();

            List<SelectListItem> cat_equipos = db.categorias_equipo.Select(item => new SelectListItem()
            {
                Text = item.categoria,
                Value = item.id.ToString()
            }).ToList();

            List<SelectListItem> proveedores = db.empresas.Where(r => r.tipos_empresa.tipo == "Venta" ||
               r.tipos_empresa.tipo == "Venta e Instalación").Select(r => new SelectListItem()
               {
                   Text = r.nombre,
                   Value = r.id.ToString()
               }).ToList();

            ComprasViewModel viewModel = new ComprasViewModel()
            {
                id = compra.id,
                lineas_compra = compra.linea_compra.ToList(),
                tipo_contratacion = compra.tipo_contratacion,
                idInstitucion = compra.institucion,
                instituciones = instituciones,
                tipos_compra = tipos_compra,
                tipos_equipo = cat_equipos,
                proveedores = proveedores,
                fecha = compra.fecha
            };

            ViewBag.Button = "Editar";
            ViewBag.Action = "Edit";
            ViewBag.PageHeader = "Editar Compra";

            return View("Create" ,viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ComprasViewModel viewModel)
        {
            compras compra = db.compras.Find(viewModel.id);
            ViewBag.Button = "Editar";
            ViewBag.Action = "Edit";
            ViewBag.PageHeader = "Editar Compra";

            if (viewModel.lineas_compra != null && viewModel.lineas_compra.Count > 0)
            {
                if (ModelState.IsValid)
                {
                    compra.fecha = viewModel.fecha;
                    compra.institucion = viewModel.idInstitucion;
                    compra.tipo_contratacion = viewModel.tipo_contratacion;

                    foreach (var linea_compra in compra.linea_compra.ToList())
                    {
                        if (!viewModel.lineas_compra.Any(lc => lc.cantidad == linea_compra.cantidad &&
                        lc.esp_equipos.id == linea_compra.esp_equipos.id))
                        {
                            compra.linea_compra.Remove(linea_compra);
                        }
                    }

                    foreach( var linea_compra in viewModel.lineas_compra)
                    {
                        if (!compra.linea_compra.Any(lc => lc.cantidad == linea_compra.cantidad &&
                        lc.esp_equipos.id == linea_compra.esp_equipos.id))
                        {
                            db.esp_equipos.Attach(linea_compra.esp_equipos);
                            compra.linea_compra.Add(linea_compra);
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
                            if (e.Procedure == "max_compras")
                            {
                                ModelState.AddModelError("", ex.InnerException.InnerException.Message);
                            }
                        }
                    }
                    TempData["successMessage"] = "Compra creada exitosamente";

                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("", "Por favor ingrese al menos una linea de compra");
            }
            ViewBag.Button = "Crear";
            ViewBag.Action = "Create";
            ViewBag.PageHeader = "Crear Compra";

            List<SelectListItem> tipos_compra = new List<SelectListItem>();

            tipos_compra.Add(new SelectListItem()
            {
                Text = "Licitación",
                Value = "1"
            });

            tipos_compra.Add(new SelectListItem()
            {
                Text = "Compra Directa",
                Value = "2"
            });

            List<SelectListItem> instituciones = db.instituciones.Select(item => new SelectListItem()
            {
                Text = item.nombre,
                Value = item.id.ToString()
            }).ToList();

            List<SelectListItem> cat_equipos = db.categorias_equipo.Select(item => new SelectListItem()
            {
                Text = item.categoria,
                Value = item.id.ToString()
            }).ToList();

            List<SelectListItem> proveedores = db.empresas.Where(r => r.tipos_empresa.tipo == "Venta" ||
               r.tipos_empresa.tipo == "Venta e Instalación").Select(r => new SelectListItem()
               {
                   Text = r.nombre,
                   Value = r.id.ToString()
               }).ToList();

            ComprasViewModel vmNew = new ComprasViewModel()
            {
                instituciones = instituciones,
                tipos_compra = tipos_compra,
                lineas_compra = new List<linea_compra>(),
                tipos_equipo = cat_equipos,
                proveedores = proveedores,
                fecha = DateTime.Now
            };
            return View("Create", vmNew);
        }
    }
}