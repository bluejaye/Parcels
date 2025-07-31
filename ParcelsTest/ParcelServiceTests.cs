using ParcelsService.Enums;
using ParcelsService.Models;
using ParcelsService.Services;

namespace ParcelsTest
{
    public class ParcelServiceTests
    {
        [Fact]
        public void GetQuotes_ReturnsExpectedQuotes()
        {
            // Arrange
            var parcels = new List<Parcel>
        {
            new Parcel(10, 10, 10), // Medium
            new Parcel(2, 2, 2),    // Small
        };
            var service = new ParcelService(parcels);

            // Act
            var quotes = service.GetQuotes().ToList();

            // Assert
            Assert.Equal(2, quotes.Count);
            Assert.Equal(ParcelSize.Medium, quotes[0].Size);
            Assert.Equal(8m, quotes[0].Cost); // Cost from CalculatePrice()
            Assert.Equal(ParcelSize.Small, quotes[1].Size);
            Assert.Equal(3m, quotes[1].Cost);
        }

        [Fact]
        public void GetCheapestOption_ReturnsSmallParcel()
        {
            // Arrange
            var parcels = new List<Parcel>
            {
                new Parcel(30, 30, 30, ShippingMethod.Standard), // Small ¡ú Lower price
                new Parcel(30, 30, 30, ShippingMethod.Speedy)     // Medium ¡ú More expensive
            };
            var service = new ParcelService(parcels);

            // Act
            var cheapest = service.GetCheapestOption();

            // Assert
            Assert.NotNull(cheapest);
            Assert.Equal(ParcelSize.Medium, cheapest.Size);
            Assert.Equal(ShippingMethod.Standard, cheapest.Method);
            Assert.Equal(cheapest.Cost, cheapest.ShippingCost); // No speed boost
        }

        [Fact]
        public void AddParcel_AppendsToParcelList()
        {
            // Arrange
            var service = new ParcelService(new List<Parcel>());
            var newParcel = new Parcel(5, 5, 5); // Small

            // Act
            service.AddParcel(newParcel);
            var quotes = service.GetQuotes().ToList();

            // Assert
            Assert.Single(quotes);
            Assert.Equal(ParcelSize.Small, quotes[0].Size);
            Assert.Equal(3m, quotes[0].Cost);
        }
        [Fact]
        public void GetShippingQuotes_ReturnsExpectedQuotes()
        {
            // Arrange
            var parcels = new List<Parcel>
            {
                new Parcel(9, 9, 9, ShippingMethod.Standard), // Small
                new Parcel(30, 30, 30, ShippingMethod.Speedy)     // Medium
            };
            var service = new ParcelService(parcels);

            // Act
            var quotes = service.GetQuotes().ToList();

            // Assert
            Assert.Equal(2, quotes.Count);

            Assert.Equal(ParcelSize.Small, quotes[0].Size);
            Assert.Equal(ShippingMethod.Standard, quotes[0].Method);
            Assert.Equal(quotes[0].Cost * 1, quotes[0].ShippingCost);

            Assert.Equal(ParcelSize.Medium, quotes[1].Size);
            Assert.Equal(ShippingMethod.Speedy, quotes[1].Method);
            Assert.Equal(quotes[1].Cost * 2, quotes[1].ShippingCost); // Speedy doubles the price
        }
    }
}