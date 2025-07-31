using ParcelsService.Enums;
using ParcelsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelsService.Strategies
{
    public class DimensionBasedStrategy : IPricingStrategy
    {
        public decimal Calculate(Parcel parcel)
        {
            return parcel.Size switch
            {
                ParcelSize.Small => 3m,
                ParcelSize.Medium => 8m,
                ParcelSize.Large => 15m,
                ParcelSize.XL => 25m,
                _ => throw new InvalidOperationException("Unknown size")
            };
        }
    }
}
