using ParcelsService.Models;
using ParcelsService.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelsService.Services
{
    public class ParcelService
    {
        private readonly IEnumerable<IPricingStrategy> _strategies;

        public ParcelService(IEnumerable<IPricingStrategy> strategies)
        {
            _strategies = strategies;
        }

        public IEnumerable<ParcelQuote> GetQuotes(Parcel parcel)
        {
            return _strategies.Select(strategy => new ParcelQuote
            {
                Parcel = parcel,
                Size = parcel.Size,                       
                Method = parcel.Method,                   
                BaseCost = strategy.Calculate(parcel),    
                OverweightCharge = 0m,
                SpeedySurcharge = 0m,
                StrategyName = strategy.GetType().Name
            });
        }

        public ParcelQuote GetCheapestOption(Parcel parcel)
        {
            return GetQuotes(parcel)
                .OrderBy(q => q.TotalCost)
                .FirstOrDefault();
        }
    }
}
