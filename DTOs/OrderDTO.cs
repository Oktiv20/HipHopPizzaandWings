namespace HipHopPizzaandWings.DTOs
{
    public class OrderDTO
    {
        public string? OrderName { get; set; }
        public string? OrderStatus { get; set; }
        public string? CustomerNumber { get; set; }
        public string? CustomerEmail { get; set; }
        public string? OrderType { get; set; }
        public List<MenuItemDTO>? MenuItems { get; set; }
    }
}
