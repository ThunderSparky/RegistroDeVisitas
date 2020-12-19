using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitas.Models
{
    public class CargosLaborales
    {
        [Dapper.Contrib.Extensions.Key]//KEY es para llaves que son autoincrementales
        public int Id { get; set; }
        [Display(Name = "Código")]
        public string Co_Cargo_Laboral { get; set; }
        [Display(Name = "Nombre")]
        public string No_Cargo_Laboral { get; set; }
        [Display(Name = "Descripción")]
        public string De_Cargo_Laboral { get; set; }
        public string Imagen { get; set; }
        [Display(Name = "Estado")]
        public string Estado { get; set; }
    }
}
