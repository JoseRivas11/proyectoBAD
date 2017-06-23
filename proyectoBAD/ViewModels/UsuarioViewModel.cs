using proyectoBAD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoBAD.ViewModels
{
    public class UsuarioViewModel
    {
        public int id { get; set; }

        [Display(Name = "Email")]
        public String email { get; set; }
        
        [Display(Name = "Nombre Completo")]
        public String nombre_completo { get; set; }

        [Display(Name = "Estado")]
        public Nullable<int> estado { get; set;}

        public String strEstado;

        [Display(Name = "Departamento")]
        public Nullable<int> intDepartamento { get; set; }

        [Display(Name = "Empresa")]
        public Nullable<int> intEmpresa { get; set; }

        [Display(Name = "Departamento")]
        public virtual departamentos departamento { get; set; }

        [Display(Name = "Empresa")]
        public virtual empresas empresa { get; set; }

        [Display(Name = "Perfil")]
        [Required]
        public perfiles_usuarios perfil { get; set; }

        public List<SelectListItem> listEmpresa;
        public List<SelectListItem> listInstituciones;
        public List<SelectListItem> listDepartamentos;
        public List<SelectListItem> perfiles;

    }
}