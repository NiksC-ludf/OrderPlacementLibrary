namespace OrderPlacement
{
    public class OrderLibrary
    {
        private const decimal discountPercentFromTenth = 0.95m;
        private const decimal discountPercentFromFiftieth = 0.85m;
        private readonly int maxAllowedKitsPerOrder = 999;
        private readonly string InvalidDateExceptionMessage = "Expected delivery date can not be in the past.";
        private readonly string InvalidKitAmountExceptionMessage = "Kit amount per order can not be more than 999.";
        private readonly string InvalidKitTypeMessage = "Kit type {0} doesnt exist.";
        private readonly string TooFewKitAmountMessage = "Kit amount can not be 0.";

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
            _ = ValidateCustomerId(customerId);
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
            Order order = new Order
            {
                customerId = ValidateCustomerId(customerId),
                expectedDeliveryDate = ValidateDate(expectedDeliveryDate),
                desiredAmount = ValidateKitAmount(desiredAmount),
                kitType = ValidateKitType(kitType),
                totalPrice = ApplyDiscount(desiredAmount, kitType),
            };

            orders.Add(order);

            return true;
        }

        /// <summary>
        /// Validates customer identifier.
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        /// <exception cref="ArgumentNullException">Thrown if customer identifier is not set.</exception>
        private int ValidateCustomerId(int customerId)
        {
            if(customerId == default) 
            {
                throw new ArgumentNullException(nameof(customerId));
            }
            return customerId;
        }

        /// <summary>
        /// Validates kit type.
        /// </summary>
        /// <param name="kitType">Kit type.</param>
        /// <exception cref="ArgumentNullException">Thrown if passed kit type is the same as default integer value.</exception>
        /// <exception cref="ArgumentException">Thrown if specified kit doesnt exist.</exception>
        private int ValidateKitType(int kitType)
        {
            if(kitType == default)
            {
                throw new ArgumentNullException(nameof(kitType));
            }

            if(!kitPrices.ContainsKey(kitType))
            {
                throw new ArgumentException(string.Format(InvalidKitTypeMessage, kitType));
            }

            return kitType;
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
            if(expectedDeliveryDate == default)
            {
                throw new ArgumentNullException(nameof(expectedDeliveryDate));
            }
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
            if (amount == default)
            {
                throw new ArgumentException(TooFewKitAmountMessage);
            }

            else if (amount > maxAllowedKitsPerOrder)
            {
                throw new ArgumentException(InvalidKitAmountExceptionMessage);
            }

            return amount;
        }
    }
}