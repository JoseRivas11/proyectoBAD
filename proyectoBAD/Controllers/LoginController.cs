using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyectoBAD.ViewModels;
using BCrypt.Net;
using proyectoBAD.Models;

namespace proyectoBAD.Controllers
{
    public class LoginController : Controller
    {
        private proyectoBADCon db = new proyectoBADCon();

        // GET: Login
        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(CreateUserViewModel cUserVM)
        {
            if (ModelState.IsValid)
            {
                string salt = BCrypt.Net.BCrypt.GenerateSalt(15);

                USUARIO user = new USUARIO();
                user.NOMBRECOMPLETO = cUserVM.name;
                user.PASSWORD = BCrypt.Net.BCrypt.HashPassword(cUserVM.password, salt);
                user.EMAIL = cUserVM.email;

                db.USUARIOs.Add(user);

                if (db.USUARIOs.Any(u => u.EMAIL == user.EMAIL))
                {
                    ModelState.AddModelError("", "Un usuario ya se encuentra registrado con este Correo Electrónico");
                }
                else
                {
                    db.SaveChanges();
                    ViewBag.successMessage = "El usuario ha sido exitosamente creado, por favor espere que sus permisos sean asignados";
                }
            }

            return View();
        }
    }
}