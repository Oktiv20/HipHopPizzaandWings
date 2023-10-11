namespace HipHopPizzaandWings.DTOs
{
    public class CreateOrderDTO
    {
        public int EmployeeId { get; set; }
        public DateTime? OrderCreated { get; set; }
        public DateTime? OrderClosed { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public decimal Tip { get; set; }
        public int ReviewScore { get; set; }
        public List<int> MenuItemId { get; set; }
    }
}
