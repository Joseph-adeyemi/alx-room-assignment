using System;
using System.Collections.Generic;
using System.Text;

namespace Promo.Core.Models.APIRequests
{
    public class ActivateBonusRequest
    {
        public int UserId { get; set; }
        public int ServiceId { get; set; }
    }
}
