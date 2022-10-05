using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebTravelTest.Entities.CustomValidations;

#nullable disable

namespace WebTravelTest.Entities.Models
{
    public partial class Editoriales
    {
        public Editoriales()
        {
            Libros = new HashSet<Libros>();
        }
        [Key]
        public int Id { get; set; }
        [MaxLength(45, ErrorMessage = "Supera el máximo permitido de caracteres para el campo Nombre")]
        [Required(ErrorMessage = "Campo requerido Nombre")]
        [AntiXssModel(ErrorMessage = "Inyección xss detectada")]
        public string Nombre { get; set; }
        [MaxLength(45, ErrorMessage = "Supera el máximo permitido de caracteres para el campo Sede")]
        [Required(ErrorMessage = "Campo requerido Sede")]
        [AntiXssModel(ErrorMessage = "Inyección xss detectada")]
        public string Sede { get; set; }

        public virtual ICollection<Libros> Libros { get; set; }
    }
}
