using proyectoBAD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.ViewModels
{
	public class EquipoFisicoViewModel
	{

        [Display(Name = "Número de Serie")]
        [Required(ErrorMessage = "Por favor ingrese el número de serie del equipo")]
        public string numserial { get; set; }

        [Display(Name = "Fecha de fabricación")]
        [Required(ErrorMessage = "Por favor ingrese la fecha de fabricación del equipo")]
        public System.DateTime fechafabricacion { get; set; }

        [Display(Name = "Tiempo de garantía")]
        [Required(ErrorMessage = "Por favor ingrese el tiempo de garantía")]
        public decimal tiempogarantia { get; set; }

        [Display(Name = "Detalle de garantía")]
        [Required(ErrorMessage = "Por favor ingrese los detalles de la garantía")]
        public String detallesgarantia { get; set; }

        [Display(Name = "EstadoGarantía: 1.Activa 2.Inactiva")]
        [Required(ErrorMessage = "Por favor ingrese el estado de la garantía")]
        [Range(1, 2, ErrorMessage = "Solo se permiten los numeros 1: Garantía Activa 2:Garantía Inactiva")]
        public int estadogarantia { get; set; }

        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "Por favor seleccione el departamento al que pertenece el equipo físico")]
        public int idDepEquiFisico { get; set; }

        [Display(Name = "Departamento perteneciente")]
        public virtual departamentos iddepartamento { get; set; }


        [Display(Name = "Especificación del Equipo")]
        [Required(ErrorMessage = "Por favor seleccione la especificación del equipo a la que pertenece el equipo físico")]
        public int idEspEquiFisico { get; set; }

        [Display(Name = "Especificación del Equipo perteneciente")]
        public virtual esp_equipos idEspequifi { get; set; }


        public IEnumerable<SelectListItem> departamentoequipofisico;
        public IEnumerable<SelectListItem> especificacionequipofisico;

    }
}