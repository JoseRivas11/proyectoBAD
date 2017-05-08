using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyectoBAD.ViewModels;
using proyectoBAD.Models;
using proyectoBAD.Authentication;
using System.Security.Claims;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginVM)
        {

            if (ModelState.IsValid)
            {
                USUARIO user = UserManager.isValid(loginVM.email, loginVM.password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Correo o contraseña incorrectos");
                }
                else
                {
                    //TODO: Agregar claims de los permisos
                    var ident = new ClaimsIdentity(
                        new[] {
                            new Claim(ClaimTypes.Email, loginVM.email),
                            new Claim(ClaimTypes.Name, user.NOMBRECOMPLETO),
                            new Claim(ClaimTypes.NameIdentifier, user.IDUSUARIO.ToString(), ClaimValueTypes.Integer)
                        }, DefaultAuthenticationTypes.ApplicationCookie);
                    HttpContext.GetOwinContext().Authentication.SignIn(
                        new AuthenticationProperties { IsPersistent = false }, ident);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Login");
        }
    }
}