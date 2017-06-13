using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace proyectoBAD.ViewModels
{
    public class CategoriaEquipoViewModel
    {
        [Display(Name = "id categoria")]
        public int ID { get; set; }

        [Display (Name = "Nombre de categoría")]
        [Required(ErrorMessage = "Por favor ingrese el nombre")]
        public string CATEGORIA { get; set; }
    }
}