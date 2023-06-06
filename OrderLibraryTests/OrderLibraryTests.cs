using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderPlacement;
using FluentAssertions;
using System;

namespace OrderLibraryTests
{
    [TestClass]
    public class OrderLibraryTests
    {
        [TestMethod]
        public void GetAllCustomerOrders_ValidId_ReturnsListOfOrders()
        {
            // Arrange
            OrderLibrary orderLibrary = new OrderLibrary();
            orderLibrary.PlaceOrder(1, DateTime.Now.AddMonths(1).Date, 2, 1);
            orderLibrary.PlaceOrder(1, DateTime.Now.AddMonths(2).Date, 4, 1);
            orderLibrary.PlaceOrder(1, DateTime.Now.AddMonths(3).Date, 6, 1);
            orderLibrary.PlaceOrder(2, DateTime.Now.AddMonths(4).Date, 7, 1);

            // Act
            var result = orderLibrary.GetAllCustomerOrders(1);

            // Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(3);
            result[0].customerId.Should().Be(1);
            result[0].desiredAmount.Should().Be(2);
            result[0].totalPrice.Should().BePositive();
            result[1].customerId.Should().Be(1);
            result[1].desiredAmount.Should().Be(4);
            result[1].totalPrice.Should().BePositive();
            result[2].customerId.Should().Be(1);
            result[2].desiredAmount.Should().Be(6);
            result[2].totalPrice.Should().BePositive();
        }
    }
}
