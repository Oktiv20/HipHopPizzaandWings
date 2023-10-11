using HipHopPizzaandWings.Models;
using Microsoft.EntityFrameworkCore;

namespace HipHopPizzaandWings
{
    public class HipHopPizzaandWingsDbContext : DbContext
    {
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderType>? OrderTypes { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<MenuItem>? MenuItems { get; set; }
        public DbSet<PaymentType>? PaymentTypes { get; set; }

        public HipHopPizzaandWingsDbContext(DbContextOptions<HipHopPizzaandWingsDbContext> context) : base(context) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(new Employee[]
            {
                new Employee { EmployeeId = 1, employeeName = "Kyle Blunt", employeeEmail = "kblunt@kb.com", employeePassword = "1234", isEmployee = true }
            });

            modelBuilder.Entity<OrderType>().HasData(new OrderType[]
            {
                new OrderType { OrderTypeId = 1, Type = "Walk In" },
                new OrderType { OrderTypeId = 2, Type = "Call In" },
            });

            modelBuilder.Entity<PaymentType>().HasData(new PaymentType[]
            {
                new PaymentType { PaymentTypeId = 1, paymentType = "Cash" },
                new PaymentType { PaymentTypeId = 2, paymentType = "Credit Card" },
            });

            modelBuilder.Entity<MenuItem>().HasData(new MenuItem[]
            {
                new MenuItem { MenuItemId = 1, menuItemName = "Pizza", Description = "Hot Chicken with Honey Drizzle", Price = 14.99m },
                new MenuItem { MenuItemId = 2, menuItemName = "Wings", Description = "Double Fried Bone-In Flats & Drums", Price = 10.99m },
                new MenuItem { MenuItemId = 3, menuItemName = "Cookies", Description = "Homemade Chocolate Chip Cookies", Price = 7.99m },
            });

            modelBuilder.Entity<OrderStatus>().HasData(new OrderStatus[]
            {
                new OrderStatus { OrderStatusId = 1, Status = "Open" },
                new OrderStatus { OrderStatusId = 2, Status = "Closed" },
            });

            modelBuilder.Entity<Order>().HasData(new Order[]
            {
                new Order
                {
                    OrderId = 1,
                    EmployeeId = 1,
                    OrderCreated = DateTime.Now.AddHours(-3),
                    Tip = 2.00m,
                    CustomerName = "Pizz Hut",
                    CustomerEmail = "thehut@pizza.com",
                    CustomerPhone = "281-330-8004",
                    ReviewScore = 3,
                },

                new Order
                {
                    OrderId = 2,
                    EmployeeId = 1,
                    OrderCreated = DateTime.Now.AddHours(-1),
                    Tip = 5.00m,
                    CustomerName = "Meg Lee",
                    CustomerEmail = "ml@ml.com",
                    CustomerPhone = "281-330-8004",
                    ReviewScore = 5,
                },

            });
        }
    }
}
