using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SkiStore.Domain.Models.OrderAggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "PaymentRecevied")]
        PaymentRecevied,
        [EnumMember(Value = "PaymentFailed")]
        PaymentFailed
    }
}
