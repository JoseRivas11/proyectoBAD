using proyectoBAD.Models;
using proyectoBAD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.Controllers
{
    public class UsuarioController : Controller
    {
        private proyectoBADEntities db = new proyectoBADEntities();

        // GET: Usuario
        [Authorize]
        public ActionResult Index()
        {
            List<UsuarioViewModel> usuarios = db.usuarios.Select(u => new UsuarioViewModel()
            {
                email = u.email,
                perfil = u.perfiles_usuarios.FirstOrDefault(p => p.estado == 1),
                strEstado = u.estado == 0 ? "Inactivo" : "Activo",
                nombre_completo = u.nombre_completo,
                empresa = u.empresas,
                departamento = u.departamentos,
                id = u.id
            }).ToList();

            if (TempData["successMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["successMessage"];
            }

            return View(usuarios);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            usuarios user = db.usuarios.FirstOrDefault(u => u.id == id);

            if (user != null)
            {
                UsuarioViewModel usuario = new UsuarioViewModel()
                {
                    email = user.email,
                    nombre_completo = user.nombre_completo,
                    estado = user.estado == 0 ? (int?)null : user.estado,

                    perfil = user.perfiles_usuarios.FirstOrDefault(p => p.estado == 1),
                    listInstituciones = db.instituciones.Select(i => new SelectListItem()
                    {
                        Value = i.id.ToString(),
                        Text = i.nombre
                    }).ToList(),
                    listEmpresa = db.empresas.Select(e => new SelectListItem()
                    {
                        Value = e.id.ToString(),
                        Text = e.nombre
                    }).ToList(),
                    perfiles = db.perfiles.Select(p => new SelectListItem()
                    {
                        Value = p.id.ToString(),
                        Text = p.perfil
                    }).ToList(),


                };

                if (user.departamento != null)
                {
                    usuario.listDepartamentos = db.departamentos.Where(d => d.institucion == user.departamentos.institucion)
                        .Select(d => new SelectListItem()
                        {
                            Text = d.nombre,
                            Value = d.id.ToString()
                        }).ToList();
                    usuario.listInstituciones.FirstOrDefault(i => i.Value == user.departamento.ToString()).Selected = true;
                    usuario.intDepartamento = (int)user.departamento;
                    
                }
                else
                {
                    usuario.listDepartamentos = new List<SelectListItem>()
                    {
                        new SelectListItem()
                    };
                }

                if (user.empresa != null)
                {
                    usuario.intEmpresa = (int)user.empresa;
                }
                return View(usuario);
            }

            ViewBag.SuccesMessage = "No se pudo encontrar el usuario";
            return RedirectToAction("Index"); 
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(UsuarioViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (!(viewModel.departamento != null && viewModel.empresa != null))
                {
                    usuarios user = db.usuarios.Find(viewModel.email);

                    user.estado = viewModel.estado == null ? 0 : 1;
                    
                    if (viewModel.intDepartamento != null)
                    {
                        user.departamento = viewModel.intDepartamento;
                    }
                    else if (viewModel.intEmpresa != null)
                    {
                        user.empresa = viewModel.intEmpresa;
                    }

                    if (user.perfiles_usuarios.Any(p => p.estado == 1 && p.perfil != viewModel.perfil.perfil))
                    {
                        if (user.perfiles_usuarios.Any(p => p.estado == 0 && p.perfil == viewModel.perfil.perfil))
                        {
                            user.perfiles_usuarios.Single(p => p.estado == 0 && p.perfil == viewModel.perfil.perfil).estado = 1;
                        }
                        else
                        {
                            viewModel.perfil.estado = 1;
                            user.perfiles_usuarios.Add(viewModel.perfil);
                        }
                    }

                    db.SaveChanges();

                    ViewBag.SuccesMessage = "Permisos del usaurio correctamente editados";
                    return RedirectToAction("Index");

                }

                ModelState.AddModelError("", "Solo se puede asignar o la empresa o el departamento al cual pertenece al usuario");
            }
            return View(viewModel);
        }

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

    }
}