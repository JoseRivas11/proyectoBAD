using proyectoBAD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyectoBAD.ExtHelpers
{
    public class OpcionMenu
    {
        public menus menu { get; set; }
        public List<OpcionMenu> menus { get; set; }
        public bool isSelected { get; set; }
        
    }
}