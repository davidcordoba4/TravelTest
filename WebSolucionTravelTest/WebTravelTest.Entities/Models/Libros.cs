using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebTravelTest.Entities.CustomValidations;

#nullable disable

namespace WebTravelTest.Entities.Models
{
    public partial class Libros
    {
        public Libros()
        {
            AutoresHasLibros = new HashSet<AutoresHasLibros>();
        }
        [Key]
        [Required(ErrorMessage = "Valor requerido Isbn")]
        [Range(1, 9999999999999, ErrorMessage = "Valor no permitido para Isbn")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Valor no permitido para Isbn")]
        [AntiXssModel(ErrorMessage = "Inyección xss detectada")]
        public long? Isbn { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Valor no permitido para Editorial")]
        [Required(ErrorMessage = "Valor requerido Editorial")]
        [AntiXssModel(ErrorMessage = "Inyección xss detectada")]
        public int EditorialesId { get; set; }
        [MaxLength(45, ErrorMessage = "Supera el máximo permitido de caracteres para el campo Titulo")]
        [Required(ErrorMessage = "Campo requerido Titulo")]
        [AntiXssModel(ErrorMessage = "Inyección xss detectada")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Campo requerido Sinopsis")]
        [AntiXssModel(ErrorMessage = "Inyección xss detectada")]
        public string Sinopsis { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Valor no permitido para el campo NPaginas")]
        [Required(ErrorMessage = "Campo requerido NPaginas")]
        [AntiXssModel(ErrorMessage = "Inyección xss detectada")]
        public string NPaginas { get; set; }

        public virtual Editoriales Editoriales { get; set; }
        public virtual ICollection<AutoresHasLibros> AutoresHasLibros { get; set; }
    }
}
