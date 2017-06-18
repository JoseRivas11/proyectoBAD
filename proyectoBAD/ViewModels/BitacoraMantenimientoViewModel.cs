using proyectoBAD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.ViewModels
{
    public class BitacoraMantenimientoViewModel
    {
        [Display(Name = "Id")]
        public int idMa { get; set; }

        [Display(Name = "Detalles")]
        [Required(ErrorMessage = "Por favor ingrese el detalle del mantenimiento")]
        public string detallesMa { get; set; }

        [Display(Name = "Mantenimiento")]
        [Required(ErrorMessage = "Por favor ingrese el numeto de mantenimiento")]
        public int mantenimientoMa { get; set; }

        [Display(Name = "Mantenimientos")]
        public virtual mantenimientos manteMa { get; set; }

        public IEnumerable<SelectListItem> mantenMa;

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "Por favor ingrese la fecha del mantenimiento")]
        public DateTime fechaMa { get; set; }

        

    }
}