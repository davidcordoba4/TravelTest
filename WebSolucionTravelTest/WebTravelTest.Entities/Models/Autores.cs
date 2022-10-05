using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebTravelTest.Entities.CustomValidations;

#nullable disable

namespace WebTravelTest.Entities.Models
{
    public partial class Autores
    {
        public Autores()
        {
            AutoresHasLibros = new HashSet<AutoresHasLibros>();
        }
        [Key]
        [AntiXssModel(ErrorMessage = "Inyección xss detectada")]
        public int Id { get; set; }
        [MaxLength(45, ErrorMessage = "Supera el máximo permitido de caracteres para el campo Nombre")]
        [Required(ErrorMessage = "Campo requerido Nombre")]
        [AntiXssModel(ErrorMessage = "Inyección xss detectada")]
        public string Nombre { get; set; }
        [MaxLength(45, ErrorMessage = "Supera el máximo permitido de caracteres para el campo Apellidos")]
        [Required(ErrorMessage = "Campo requerido Apellidos")]
        [AntiXssModel(ErrorMessage = "Inyección xss detectada")]
        public string Apellidos { get; set; }

        public virtual ICollection<AutoresHasLibros> AutoresHasLibros { get; set; }
    }
}
