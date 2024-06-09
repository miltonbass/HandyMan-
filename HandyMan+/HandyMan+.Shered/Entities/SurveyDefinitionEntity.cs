using HandyMan_.Frontend.Shared.Interfaces;
using HandyMan_.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace HandyMan_.Shared.Entities
{
    public class SurveyDefinitionEntity 
    {
        public int Id { get; set; }

        [Display(Name = "Título de la Pregunta")]
        [MaxLength(200, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Title { get; set; } = null!;

        [Display(Name = "Descripción")]
        [MaxLength(500, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Description { get; set; } = null!;

        [Display(Name = "Tipo de Pregunta")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string QuestionType { get; set; } = null!;

        [Display(Name = "Opciones")]
        public List<string> Options { get; set; } = new List<string>();

        [Display(Name = "Respuesta")]
        public string Answer { get; set; } = " ";

        public List<string> SelectedOptions { get; set; } = new List<string>();

        [Display(Name = "Calificación Estrellas")]
        public int StarRating { get; set; } = 0;

        [Display(Name = "Recomendación")]
        public bool Recommend { get; set; } = false;

        [Display(Name = "Tipo de Usuario")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string UserType { get; set; } = null!;
    }
}