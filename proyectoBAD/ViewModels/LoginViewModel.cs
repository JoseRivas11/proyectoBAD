using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace proyectoBAD.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Por favor ingresé su correo")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Correo no válido")]
        public String email { get; set; }

        [Required(ErrorMessage = "Por favor ingresé su contraseña")]
        public String password { get; set; }
    }
}