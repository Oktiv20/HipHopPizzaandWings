namespace HipHopPizzaandWings.Models
{
    public class OrderStatus
    {
        public int OrderStatusId { get; set; }
        public string? Status { get; set; }
        public List<Order>? Order { get; set; }
    }
}
