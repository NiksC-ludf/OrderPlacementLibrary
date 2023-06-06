using FluentAssertions;
using OrderPlacement;

namespace OrderLibraryTest
{
    /// <summary>
    /// Order library tests, test name structure:
    /// MethodName_PassedInData_ExpectedResult
    /// </summary>
    public class OrderLibraryTests
    {
        [Fact]
        public void GetAllCustomerOrders_ValidId_ReturnsListOfOrders()
        {
            // Arrange
            OrderLibrary orderLibrary = new OrderLibrary();
            DateTime date = DateTime.Now.AddMonths(1);
            orderLibrary.PlaceOrder(1, date, 2, 1);
            orderLibrary.PlaceOrder(1, date, 4, 1);
            orderLibrary.PlaceOrder(1, date, 6, 1);
            orderLibrary.PlaceOrder(2, date, 7, 1);

            // Act
            var result = orderLibrary.GetAllCustomerOrders(1);

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(3);
            result[0].customerId.Should().Be(1);
            result[0].desiredAmount.Should().Be(2);
            result[0].totalPrice.Should().BePositive();
            result[0].expectedDeliveryDate.Should().Be(date);
            result[1].customerId.Should().Be(1);
            result[1].desiredAmount.Should().Be(4);
            result[1].totalPrice.Should().BePositive();
            result[1].expectedDeliveryDate.Should().Be(date);
            result[2].customerId.Should().Be(1);
            result[2].desiredAmount.Should().Be(6);
            result[2].totalPrice.Should().BePositive();
            result[2].expectedDeliveryDate.Should().Be(date);
        }

        [Fact]
        public void GetAllCustomerOrders_InValidId_ReturnsEmptyList()
        {
            // Arrange
            OrderLibrary orderLibrary = new OrderLibrary();
            orderLibrary.PlaceOrder(1, DateTime.Now.AddMonths(1).Date, 2, 1);
            orderLibrary.PlaceOrder(1, DateTime.Now.AddMonths(2).Date, 4, 1);
            orderLibrary.PlaceOrder(1, DateTime.Now.AddMonths(3).Date, 6, 1);
            orderLibrary.PlaceOrder(2, DateTime.Now.AddMonths(4).Date, 7, 1);

            // Act
            var result = orderLibrary.GetAllCustomerOrders(3);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Fact]
        public void PlaceOrder_ValidData_ReturnsTrue()
        {
            // Arrange
            OrderLibrary orderLibrary = new OrderLibrary();
            DateTime date = DateTime.Now.AddMonths(1);
            int kitType = 1;
            int desiredAmount = 2;
            decimal kitPrice = 98.99m;
            decimal totalPrice = kitPrice * desiredAmount;

            // Act
            bool result = orderLibrary.PlaceOrder(1, date, desiredAmount, kitType);

            // Assert
            result.Should().BeTrue();

            var results = orderLibrary.GetAllCustomerOrders(1);
            results.Should().HaveCount(1);
            results[0].customerId.Should().Be(1);
            results[0].desiredAmount.Should().Be(2);
            results[0].totalPrice.Should().Be(totalPrice);
            results[0].expectedDeliveryDate.Should().Be(date);
        }

        [Fact]
        public void PlaceOrder_InvalidDate_ReturnsFalse()
        {
            // Arrange
            OrderLibrary orderLibrary = new OrderLibrary();
            DateTime date = DateTime.Now.AddMonths(-1);
            int kitType = 1;
            int desiredAmount = 2;

            // Act
            bool result = orderLibrary.PlaceOrder(1, date, desiredAmount, kitType);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void PlaceOrder_ZeroKitAmount_ReturnsFalse()
        {
            // Arrange
            OrderLibrary orderLibrary = new OrderLibrary();
            DateTime date = DateTime.Now.AddMonths(1);
            int kitType = 1;
            int desiredAmount = 0;
            decimal kitPrice = 98.99m;

            // Act
            bool result = orderLibrary.PlaceOrder(1, date, desiredAmount, kitType);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void PlaceOrder_TooManyDesiredKits_ReturnsFalse()
        {
            // Arrange
            OrderLibrary orderLibrary = new OrderLibrary();
            DateTime date = DateTime.Now.AddMonths(1);
            int kitType = 1;
            int desiredAmount = 1000;

            // Act
            bool result = orderLibrary.PlaceOrder(1, date, desiredAmount, kitType);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void PlaceOrder_NoDiscount_ReturnsTrueAndCorrectPrice()
        {
            // Arrange
            OrderLibrary orderLibrary = new OrderLibrary();
            DateTime date = DateTime.Now.AddMonths(1);
            int kitType = 1;
            int desiredAmount = 2;
            decimal totalPrice = 197.98m;

            // Act
            bool result = orderLibrary.PlaceOrder(1, date, desiredAmount, kitType);

            // Assert
            result.Should().BeTrue();
            var results = orderLibrary.GetAllCustomerOrders(1);
            results.Should().NotBeNullOrEmpty();
            results[0].totalPrice.Should().Be(totalPrice);
        }

        [Fact]
        public void PlaceOrder_TenOrderDiscount_ReturnsTrueAndCorrectPrice()
        {
            // Arrange
            OrderLibrary orderLibrary = new OrderLibrary();
            DateTime date = DateTime.Now.AddMonths(1);
            int kitType = 1;
            int desiredAmount = 10;
            decimal totalPrice = 940.405m;

            // Act
            bool result = orderLibrary.PlaceOrder(1, date, desiredAmount, kitType);

            // Assert
            result.Should().BeTrue();
            var results = orderLibrary.GetAllCustomerOrders(1);
            results.Should().NotBeNullOrEmpty();
            results[0].totalPrice.Should().Be(totalPrice);
        }


        [Fact]
        public void PlaceOrder_FiftyOrderDiscount_ReturnsTrueAndCorrectPrice()
        {
            // Arrange
            OrderLibrary orderLibrary = new OrderLibrary();
            DateTime date = DateTime.Now.AddMonths(1);
            int kitType = 1;
            int desiredAmount = 50;
            decimal totalPrice = 4207.075m;

            // Act
            bool result = orderLibrary.PlaceOrder(1, date, desiredAmount, kitType);

            // Assert
            result.Should().BeTrue();
            var results = orderLibrary.GetAllCustomerOrders(1);
            results.Should().NotBeNullOrEmpty();
            results[0].totalPrice.Should().Be(totalPrice);
        }
    }
}