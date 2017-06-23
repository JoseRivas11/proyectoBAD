using proyectoBAD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.ViewModels
{
    public class SolicitudesViewModel
    {
        [Display(Name ="Id")]
        public int ID { get; set; }

        [Display(Name ="Fecha de Registro")]
        [Required(ErrorMessage = "Ingrese la fecha de la solicitud")]
        public DateTime FECHA_REGISTRO { get; set; }

        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "Seleccione el departamento")]
        public int IDDEPARTAMENTO { get; set; }

        [Display(Name = "Departamento")]
        public departamentos DEPARTAMENTO { get; set; }

        [Display(Name = "Estado")]//estado
        [Required(ErrorMessage = "Por favor seleccione el estado de la solicitud")]
        public int ESTADO { get; set; }

        [Display(Name = "Seleccione los equipos")]
        public String ESTADO_STRING;

        [Display(Name = "Seleccione institución")]
        public int INTITUCION;

        public List<equipos_solicitud> EQUIPOS_SOLICITUD { get; set; }

        public IEnumerable<SelectListItem> DEPARTAMENTOS;
        public IEnumerable<SelectListItem> EQUIPOS;
        public IEnumerable<SelectListItem> TIPOS_SOLICITUD;
        public IEnumerable<SelectListItem> tipo_estado;
        public IEnumerable<SelectListItem> INSTITUCIONES;   

    }
}