using proyectoBAD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.ViewModels
{
    public class BitacoraInstalacionViewModel
    {
        [Display(Name = "Id")]
        public int id1 { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "Por favor ingrese la fecha de instalacion")]
        public DateTime fecha1 { get; set; }

        [Display(Name = "Detalles")]
        [Required(ErrorMessage = "Por favor ingrese el detalle de la instalacion")]
        public string detalles1 { get; set; }

        [Display(Name = "Equipos en Proceso de instalacion")]
        [Required(ErrorMessage = "Por favor ingrese el numero del proceso de instalacion")]
        public int proc_instalacion1 { get; set; }

        [Display(Name = "Proceso de instalacion")]
        public virtual proc_instalacion pro_id { get; set; }

        public IEnumerable<SelectListItem> proc_insta;


    }
}