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
        public decimal Cost { get; set; }
        public ParcelSize Size { get; set; }
    }
}
