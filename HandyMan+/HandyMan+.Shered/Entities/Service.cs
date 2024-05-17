using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan_.Shered.Entities
{
    public class Service
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un {0}.")]
        public int CategoryId { get; set; }

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public Category? Category { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; } = null!;
        [Display(Name = "Detalle")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Detail { get; set; } = null!;
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Price { get; set; } = null!;
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un {0}.")]
        [Display(Name = "Proveedor")]
        public int PeopleId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Display(Name = "Proveedor")]
        public People? People { get; set; }
    }
}
