using HipHopPizzaandWings;
using HipHopPizzaandWings.DTOs;
using HipHopPizzaandWings.Models;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7284",
                                              "http://localhost:3000")
                                               .AllowAnyHeader()
                                               .AllowAnyMethod();
                      });
});

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<HipHopPizzaandWingsDbContext>(builder.Configuration["HHPWDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


// ENDPOINTS


// ORDERS 


// GET ALL ORDERS
app.MapGet("/api/orders", async (HipHopPizzaandWingsDbContext db) =>
{
    var orders = await db.Orders
    .Include(o => o.Type)
    .ToListAsync();

    var ordersDTO = orders.Select(order => new OrderDTO
    {
        OrderName = order.CustomerName,
        OrderStatus = order.OrderClosed.HasValue ? "Closed" : "Open",
        CustomerNumber = order.CustomerPhone,
        CustomerEmail = order.CustomerEmail,
        OrderType = order.Type?.FirstOrDefault()?.Type,
    }).ToList();

    if (orders == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(ordersDTO);
});


// GET SINGLE ORDER
app.MapGet("/api/orders/{orderId}", async (HipHopPizzaandWingsDbContext db, int orderId) =>
{
    var order = await db.Orders
    .Include(o => o.Type)
    .Include(o => o.MenuItems)
    .Where(o => o.OrderId == orderId)
    .FirstOrDefaultAsync();

    if (order == null)
    {
        return Results.NotFound();
    }

    var orderDTO = new OrderDTO
    {
        OrderName = order.CustomerName,
        OrderStatus = order.OrderClosed.HasValue ? "Closed" : "Open",
        CustomerNumber = order.CustomerPhone,
        CustomerEmail = order.CustomerEmail,
        OrderType = order.Type?.FirstOrDefault()?.Type,
        MenuItems = order.MenuItems.Select(menuItem => new MenuItemDTO
        {
            menuItemName = menuItem.Name,
            Price = menuItem.Price,
        }).ToList(),
    };

    return Results.Ok(orderDTO);
});


// CREATE ORDER
app.MapPost("/api/orders", async (HipHopPizzaandWingsDbContext db, CreateOrderDTO orderDTO) =>
{
    try
    {
        if (orderDTO == null)
        {
            return Results.BadRequest("Invalid data provided.");
        }

        // Create an Order entity based on the OrderDTO.
        var order = new Order
        {
            EmployeeId = orderDTO.EmployeeId,
            CustomerName = orderDTO.CustomerName,
            CustomerEmail = orderDTO.CustomerEmail,
            CustomerPhone = orderDTO.CustomerPhone,
            Tip = orderDTO.Tip,
            ReviewScore = orderDTO.ReviewScore,
            OrderCreated = orderDTO.OrderCreated,
            OrderClosed = orderDTO.OrderClosed,
        };

        // Handle menu items
        if (orderDTO.MenuItemId != null)
        {
            order.MenuItems = new List<MenuItem>();
            foreach (var itemId in orderDTO.MenuItemId)
            {
                var menuItem = await db.MenuItems.FindAsync(itemId);
                if (menuItem != null)
                {
                    order.MenuItems.Add(menuItem);
                }
            }
        }

        // Save the order to the database.
        db.Orders.Add(order);
        await db.SaveChangesAsync();

        // Return a success response with the newly created order.
        return Results.Created($"/api/orders/{order.OrderId}", order);
    }
    catch (Exception ex)
    {
        // Handle exceptions and return a relevant error response.
        return Results.BadRequest("An error occurred while creating the order.");
    }
});


// EDIT ORDER BY ID
app.MapPut("/api/orders/{orderId}", async (HipHopPizzaandWingsDbContext db, int orderId, CreateOrderDTO updatedOrderDTO) =>
{
    var order = await db.Orders.FindAsync(orderId);

    if (order == null)
    {
        return Results.NotFound();
    }

    try
    {
        order.EmployeeId = updatedOrderDTO.EmployeeId;
        order.OrderCreated = updatedOrderDTO.OrderCreated;
        order.OrderClosed = updatedOrderDTO.OrderClosed;
        order.CustomerName = updatedOrderDTO.CustomerName;
        order.CustomerPhone = updatedOrderDTO.CustomerPhone;
        order.CustomerEmail = updatedOrderDTO.CustomerEmail;
        order.Tip = updatedOrderDTO.Tip;
        order.ReviewScore = updatedOrderDTO.ReviewScore;


        db.Update(order);
        db.SaveChanges();

        return Results.Ok(order);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex);
    }
});


// DELETE ORDER BY ID
app.MapDelete("/api/orders/{orderId}", async (HipHopPizzaandWingsDbContext db, int orderId) =>
{
    try
    {
        // Find the order by OrderId
        var order = await db.Orders.FindAsync(orderId);

        if (order == null)
        {
            return Results.NotFound("Order not found.");
        }

        // Remove the order from the database
        db.Orders.Remove(order);
        await db.SaveChangesAsync();
        return Results.NoContent(); // 204 No Content on successful deletion
    }
    catch (Exception ex)
    {
        // Handle exceptions and return a relevant error response
        return Results.BadRequest("An error occurred while deleting the order.");
    }
});



// MENU ITEMS


// GET MENU ITEMS
app.MapGet("/api/menuItems", async (HipHopPizzaandWingsDbContext db) =>
{
    var menuItems = await db.MenuItems.ToListAsync();

    if (menuItems == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(menuItems);
});


// GET SINGLE MENU ITEM
app.MapGet("/api/menuItems/{menuItemId}", async (HipHopPizzaandWingsDbContext db, int menuItemId) =>
{
    var menuItem = await db.MenuItems.FindAsync(menuItemId);

    if (menuItem == null)
    {
        return Results.NotFound("Menu item not found");
    }

    return Results.Ok(menuItem);
});


// ADD MENU ITEM TO ORDER
app.MapPost("/api/order/{orderId}/menuItem/{menuItemId}", async (HipHopPizzaandWingsDbContext db, int orderId, int menuItemId) =>
{
    var order = await db.Orders
        .Include(o => o.MenuItems)
        .FirstOrDefaultAsync(o => o.OrderId == orderId);

    if (order == null)
    {
        return Results.NotFound("Order not found");
    }

    var itemToAdd = await db.MenuItems.FindAsync(menuItemId);

    if (itemToAdd == null)
    {
        return Results.NotFound("Item not found");
    }

    order?.MenuItems?.Add(itemToAdd);

    await db.SaveChangesAsync();

    return Results.Ok(order);
});


// EDIT MENU ITEM
app.MapPut("/api/menuItem/{menuItemId}", async (HipHopPizzaandWingsDbContext db, int menuItemId, MenuItem menuItem) =>
{
    var itemToUpdate = await db.MenuItems.FirstOrDefaultAsync(i => i.MenuItemId == menuItemId);
    if (itemToUpdate == null)
    {
        return Results.NotFound();
    }

    itemToUpdate.Name = menuItem.Name;
    itemToUpdate.Description = menuItem.Description;
    itemToUpdate.Price = menuItem.Price;

    db.SaveChanges();
    return Results.Ok(itemToUpdate);
});


// REMOVE MENU ITEM FROM ORDER
app.MapDelete("/api/orders/{orderId}/menuItems/{menuItemId}", async (HipHopPizzaandWingsDbContext db, int orderId, int menuItemId) =>
{
    var order = await db.Orders
       .Include(o => o.MenuItems)
       .FirstOrDefaultAsync(o => o.OrderId == orderId);

    if (order == null)
    {
        return Results.NotFound("Order not found");
    }

    var itemToRemove = db.MenuItems.Find(menuItemId);

    if (itemToRemove == null)
    {
        return Results.NotFound("Item not found");
    }

    order.MenuItems.Remove(itemToRemove);
    db.SaveChanges();
    return Results.Ok(order);
});



// PAYMENT TYPES


// GET PAYMENT TYPES
app.MapGet("/api/paymentType", async (HipHopPizzaandWingsDbContext db) =>
{
    var paymentTypes = await db.PaymentTypes.ToListAsync();

    if (paymentTypes == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(paymentTypes);
});


// ADD A PAYMENT TYPE
app.MapPost("/api/paymentType", async (HipHopPizzaandWingsDbContext db, PaymentType newPaymentType) =>
{
    // Check if the provided newPaymentType is valid
    if (newPaymentType == null)
    {
        return Results.BadRequest("Invalid data provided.");
    }

    // Add the new payment type to the database
    db.PaymentTypes.Add(newPaymentType);
    await db.SaveChangesAsync();

    // Return a "Created" response with the newly created payment type
    return Results.Created($"/api/paymentType/{newPaymentType.PaymentTypeId}", newPaymentType);
});


// DELETE A PAYMENT TYPE
app.MapDelete("/api/paymentType/{paymentTypeId}", async (HipHopPizzaandWingsDbContext db, int paymentTypeId) =>
{
    // Find the payment type by its ID
    var paymentType = await db.PaymentTypes.FindAsync(paymentTypeId);

    if (paymentType == null)
    {
        return Results.NotFound("Payment type not found");
    }

    // Remove the payment type from the database
    db.PaymentTypes.Remove(paymentType);
    await db.SaveChangesAsync();

    // Return a "No Content" response to indicate successful deletion
    return Results.NoContent();
});
app.Run();

