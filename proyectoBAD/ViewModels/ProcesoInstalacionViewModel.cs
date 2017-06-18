using proyectoBAD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.ViewModels
{
    public class ProcesoInstalacionViewModel
    {
        [Display(Name = "Id")]
        public int idProIns { get; set; }

        [Display(Name = "Equipo fisico")]
        [Required(ErrorMessage = "Por favor ingrese el detalle de la instalacion")]
        public string equipo { get; set; }

        [Display(Name = "Proceso de instalacion")]
        public virtual equipos_fisicos equi { get; set; }

        public IEnumerable<SelectListItem> equipos;

        [Display(Name = "Fecha de inicio")]
        [Required(ErrorMessage = "Por favor ingrese la fecha de inicio del mantenimiento")]
        public DateTime fechaIniPro { get; set; }

        [Display(Name = "Fecha de Finalizacion")]
        [Required(ErrorMessage = "Por favor ingrese la fecha de finalizacion del mantenimiento")]
        public DateTime fechaFinPro { get; set; }



    }
}