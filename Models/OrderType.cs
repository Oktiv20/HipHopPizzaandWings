namespace HipHopPizzaandWings.Models
{
    public class OrderType
    {
        public int OrderTypeId { get; set; }
        public string? Type { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
