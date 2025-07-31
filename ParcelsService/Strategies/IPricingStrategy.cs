using ParcelsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcelsService.Strategies
{
    public interface IPricingStrategy
    {
        decimal Calculate(Parcel parcel);
    }
}
