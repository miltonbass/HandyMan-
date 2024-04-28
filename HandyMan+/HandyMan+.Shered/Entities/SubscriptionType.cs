using HandyMan_.Frontend.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace HandyMan_.Shared.Entities
{
    public class SubscriptionType : IEntityWithName
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de Subscripción")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public double Price { get; set; } = 0;

        [Display(Name = "Descripción")]
        [MaxLength(300, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Description { get; set; } = null!;

        [Display(Name = "Tipo de Usuario")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string UserType { get; set; } = null!;
    }
}