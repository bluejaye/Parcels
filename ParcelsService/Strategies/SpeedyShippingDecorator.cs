using ParcelsService.Enums;
using ParcelsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelsService.Strategies
{
    public class SpeedyShippingDecorator : IPricingStrategy
    {
        private readonly IPricingStrategy[] _strategies;

        public SpeedyShippingDecorator(params IPricingStrategy[] strategies)
        {
            _strategies = strategies;
        }

        public decimal Calculate(Parcel parcel)
        {
            decimal baseCost = _strategies.Sum(strategy => strategy.Calculate(parcel));
            return parcel.Method == ShippingMethod.Speedy ? baseCost * 2 : baseCost;
        }
    }
}
