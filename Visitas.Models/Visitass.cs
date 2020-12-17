using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitas.Models
{
    public class Visitass
    {
        [Dapper.Contrib.Extensions.Key]//KEY es para llaves que son autoincrementales
        public int Id { get; set; }
        [Display(Name = "Fecha de Registro")]
        public DateTime? Fe_Registro { get; set; }
        [Display(Name = "Código de Pase")]
        public string Co_Pase { get; set; }
        [Display(Name = "Fecha de Ingreso")]
        public string Fe_Ingreso { get; set; }
        [Display(Name = "Fecha de Salida")]
        public string Fe_Salida { get; set; }
        [Display(Name = "Usuario que Registró")]
        public string No_Usuario_Registro { get; set; }
        [Display(Name = "Usuario que Modificó")]
        public string No_Usuario_Modificación { get; set; }
        [Display(Name = "Lugar de Reunión")]
        public string No_Lugar_Reunion { get; set; }
        [Display(Name = "Motivo")]
        public string Motivo { get; set; }
        [Display(Name = "Activo")]
        public string No_Estado { get; set; }
        [Display(Name = "Código de Visitante")]
        public int VisitanteId { get; set; }
        [Display(Name = "Código de Trabajador")]
        public int TrabajadorId { get; set; }
    }
}
