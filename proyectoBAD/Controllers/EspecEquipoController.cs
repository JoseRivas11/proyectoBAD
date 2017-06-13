using proyectoBAD.Models;
using proyectoBAD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.Controllers
{
    public class EspecEquipoController : Controller
    {
        private proyectoBADEntities db = new proyectoBADEntities();
        // GET: EspecEquipo
        [Authorize]
        public ActionResult Index()
        {
            List<EspecEquipoViewModel> especEquipos = db.esp_equipos.Select(t => new EspecEquipoViewModel()
            {
                IDESPEQ = t.id,
                MARCAESPEQ = t.marca,
                MODELESPEQ = t.modelo,
                POTENCIAESPEQ = t.potencia,
                CONSCORRESPEQ = t.consumo_corriente,
                CAPBTUESPEQ = t.capacidad_btu,
                IDEQUIPO = t.equipos,
                IDEMPRESA = t.empresas,
                PRECIOESPEQ = t.precio
            }
                ).ToList();
            if (TempData["successMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["successMessage"];
            }
            return View(especEquipos);
        }
        [Authorize]
        public ActionResult Create()
        {
            IEnumerable<SelectListItem> listEq = db.equipos.Select(t => new SelectListItem()
            {
                Text = t.nombre,
                Value = t.id.ToString()
            });
            IEnumerable<SelectListItem> listEmp = db.empresas.Select(t => new SelectListItem()
            {
                Text = t.nombre,
                Value = t.id.ToString()
            });

            EspecEquipoViewModel vModel = new EspecEquipoViewModel
            {
                equiposEsp = listEq,
                empresasEsp = listEmp
            };
            ViewBag.Button = "Agregar";
            ViewBag.Action = "Create";
            //ViewBag.PageHeader = "Crear Equipo";

            return View(vModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EspecEquipoViewModel vModel)
        {
            if (ModelState.IsValid)
            {
                esp_equipos esp_equipo = new esp_equipos();

                esp_equipo.marca = vModel.MARCAESPEQ;
                esp_equipo.modelo = vModel.MODELESPEQ;
                esp_equipo.potencia = vModel.POTENCIAESPEQ;
                esp_equipo.consumo_corriente = vModel.CONSCORRESPEQ;
                esp_equipo.capacidad_btu = vModel.CAPBTUESPEQ;
                esp_equipo.equipo = vModel.IDEQESPEQ;
                esp_equipo.empresa = vModel.IDEMPESPEQ;
                esp_equipo.precio = vModel.PRECIOESPEQ;

                db.esp_equipos.Add(esp_equipo);
                db.SaveChanges();

                TempData["successMessage"] = "Especificación creado exitosamente";

                return RedirectToAction("Index");
            }
            return View();
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            esp_equipos esp_equipo = db.esp_equipos.Find(id);

            ViewBag.Button = "Editar";
            ViewBag.Action = "Edit";
            ViewBag.PageHeader = "Editar Especificación";

            if (esp_equipo != null)
            {
                EspecEquipoViewModel vModel = new EspecEquipoViewModel();
                IEnumerable<SelectListItem> listEq = db.equipos.Select(t => new SelectListItem()
                {
                    Text = t.nombre,
                    Value = t.id.ToString()
                });
                IEnumerable<SelectListItem> listEmp = db.empresas.Select(t => new SelectListItem()
                {
                    Text = t.nombre,
                    Value = t.id.ToString()
                });
                vModel.IDESPEQ = esp_equipo.id;
                vModel.MARCAESPEQ = esp_equipo.marca;
                vModel.MODELESPEQ = esp_equipo.modelo;
                vModel.POTENCIAESPEQ = esp_equipo.potencia;
                vModel.CONSCORRESPEQ = esp_equipo.consumo_corriente;
                vModel.CAPBTUESPEQ = esp_equipo.capacidad_btu;
                vModel.IDEQESPEQ = esp_equipo.equipo;
                vModel.equiposEsp = listEq;
                vModel.IDEMPESPEQ = esp_equipo.empresa;
                vModel.empresasEsp = listEmp;
                vModel.PRECIOESPEQ = esp_equipo.precio;

                return View("Create", vModel);
            }
            return View("Create", null);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EspecEquipoViewModel vModel)
        {
            if (ModelState.IsValid)
            {
                esp_equipos esp_equipo = db.esp_equipos.Find(vModel.IDESPEQ);

                if (esp_equipo != null)
                {
                    esp_equipo.marca = vModel.MARCAESPEQ;
                    esp_equipo.modelo = vModel.MODELESPEQ;
                    esp_equipo.potencia = vModel.POTENCIAESPEQ;
                    esp_equipo.consumo_corriente = vModel.CONSCORRESPEQ;
                    esp_equipo.capacidad_btu = vModel.CAPBTUESPEQ;
                    esp_equipo.equipo = vModel.IDEQESPEQ;
                    esp_equipo.empresa = vModel.IDEMPESPEQ;
                    esp_equipo.precio = vModel.PRECIOESPEQ;

                    db.SaveChanges();

                    TempData["successMessage"] = "Especificación editada exitosamente";

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error a la hora de guardar los datos, por favor intente mas tarde");
                }
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            try
            {
                esp_equipos especEq = db.esp_equipos.Find(id);
                db.esp_equipos.Remove(especEq);
                db.SaveChanges();
                TempData["successMessage"] = "Especificación Eliminada";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["successMessage"] = "Error al eliminar la categoria Revise la Adm. de Linea de compra";
                return RedirectToAction("Index");
            }


        }

    }
}