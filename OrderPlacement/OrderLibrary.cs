namespace OrderPlacement
{
    public class OrderLibrary
    {
        private const decimal discountPercentFromTenth = 0.95m;
        private const decimal discountPercentFromFiftieth = 0.85m;
        private readonly int maxAllowedKitsPerOrder = 999;
        private readonly string InvalidDateExceptionMessage = "Expected delivery date can not be in the past.";
        private readonly string InvalidKitAmountExceptionMessage = "Kit amount per order can not be more than 999.";

        private List<Order> orders = new();

        private Dictionary<int, decimal> kitPrices = new Dictionary<int, decimal>
        {
            { 1, 98.99m }, // First kit type and price
        };

        /// <summary>
        /// Gets list of all customer orders.
        /// </summary>
        public List<Order> GetAllCustomerOrders(int customerId)
        {
            return orders.Where(x => x.customerId == customerId).ToList();
        }

        /// <summary>
        /// Places kit order.
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        /// <param name="expectedDeliveryDate">Expected kit delivery date.</param>
        /// <param name="desiredAmount">Desired amount of kits.</param>
        /// <param name="kitType">Kit type identifier.</param>
        /// <returns>True if successful, False in case of failure.</returns>
        public bool PlaceOrder(int customerId, DateTime expectedDeliveryDate, int desiredAmount, int kitType)
        {
            try
            {
                Order order = new Order
                {
                    customerId = customerId,
                    expectedDeliveryDate = ValidateDate(expectedDeliveryDate),
                    desiredAmount = ValidateKitAmount(desiredAmount),
                    kitType = kitType,
                    totalPrice = ApplyDiscount(desiredAmount, kitType),
                };

                orders.Add(order);

            } catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets total price of order minus discount.
        /// </summary>
        /// <param name="amount">Amount of kits in order.</param>
        /// <param name="kitType">Kit type identifier.</param>
        /// <returns></returns>
        private decimal ApplyDiscount(int amount, int kitType)
        {
            decimal price = GetKitPrice(kitType) * amount;

            if (amount >= 10 && amount < 50)
            {
                price *= discountPercentFromTenth;
            }
            if (amount >= 50)
            {
                price *= discountPercentFromFiftieth;
            }

            return price;
        }

        /// <summary>
        /// Gets price of specified kit by its type identifier.
        /// </summary>
        /// <param name="kitType">Kit type identifier.</param>
        /// <returns>Kit price.</returns>
        private decimal GetKitPrice(int kitType)
        {
            return kitPrices.Where(x => x.Key == kitType).First().Value;
        }

        /// <summary>
        /// Validates that expected delivery date is not set in the past.
        /// </summary>
        /// <param name="expectedDeliveryDate">Expected order delivery date.</param>
        /// <returns>Expected delivery date if its valid.</returns>
        /// <exception cref="Exception">Exception thrown if expected delivery date is in the past.</exception>
        private DateTime ValidateDate(DateTime expectedDeliveryDate)
        {
            if (expectedDeliveryDate.Date < DateTime.Now.Date)
            {
                throw new Exception(InvalidDateExceptionMessage);
            }

            return expectedDeliveryDate;
        }

        /// <summary>
        /// Validates kit amount per order. Max is 999.
        /// </summary>
        /// <param name="amount">Amount of kits in order.</param>
        /// <returns>Kit amount if it is valid.</returns>
        /// <exception cref="Exception">Exception throw if amount is invalid.</exception>
        private int ValidateKitAmount(int amount)
        {
            if (amount < 1 || amount > maxAllowedKitsPerOrder)
            {
                throw new Exception(InvalidKitAmountExceptionMessage);
            }

            return amount;
        }
    }
}