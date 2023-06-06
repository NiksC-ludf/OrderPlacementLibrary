namespace OrderPlacement
{
    /// <summary>
    /// Customer order class.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Customer identifier.
        /// </summary>
        public int customerId;

        /// <summary>
        /// Expected kit delivery date.
        /// </summary>
        public DateTime expectedDeliveryDate;

        /// <summary>
        /// Desired kit amount.
        /// </summary>
        public int desiredAmount;

        /// <summary>
        /// Type of kit to order.
        /// </summary>
        public int kitType;

        /// <summary>
        /// Total sum of order;
        /// </summary>
        public decimal totalPrice;
    }
}
