using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandyMan_.Shered.Entities
{
    public class ServiceOrder
    {
        public int Id { get; set; }

        [Display(Name = "Estado")]
        public string? State { get; set; }

        [Display(Name = "Fecha Creación")]
        public DateOnly? CreationDate { get; set; }

        [Display(Name = "Fecha Ejecución")]
        public DateOnly? ExecutionDate { get; set; }

        [Display(Name = "Detalle")]
        public string? Detail { get; set; }


    }
}
