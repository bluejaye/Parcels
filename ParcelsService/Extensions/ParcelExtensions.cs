using ParcelsService.Enums;
using ParcelsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelsService.Extensions
{
    public static class ParcelExtensions
    {
        public static ParcelSize EvaluateSize(this Parcel parcel)
        {
            if (parcel.Length >= 100 || parcel.Width >= 100 || parcel.Height >= 100)
                return ParcelSize.XL;
            if (parcel.Length < 10 && parcel.Width < 10 && parcel.Height < 10)
                return ParcelSize.Small;
            if (parcel.Length < 50 && parcel.Width < 50 && parcel.Height < 50)
                return ParcelSize.Medium;
            if (parcel.Length < 100 && parcel.Width < 100 && parcel.Height < 100)
                return ParcelSize.Large; 
            throw new ArgumentNullException("Invalid parcel dimensions");
        }
        public static decimal CalculatePrice(this Parcel parcel)
        {
            return parcel.EvaluateSize() switch
            {
                ParcelSize.Small => 3m,
                ParcelSize.Medium => 8m,
                ParcelSize.Large => 15m,
                ParcelSize.XL => 25m,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

    }
}
