using proyectoBAD.Models;
using proyectoBAD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyectoBAD.ExtHelpers;

namespace proyectoBAD.Controllers
{
    public class PerfilController : Controller
    {

        private proyectoBADEntities db = new proyectoBADEntities();

        // GET: Perfil
        [Authorize]
        public ActionResult Index()
        {
            List<PerfilViewModel> perfiles = db.perfiles.Select(t => new PerfilViewModel()
            {
                id = t.id,
                perfil = t.perfil
            }).ToList();

            if (TempData["successMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["successMessage"];
            }
            return View(perfiles);
        }

        [Authorize]
        public ActionResult Create()
        {
            List<menus> opcionesMenu = db.menus.Where(m => m.super_opcion == null).ToList();
            PerfilViewModel viewModel = new PerfilViewModel()
            {
                opcionesMenu = new List<OpcionMenu>()
            };

            foreach (var opcion in opcionesMenu)
            {
                OpcionMenu opt = new OpcionMenu()
                {
                    menu = opcion,
                    isSelected = false,
                    menus = new List<OpcionMenu>()
                };

                foreach(var subOpcion in opcion.menus1)
                {
                    opt.menus.Add(new OpcionMenu()
                    {
                        menu = subOpcion,
                        isSelected = false
                    });
                }

                viewModel.opcionesMenu.Add(opt);
            }

            ViewBag.Button = "Crear";
            ViewBag.Action = "Create";
            ViewBag.PageHeader = "Crear Perfil";
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PerfilViewModel viewModel)
        {
            ViewBag.Button = "Crear";
            ViewBag.Action = "Create";
            ViewBag.PageHeader = "Crear Perfil";

            if (ModelState.IsValid)
            {
                perfiles perfil = new perfiles()
                {
                    perfil = viewModel.perfil
                };

                for (int i = 0; i < viewModel.opcionesMenu.Count; i++)
                {
                    var opt = viewModel.opcionesMenu[i];
                    if (opt.isSelected == true)
                    {
                        db.menus.Attach(opt.menu);
                        perfiles_permisos permiso = new perfiles_permisos()
                        {
                            menus = opt.menu,
                            estado = 1
                        };
                        perfil.perfiles_permisos.Add(permiso);
                        for (int j = 0; j < opt.menus.Count; j++)
                        {
                            var subMenu = opt.menus[j];
                            if (subMenu.isSelected == true)
                            {
                                db.menus.Attach(subMenu.menu);
                                perfiles_permisos subPermiso = new perfiles_permisos()
                                {
                                    menus = subMenu.menu,
                                    estado = 1
                                };

                                perfil.perfiles_permisos.Add(subPermiso);
                                
                            }
                           
                        }
                    }
                }

                db.perfiles.Add(perfil);
                db.SaveChanges();

                TempData["successMessage"] = "Perfil creado exitosamente";

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            ViewBag.Button = "Editar";
            ViewBag.Action = "Edit";
            ViewBag.PageHeader = "Editar Compra";

            perfiles perfil = db.perfiles.Find(id);
            List<menus> opcionesMenu = db.menus.Where(m => m.super_opcion == null).ToList();
            PerfilViewModel viewModel = new PerfilViewModel()
            {
                id = perfil.id,
                perfil = perfil.perfil,
                opcionesMenu = new List<OpcionMenu>()
            };

            foreach (var opcion in opcionesMenu)
            {
                OpcionMenu opt = new OpcionMenu()
                {
                    menu = opcion,
                    isSelected = perfil.perfiles_permisos.Any(p => p.menu == opcion.id),
                    menus = new List<OpcionMenu>()
                };

                foreach (var subOpcion in opcion.menus1)
                {
                    opt.menus.Add(new OpcionMenu()
                    {
                        menu = subOpcion,
                        isSelected = perfil.perfiles_permisos.Any(p => p.menu == subOpcion.id)
                    });
                }

                viewModel.opcionesMenu.Add(opt);
            }

            return View("Create", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PerfilViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                perfiles perfil = db.perfiles.Find(viewModel.id);
                
                foreach (var permiso in perfil.perfiles_permisos.ToList())
                {
                    if (!viewModel.opcionesMenu.Any(p => p.menu.id == permiso.menu && !p.isSelected || 
                    (p.menus.Any(sp => sp.menu.id == permiso.menu && !p.isSelected)))){
                        perfil.perfiles_permisos.Remove(permiso);
                    }
                }

                foreach (var opt in viewModel.opcionesMenu)
                {
                    if (!perfil.perfiles_permisos.Any(p => p.menu == opt.menu.id) && opt.isSelected)
                    {
                        db.menus.Attach(opt.menu);
                        perfil.perfiles_permisos.Add(new perfiles_permisos()
                        {
                            menus = opt.menu,
                            estado = 1
                        });
                    }

                    foreach (var subOpt in opt.menus)
                    {
                        if (!perfil.perfiles_permisos.Any(p => p.menu == subOpt.menu.id) && subOpt.isSelected)
                        {
                            db.menus.Attach(subOpt.menu);
                            perfil.perfiles_permisos.Add(new perfiles_permisos()
                            {
                                menus = subOpt.menu,
                                estado = 1
                            });
                        }
                    }
                }

                db.SaveChanges();
                TempData["successMessage"] = "Perfil modificado exitosamente";
                return RedirectToAction("Index");
            }
            return View("Create", viewModel);
        }
    }
}