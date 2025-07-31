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
            new Parcel(80, 80, 80), // Large - 15
            new Parcel(2, 2, 2),    // Small - 3
            new Parcel(30, 30, 30)  // Medium - 8
        };
            var service = new ParcelService(parcels);

            // Act
            var cheapest = service.GetCheapestOption();

            // Assert
            Assert.NotNull(cheapest);
            Assert.Equal(ParcelSize.Small, cheapest.Size);
            Assert.Equal(3m, cheapest.Cost);
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

    }
}