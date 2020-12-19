using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitas.Models
{
    public class Oficinas
    {
        [Dapper.Contrib.Extensions.Key]//KEY es para llaves que son autoincrementales
        public int Id { get; set; }
        public string Sede { get; set; }
        [Display(Name = "Código")]
        public string Co_Oficina { get; set; }
        [Display(Name = "Nombre")]
        public string No_Oficina { get; set; }
        [Display(Name = "Descripción")]
        public string De_Oficina { get; set; }
        [Display(Name = "Jefe")]
        public string Jefe_Oficina { get; set; }
        public string Imagen { get; set; }
        [Display(Name = "Estado")]
        public string Estado { get; set; }
    }
}
