namespace HipHopPizzaandWings.Models
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public List<Order>? Order { get; set; }
    }
}
