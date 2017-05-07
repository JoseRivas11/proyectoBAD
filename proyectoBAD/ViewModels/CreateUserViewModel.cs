using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace proyectoBAD.ViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [StringLength(250, ErrorMessage = "El campo nombre no debe superar los 250 carácteres")]
        public String name { get; set; }

        [Required(ErrorMessage = "El campo Correo Electrónico es obligatorio")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "El correo ingresado no es válido")]
        public String email { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es obligatorio")]
        [StringLength(25, MinimumLength = 8, ErrorMessage = "La contraseña debe ser de 8 a 25 carácteres de longitud")]
        public String password { get; set; }

        [Required(ErrorMessage = "El campo Confirmación de Contraseña es obligatorio")]
        [Compare("password", ErrorMessage = "Las contraseñas no son iguales")]
        public String passwordConfirm { get; set; }

    }
}