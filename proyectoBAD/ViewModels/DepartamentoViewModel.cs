using proyectoBAD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.ViewModels
{
    public class DepartamentoViewModel
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Por favor ingrese el nombre de la empresa")]
        public string nombre { get; set; }

        [Display(Name = "Email Contacto")]
        [Required(ErrorMessage = "Por favor ingrese el email de contacto de la empresa")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "El correo ingresado no es válido")]
        public string email_contacto { get; set; }

        [Display(Name = "Telefóno")]
        [Required(ErrorMessage = "Por favor ingrese el telefóno de contacto de la empresa")]
        public string telefono { get; set; }

        [Display(Name = "Institución")]
        [Required(ErrorMessage = "Por favor selecciona la institución a la que pertenece este departamento")]
        public int idInst { get; set; }

        [Display(Name = "Institución perteneciente")]
        public virtual instituciones institucion { get; set; }

        public IEnumerable<SelectListItem> instituciones;
    }
}