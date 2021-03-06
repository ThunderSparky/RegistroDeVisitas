﻿using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitas.Models
{
    public class Trabajadores
    {
        [Dapper.Contrib.Extensions.Key]//KEY es para llaves que son autoincrementales
        public int Id { get; set; }
        [Display(Name = "Código")]
        public string Co_Trabajador { get; set; }
        [Display(Name = "Nombre")]
        public string No_Trabajador { get; set; }
        [Display(Name = "Apellido Paterno")]
        public string Ap_Paterno_Trabajador { get; set; }
        [Display(Name = "Apellido Materno")]
        public string Ap_Materno_Trabajador { get; set; }
        [Display(Name = "Tipo de Documento")]
        public string Tipo_Documento { get; set; }
        [Display(Name = "Número de Documento")]
        public string Nu_Documento { get; set; }
        [Display(Name = "Biografía")]
        public string Biografia { get; set; }
        public string Imagen { get; set; }
        [Display(Name = "Estado")]
        public string Estado { get; set; }
        [Display(Name = "Oficina")]
        public int OficinaId { get; set; }
        [Display(Name = "Cargo Laboral")]
        public int CargoLaboralId { get; set; }
    }
}
