using System;
using System.Collections.Generic;
using System.Text;

namespace Promo.Core.Models
{
    public class Service : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PromoCode { get; set; }
    }
}
