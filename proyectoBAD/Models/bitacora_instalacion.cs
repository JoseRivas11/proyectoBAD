//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace proyectoBAD.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class bitacora_instalacion
    {
        public int id { get; set; }
        public System.DateTime fecha { get; set; }
        public string detalles { get; set; }
        public int proc_instalacion { get; set; }
    
        public virtual proc_instalacion proc_instalacion1 { get; set; }
    }
}
