using proyectoBAD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.ViewModels
{
    public class ComprasViewModel
    {
        [Display(Name = "Id")]
        public int id { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "Por favor ingrese la fecha")]
        public DateTime fecha { get; set; }

        [Display(Name = "Tipo de Contratación")]
        [Required(ErrorMessage = "Por favor seleccione un tipo de contratación")]
        public int tipo_contratacion { get; set; }

        [Display(Name = "Tipo de Contratación")]
        public String tipo_contratacion_string;

        [Display(Name = "Institución")]
        public instituciones institucion { get; set; }



        [Display(Name = "Institución")]
        [Required(ErrorMessage = "Por favor seleccione la institución que realizo la compra")]
        public int idInstitucion { get; set; }

        [Display(Name = "Total Compra")]
        public decimal total_compra { get; set; }

        public List<linea_compra> lineas_compra { get; set; }

        public IEnumerable<SelectListItem> tipos_compra;
        public IEnumerable<SelectListItem> instituciones;
        public IEnumerable<SelectListItem> proveedores;
        public IEnumerable<SelectListItem> tipos_equipo;

    }
}