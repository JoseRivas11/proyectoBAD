using proyectoBAD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.ViewModels
{
    public class MantenimientosViewModel
    {
        [Display(Name = "Id")]
        public int idMan { get; set; }

        [Display(Name = "Fecha de inicio")]
        [Required(ErrorMessage = "Por favor ingrese la fecha de inicio del mantenimiento")]
        public DateTime fechaIniMant { get; set; }

        [Display(Name = "Fecha de Finalizacion")]
        [Required(ErrorMessage = "Por favor ingrese la fecha de finalizacion del mantenimiento")]
        public DateTime fechaFinMant { get; set; }

        [Display(Name = "Tipo de mantenimiento")]
        [Required(ErrorMessage = "Por favor ingrese el numero del proceso de instalacion")]
        public int tipoMant { get; set; }

        [Display(Name = "Equipo fisico")]
        [Required(ErrorMessage = "Por favor ingrese el detalle de la instalacion")]
        public string equipoFisi { get; set; }

        [Display(Name = "Proceso de instalacion")]
        public virtual equipos_fisicos equiFis { get; set; }

        public IEnumerable<SelectListItem> equipos;


    }
}