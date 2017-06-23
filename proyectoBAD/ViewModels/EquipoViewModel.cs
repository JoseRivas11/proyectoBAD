using proyectoBAD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.ViewModels
{
    public class EquipoViewModel
    {
        [Display(Name = "Id Equipo")]
        public int IDEQ { get; set; }

        [Display(Name = "Nombre equipo")]
        [Required(ErrorMessage = "Por favor ingrese el nombre del equipo")]
        public string NOMEQ { get; set; }

        [Display(Name = "Especificación de equipo")]
        [Required(ErrorMessage = "Por favor ingrese la especificación del equipo")]
        public string ESPECEQ { get; set; }

        [Display(Name = "Cagtegoría de equipo")]
        [Required(ErrorMessage = "Por favor ingrese la categoria del equipo")]
        public int IDCATEQ { get; set; }

        [Display(Name = "Categoría de equipo")]
        public virtual categorias_equipo CATEQUIPO { get; set; }

        public IEnumerable<SelectListItem> categoriaEquipo;

       
    }
}