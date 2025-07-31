using ParcelsService.Enums;
using ParcelsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelsService.Strategies
{
    public class OverweightChargeStrategy : IPricingStrategy
    {
        public decimal Calculate(Parcel parcel)
        {
            var limit = parcel.Size switch
            {
                ParcelSize.Small => 1,
                ParcelSize.Medium => 3,
                ParcelSize.Large => 6,
                ParcelSize.XL => 10,
                _ => 0
            };

            var overweight = parcel.WeightKg - limit;
            return overweight > 0 ? overweight * 2 : 0m;
        }
    }
}
