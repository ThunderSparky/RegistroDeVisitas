using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitas.Models
{
    public class Visitantes
    {
        [Dapper.Contrib.Extensions.Key]//KEY es para llaves que son autoincrementales
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        public string No_Visitante { get; set; }
        [Display(Name = "Apellido Paterno")]
        public string Ap_Paterno_Visitante { get; set; }
        [Display(Name = "Apellido Materno")]
        public string Ap_Materno_Visitante { get; set; }
        [Display(Name = "Tipo de Documento")]
        public string Tipo_Documento { get; set; }
        [Display(Name = "Número de Documento")]
        public string Nu_Documento { get; set; }
        public string Imagen { get; set; }
        [Display(Name = "Entidad")]
        public int InstitutoId { get; set; }
        [Display(Name = "Estado")]
        public string Estado { get; set; }
    }
}
