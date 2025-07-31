using ParcelsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelsService.Strategies
{
    public class CombinedPricingStrategy : IPricingStrategy
    {
        private readonly IPricingStrategy[] _strategies;

        public CombinedPricingStrategy(params IPricingStrategy[] strategies)
        {
            _strategies = strategies;
        }

        public decimal Calculate(Parcel parcel)
        {
            return _strategies.Sum(strategy => strategy.Calculate(parcel));
        }
    }
}
