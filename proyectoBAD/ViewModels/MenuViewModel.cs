using proyectoBAD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.ViewModels
{
    public class MenuViewModel
    {
        [Display(Name = "Id Menú")]
        public int id { get; set; }

        [Display(Name = "Subopcion de")]
        public Nullable<int> super_opcion { get; set; }

        [Display(Name = "Nombre opción")]
        [Required(ErrorMessage = "Por favor ingrese el nombre de la opción")]
        public String nombre_opcion { get; set; }

        [Display(Name = "Controlador")]
        public String controlador { get; set; }

        [Display(Name = "Acción")]
        public String accion { get; set; }

        [Display(Name = "Estado")]
        public Nullable<int> estado { get; set; }

        public String nombre_estado;

        public menus superOpcion;

        public List<SelectListItem> superOpciones;


    }
}