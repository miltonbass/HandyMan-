using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandyMan_.Shered.Entities
{
    public class PeopleType
    {
        public int Id { get; set; }
        [Display(Name = "TipoPersona")]
        public string? Name { get; set; }
        [Display(Name = "Detalle")]
        public string? Detail { get; set; }

        
        public ICollection<People>? People { get; set; }
    }
}
