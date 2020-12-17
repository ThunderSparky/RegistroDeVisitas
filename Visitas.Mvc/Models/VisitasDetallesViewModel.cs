using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Visitas.Mvc.Models
{
    public class VisitasDetallesViewModel
    {
        [Display(Name = "Id")]
        public string Id { get; set; }
        [Display(Name = "Código pase")]
        public string Co_Pase { get; set; }
        [Display(Name = "Fecha de Ingreso")]
        public string Fe_Ingreso { get; set; }
        [Display(Name = "Fecha de Salida")]
        public string Fe_Salida { get; set; }
        [Display(Name = "Nombre de Usuario que Registro")]
        public string No_Usuario_Registro { get; set; }
        [Display(Name = "Nombre de Usuario que Modifico")]
        public string No_Usuario_Modificación { get; set; }
        [Display(Name = "Nombre del Lugar de Reunión")]
        public string No_Lugar_Reunion { get; set; }
        [Display(Name = "Nombre del Visitante")]
        public string NombreDelVisitante { get; set; }
        [Display(Name = "Nombre del Trabajador Visitado")]
        public string NombreDelTrabajador { get; set; }
    }
}