using HandyMan_.Shared.Enums;
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
        public string? Photo { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un {0}.")]
        [Display(Name = "Categoria")]
        public int CategoryId { get; set; }

        
        public Category? Category { get; set; }


        public string Name { get; set; } = null!;
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]

        public string Detail { get; set; } = null!;
        [Display(Name = "Detalle")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]

        
        
        public string Price { get; set; } = null!;
        
        [Display(Name = "Proveedor")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string? UserId { get; set; }
       
       
        public User? User { get; set; }
    }

}
