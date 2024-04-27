using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandyMan_.Shered.Entities
{
    public class People
    {
        public int Id { get; set; }
        [Display(Name = "Identificacion")]
        public string? Identification { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;
        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Surname { get; set; }
        [Display(Name = "Correo Electronico")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string? Email { get; set; }
        [Display(Name = "Telefono")]
        public string? Phone { get; set; }

        public int PeopleTypeId { get; set; }

        public PeopleType? PeopleType { get; set; }

        public ICollection<Service>? Service { get; set; }
    }
}
