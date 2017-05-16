using proyectoBAD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.ViewModels
{
    public class EmpresaViewModel
    {
        [Display(Name = "Id Empresa")]
        public int IDEMP { get; set; }

        [Display(Name = "Nombre Empresa")]
        [Required(ErrorMessage = "Por favor ingrese el nombre de la empresa")]
        public string NOMBREEMP { get; set; }

        [Display(Name = "Direccion")]
        [Required(ErrorMessage = "Por favor ingrese la dirección de la empresa")]
        public string DIRECCIONEMP { get; set; }

        [Display(Name = "Telefono Contacto")]
        [Required(ErrorMessage = "Por favor ingrese el telefóno de contacto de la empresa")]
        public string TELEFONOCONTACTO { get; set; }

        [Display(Name = "Email contacto")]
        [Required(ErrorMessage = "Por favor ingrese el email de contacto de la empresa")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "El correo ingresado no es válido")]
        public string EMAILCONTACTO { get; set; }

        [Display(Name = "NIT")]
        [Required(ErrorMessage = "Por favor ingrese el NIT de la empresa")]
        public string NITEMP { get; set; }

        [Display(Name = "Nombre Representante")]
        [Required(ErrorMessage = "Por favor ingrese el nombre del representante de la empresa")]
        public string NOMBRERESPEMP { get; set; }

        [Display(Name = "DUI Responsanble")]
        [Required(ErrorMessage = "Por favor ingrese el DUI del representante de la empresa")]
        public string DUIRESPEMP { get; set; }

        [Display(Name = "Tipo Empresa")]
        [Required(ErrorMessage = "Por favor seleccione el tipo de empresa")]
        public int IDTIPO { get; set; }

        public IEnumerable<SelectListItem> tiposEmpresa;
    }
}