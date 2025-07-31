using ParcelsService.Enums;
using ParcelsService.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelsService.Models
{
    public class Parcel
    {
        public decimal Length { get; }
        public decimal Width { get; }
        public decimal Height { get; }
        public ParcelSize Size => this.EvaluateSize();
        public decimal DimensionCost =>this.CalculateDimensionPrice();
        public decimal DimensionShippingCost =>this.CalculateDimensionShippingPrice();
        public ShippingMethod Method{ get; set; } = ShippingMethod.Standard;
        public int WeightKg { get; internal set; }
        public Parcel(decimal length, decimal width, decimal height, int weightKg, ShippingMethod method = ShippingMethod.Standard)
        {
            Length = length;
            Width = width;
            Height = height;
            WeightKg = weightKg;
            Method = method;
        }
    }
}
