using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using proyectoBAD.Models;
using proyectoBAD.ExtHelpers;

namespace proyectoBAD.ViewModels
{
    public class PerfilViewModel
    {
        [Display(Name = "Id Perfil")]
        public int id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Por favor ingrese el nombre del perfil")]
        public string perfil { get; set; }

        public List<OpcionMenu> opcionesMenu { get; set; }
    }
}