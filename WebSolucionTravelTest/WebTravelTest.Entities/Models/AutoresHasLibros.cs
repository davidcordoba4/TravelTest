#nullable disable

using System.ComponentModel.DataAnnotations;
using WebTravelTest.Entities.CustomValidations;

namespace WebTravelTest.Entities.Models
{
    public partial class AutoresHasLibros
    {
        [Required(ErrorMessage = "Valor requerido Autor")]
        [Range(1, int.MaxValue, ErrorMessage = "Valor no permitido para Autor")]
        [Key]
        [AntiXssModel(ErrorMessage = "Inyección xss detectada")]
        public int AutoresId { get; set; }
        [Required(ErrorMessage = "Valor requerido Libro Isbn")]
        [Range(1, 9999999999999, ErrorMessage = "Valor no permitido para Libro Isbn")]
        [Key]
        [AntiXssModel(ErrorMessage = "Inyección xss detectada")]
        public long LibrosIsbn { get; set; }

        public virtual Autores Autores { get; set; }
        public virtual Libros LibrosIsbnNavigation { get; set; }
    }
}
