using ParcelsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelsService.Strategies
{
    /// <summary>
    /// **This strategy cannot work with others**
    /// Some of the extra weight charges for certain goods were excessive. A new parcel type
    /// has been added to try and address overweight parcels
    /// Heavy parcel, $50 up to 50kg +$1/kg over 50kg
    /// </summary>
    public class HeavyParcelStrategy : IPricingStrategy
    {
        const decimal baseRate = 50m;
        const decimal excessRate = 1m;
        public decimal Calculate(Parcel parcel)
        {
            if (!parcel.IsHeavy)
            {
                throw new InvalidOperationException("Only calculate heavy parcel");
            }
            if ((parcel.WeightKg <= 50))
            {
                return baseRate;
            }

            var excessWeight = parcel.WeightKg - 50;
            return baseRate + excessRate * excessWeight;
        }
    }
}
