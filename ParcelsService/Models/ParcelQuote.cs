using ParcelsService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelsService.Models
{
    public class ParcelQuote
    {
        public Parcel Parcel { get; set; }                    
        public ParcelSize Size { get; set; }                   
        public ShippingMethod Method { get; set; }             
        public decimal BaseCost { get; set; }                  
        public decimal OverweightCharge { get; set; }          
        public decimal SpeedySurcharge { get; set; }           
        public decimal TotalCost => BaseCost + OverweightCharge + SpeedySurcharge;
        public string StrategyName { get; set; }               
    }
}
