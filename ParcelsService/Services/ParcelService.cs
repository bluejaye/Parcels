using ParcelsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelsService.Services
{
    public class ParcelService
    {
        private readonly List<Parcel> _parcels;

        public ParcelService(List<Parcel> parcels)
        {
            _parcels = parcels;
        }

        public IEnumerable<ParcelQuote> GetQuotes()
        {
            foreach (var parcel in _parcels)
            {
                yield return new ParcelQuote
                {
                    Size = parcel.Size,
                    Cost = parcel.DimensionCost,
                    Method = parcel.Method,
                    ShippingCost = parcel.DimensionShippingCost,
                    Parcel = parcel
                };
            }
        }

        public ParcelQuote GetCheapestOption()
        {
            return this.GetQuotes()
                .OrderBy(q => q.ShippingCost)
                .FirstOrDefault();
        }

        public void AddParcel(Parcel parcel)
        {
            _parcels.Add(parcel);
        }
    }
}
