using System;
using System.Collections.Generic;
using System.Text;

namespace Promo.Core.Models
{
    public class PromoActivation : BaseEntity
    {
        public int UserId { get; set; }
        public int ServiceId { get; set; }
    }
}
