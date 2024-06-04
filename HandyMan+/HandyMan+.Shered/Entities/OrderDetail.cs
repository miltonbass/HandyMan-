using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan_.Shered.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public Order? Order { get; set; }

        public int OrderId { get; set; }

        public Service? Service { get; set; }

        public int ServiceId { get; set; }
    }
}
