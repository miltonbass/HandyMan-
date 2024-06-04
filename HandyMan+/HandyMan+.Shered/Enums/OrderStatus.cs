using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyMan_.Shered.Enums
{
    public enum OrderStatus
    {
        [Description("Creada")]
        Created,

        [Description("Pendiente")]
        Pending,

        [Description("Aprobada")]
        Approved,

        [Description("En Proceso")]
        Processing,

        [Description("Pagada")]
        Paid,

        [Description("Completada")]
        Completed,

        [Description("Fallida")]
        Failed,

        [Description("Cancelada")]
        Cancelled,

        [Description("Reembolsada ")]
        Refunded

    }
}
