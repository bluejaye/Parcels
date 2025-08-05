using ParcelsService.Enums;
using ParcelsService.Models;
using ParcelsService.Services;
using ParcelsService.Strategies;

namespace ParcelsTest
{
    public class ParcelServiceTests
    {
        [Fact]
        public void DimensionBasedStrategy_ShouldMatchDimensionCost()
        {
            var parcel = new Parcel(length: 20m, width: 20m, height: 20m, weightKg: 2);
            var strategy = new DimensionBasedStrategy();

            var cost = strategy.Calculate(parcel);

            Assert.Equal(parcel.DimensionCost, cost); 
        }

        [Fact]
        public void SpeedyShippingDecorator_ShouldDoubleBaseCost()
        {
            var parcel = new Parcel(length: 20m, width: 20m, height: 20m, weightKg: 2, method: ShippingMethod.Speedy);
            var baseStrategy = new DimensionBasedStrategy();
            var speedyStrategy = new SpeedyShippingDecorator(baseStrategy);

            var expected = parcel.DimensionShippingCost; 
            var result = speedyStrategy.Calculate(parcel);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void OverweightChargeStrategy_ShouldReturnCorrectOverweightCost()
        {
            var parcel = new Parcel(length: 40m, width: 30m, height: 30m, weightKg: 5);

            var strategy = new OverweightChargeStrategy();
            var cost = strategy.Calculate(parcel);
            //overweight 2kg, for each kg add $2, then it's $4
            Assert.Equal(4m, cost);
        }

        [Fact]
        public void CombinedPricingStrategy_ShouldAggregate_Dimension_Overweight_AndSpeedyCharges()
        {
            // Arrange
            var parcel = new Parcel(length: 50m, width: 50m, height: 50m, weightKg: 7, method: ShippingMethod.Speedy);

            var baseStrategy = new DimensionBasedStrategy();                  
            var overweightStrategy = new OverweightChargeStrategy();         
            var speedyDecorator = new SpeedyShippingDecorator(baseStrategy); 

            var strategy = new CombinedPricingStrategy(baseStrategy, overweightStrategy, speedyDecorator);

            var dimensionCost = baseStrategy.Calculate(parcel);              
            var overweightCost = overweightStrategy.Calculate(parcel);       
            var speedyCost = speedyDecorator.Calculate(parcel);              

            var expectedTotal = dimensionCost + overweightCost + speedyCost;

            // Act
            var actualTotal = strategy.Calculate(parcel);

            // Assert
            Assert.Equal(expectedTotal, actualTotal);
        }
        [Fact]
        public void HeavyParcelStrategy_ShouldReturnCorrectHeavyParcelCost()
        {
            // Arrange
            var parcel = new Parcel(length: 50m, width: 50m, height: 50m, weightKg: 52, method: ShippingMethod.Speedy);
            var heavyParcelStrategy = new HeavyParcelStrategy();
            var speedyDecorator = new SpeedyShippingDecorator(heavyParcelStrategy);

            // Act
            var heavyParcelCost = heavyParcelStrategy.Calculate(parcel);
            var expectedCost = 52m;
            var speedyCost = speedyDecorator.Calculate(parcel);
            var expectedSpeedyCost = expectedCost * 2;

            // Assert
            Assert.Equal(expectedCost, heavyParcelCost);
            Assert.Equal(expectedSpeedyCost, speedyCost);
        }
    }
}