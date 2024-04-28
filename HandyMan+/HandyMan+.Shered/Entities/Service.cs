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
        public int CategoryId { get; set; }

        [Display(Name = "Categoría")]
        public Category? Category { get; set; }

        [Display(Name = "Nombre")]
        public string Name { get; set; } = null!;
        [Display(Name = "Detalle")]
        public string Detail { get; set; } = null!;
        [Display(Name = "Precio")]
        public string Price { get; set; } = null!;

        

         public int PeopleId { get; set; }
        public People? People { get; set; }
    }
}
