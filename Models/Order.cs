﻿using System.ComponentModel.DataAnnotations;

namespace HipHopPizzaandWings.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int EmployeeId { get; set; }
        public Employee employee { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerEmail { get; set; }
        public DateTime OrderCreated { get; set; }
        public List<MenuItem>? menuItem { get; set; }
        public List<PaymentType>? paymentType { get; set; }
        public List<OrderType>? Type { get; set; }
        public List<OrderStatus>? Status { get; set; }
        public decimal Tip { get; set; }
        public decimal totalPrice => menuItem.Sum(i => i.Price);
        public int Revenue { get; set; }
        public int ReviewScore { get; set; }
        public Order() { 
           this.OrderCreated = DateTime.Now;
        }

    }
}
