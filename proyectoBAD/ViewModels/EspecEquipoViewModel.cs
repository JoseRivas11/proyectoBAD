using proyectoBAD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.ViewModels
{
    public class EspecEquipoViewModel
    {
        [Display(Name = "Id")]
        public int IDESPEQ { get; set; }

        [Display(Name = "Marca")]
        [Required(ErrorMessage = "Ingrese la Marca del Equipo por favor")]
        public string MARCAESPEQ { get; set; }

        [Display(Name = "Modelo")]
        [Required(ErrorMessage = ("Por favor ingrese el modelo del equipo"))]
        public string MODELESPEQ { get; set; }

        [Display(Name = "Potencia")]
        [Required(ErrorMessage = "Por favor ingrese la potencia del equipo")]
        public decimal POTENCIAESPEQ { get; set; }

        [Display(Name = "Consumo de corriente")]
        [Required(ErrorMessage = "Ingrese una cantidad")]
        public decimal CONSCORRESPEQ { get; set; }

        [Display(Name = "Capacidad de BTU")]
        public Nullable<decimal> CAPBTUESPEQ { get; set; }

        [Display(Name = "nombre del Equipo")]
        [Required(ErrorMessage = "Por favor seleccione el nombre del equipo")]
        public int IDEQESPEQ { get; set; }

        [Display(Name = "Equipo perteneciente")]
        public virtual equipos IDEQUIPO { get; set; }

        [Display(Name = "Empresa")]
        [Required(ErrorMessage = "Por favor seleccione el nombre de la empresa")]
        public int IDEMPESPEQ { get; set; }

        [Display(Name = "Empresa perteneciente")]
        public virtual empresas IDEMPRESA { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "Por favor ingrese el precio")]
        public decimal PRECIOESPEQ { get; set; }

        public IEnumerable<SelectListItem> equiposEsp;
        public IEnumerable<SelectListItem> empresasEsp;
    }
}