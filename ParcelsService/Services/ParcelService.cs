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

        public ParcelQuote GetQuote(Parcel parcel)
        {
            if (parcel.IsHeavy)
            {
                var heavyParcelStrategy = new HeavyParcelStrategy();
                var shippingDecorator = new SpeedyShippingDecorator(heavyParcelStrategy);

                return new ParcelQuote
                {
                    Parcel = parcel,
                    Size = parcel.Size,
                    Method = parcel.Method,
                    BaseCost = heavyParcelStrategy.Calculate(parcel),
                    OverweightCharge = 0m,
                    SpeedySurcharge = shippingDecorator.Calculate(parcel)
                };
            }
            else
            {
                var baseStrategy = new DimensionBasedStrategy();
                var overweightStrategy = new OverweightChargeStrategy();
                var shippingDecorator = new SpeedyShippingDecorator(baseStrategy, overweightStrategy);

                return new ParcelQuote
                {
                    Parcel = parcel,
                    Size = parcel.Size,
                    Method = parcel.Method,
                    BaseCost = baseStrategy.Calculate(parcel),
                    OverweightCharge = overweightStrategy.Calculate(parcel),
                    SpeedySurcharge = shippingDecorator.Calculate(parcel)
                };
            }
        }

        public IEnumerable<ParcelQuote> GetQuotes(IEnumerable<Parcel> parcels)
        {
            return parcels.Select(parcel=> GetQuote(parcel));
        }

        //This method will be availble when step 5 implemented.
        //public ParcelQuote GetCheapestOption(Parcel parcel)
        //{
        //    return GetQuotes(parcel)
        //        .OrderBy(q => q.TotalCost)
        //        .FirstOrDefault();
        //}
    }
}
