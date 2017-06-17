using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace proyectoBAD.ViewModels
{
    public class InstitucionViewModel
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Por favor ingrese el nombre de la institución")]
        public string nombre { get; set; }

        [Display(Name = "Telefóno")]
        [Required(ErrorMessage = "Por favor ingrese el número telefónico de la institución")]
        public string telefono { get; set; }

        [Display(Name = "Email contacto")]
        [Required(ErrorMessage = "Por favor ingrese el email de contacto de la institución")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "El correo ingresado no es válido")]
        public string email_contacto { get; set; }

        [Display(Name = "Dirrección")]
        [Required(ErrorMessage = "Por favor ingrese la dirección de la institución")]
        public string direccion { get; set; }

        [Display(Name = "Máximo Compra")]
        [Required(ErrorMessage = "Por favor ingrese el máximo de compra por año por empresa")]
        [Range(1, int.MaxValue, ErrorMessage = "Por favor ingrese un valor positivo o mayor a 0")]
        public decimal max_compras { get; set; }
    }
}