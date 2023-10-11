﻿// <auto-generated />
using System;
using HipHopPizzaandWings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HipHopPizzaandWings.Migrations
{
    [DbContext(typeof(HipHopPizzaandWingsDbContext))]
    partial class HipHopPizzaandWingsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HipHopPizzaandWings.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("employeeEmail")
                        .HasColumnType("text");

                    b.Property<string>("employeeName")
                        .HasColumnType("text");

                    b.Property<string>("employeePassword")
                        .HasColumnType("text");

                    b.Property<bool>("isEmployee")
                        .HasColumnType("boolean");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = 1,
                            employeeEmail = "kblunt@kb.com",
                            employeeName = "Kyle Blunt",
                            employeePassword = "1234",
                            isEmployee = true
                        });
                });

            modelBuilder.Entity("HipHopPizzaandWings.Models.MenuItem", b =>
                {
                    b.Property<int>("MenuItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MenuItemId"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("MenuItemId");

                    b.ToTable("MenuItems");

                    b.HasData(
                        new
                        {
                            MenuItemId = 1,
                            Description = "Hot Chicken with Honey Drizzle",
                            Name = "Pizza",
                            Price = 14.99m
                        },
                        new
                        {
                            MenuItemId = 2,
                            Description = "Double Fried Bone-In Flats & Drums",
                            Name = "Wings",
                            Price = 10.99m
                        },
                        new
                        {
                            MenuItemId = 3,
                            Description = "Homemade Chocolate Chip Cookies",
                            Name = "Cookies",
                            Price = 7.99m
                        });
                });

            modelBuilder.Entity("HipHopPizzaandWings.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderId"));

                    b.Property<string>("CustomerEmail")
                        .HasColumnType("text");

                    b.Property<string>("CustomerName")
                        .HasColumnType("text");

                    b.Property<string>("CustomerPhone")
                        .HasColumnType("text");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("OrderClosed")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("OrderCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("OrderStatusId")
                        .HasColumnType("integer");

                    b.Property<int>("Revenue")
                        .HasColumnType("integer");

                    b.Property<int>("ReviewScore")
                        .HasColumnType("integer");

                    b.Property<decimal>("Tip")
                        .HasColumnType("numeric");

                    b.HasKey("OrderId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("OrderStatusId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            CustomerEmail = "thehut@pizza.com",
                            CustomerName = "Pizz Hut",
                            CustomerPhone = "281-330-8004",
                            EmployeeId = 1,
                            OrderCreated = new DateTime(2023, 10, 10, 17, 42, 4, 83, DateTimeKind.Local).AddTicks(3491),
                            Revenue = 0,
                            ReviewScore = 3,
                            Tip = 2.00m
                        },
                        new
                        {
                            OrderId = 2,
                            CustomerEmail = "ml@ml.com",
                            CustomerName = "Meg Lee",
                            CustomerPhone = "281-330-8004",
                            EmployeeId = 1,
                            OrderCreated = new DateTime(2023, 10, 10, 19, 42, 4, 83, DateTimeKind.Local).AddTicks(3499),
                            Revenue = 0,
                            ReviewScore = 5,
                            Tip = 5.00m
                        });
                });

            modelBuilder.Entity("HipHopPizzaandWings.Models.OrderStatus", b =>
                {
                    b.Property<int>("OrderStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderStatusId"));

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.HasKey("OrderStatusId");

                    b.ToTable("OrderStatus");

                    b.HasData(
                        new
                        {
                            OrderStatusId = 1,
                            Status = "Open"
                        },
                        new
                        {
                            OrderStatusId = 2,
                            Status = "Closed"
                        });
                });

            modelBuilder.Entity("HipHopPizzaandWings.Models.OrderType", b =>
                {
                    b.Property<int>("OrderTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderTypeId"));

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("OrderTypeId");

                    b.ToTable("OrderTypes");

                    b.HasData(
                        new
                        {
                            OrderTypeId = 1,
                            Type = "Walk In"
                        },
                        new
                        {
                            OrderTypeId = 2,
                            Type = "Call In"
                        });
                });

            modelBuilder.Entity("HipHopPizzaandWings.Models.PaymentType", b =>
                {
                    b.Property<int>("PaymentTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PaymentTypeId"));

                    b.Property<int?>("OrderId")
                        .HasColumnType("integer");

                    b.Property<string>("paymentType")
                        .HasColumnType("text");

                    b.HasKey("PaymentTypeId");

                    b.HasIndex("OrderId");

                    b.ToTable("PaymentTypes");

                    b.HasData(
                        new
                        {
                            PaymentTypeId = 1,
                            paymentType = "Cash"
                        },
                        new
                        {
                            PaymentTypeId = 2,
                            paymentType = "Credit Card"
                        });
                });

            modelBuilder.Entity("MenuItemOrder", b =>
                {
                    b.Property<int>("MenuItemsMenuItemId")
                        .HasColumnType("integer");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.HasKey("MenuItemsMenuItemId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("MenuItemOrder");
                });

            modelBuilder.Entity("OrderOrderType", b =>
                {
                    b.Property<int>("OrdersOrderId")
                        .HasColumnType("integer");

                    b.Property<int>("TypeOrderTypeId")
                        .HasColumnType("integer");

                    b.HasKey("OrdersOrderId", "TypeOrderTypeId");

                    b.HasIndex("TypeOrderTypeId");

                    b.ToTable("OrderOrderType");
                });

            modelBuilder.Entity("HipHopPizzaandWings.Models.Order", b =>
                {
                    b.HasOne("HipHopPizzaandWings.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HipHopPizzaandWings.Models.OrderStatus", null)
                        .WithMany("Order")
                        .HasForeignKey("OrderStatusId");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HipHopPizzaandWings.Models.PaymentType", b =>
                {
                    b.HasOne("HipHopPizzaandWings.Models.Order", null)
                        .WithMany("paymentType")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("MenuItemOrder", b =>
                {
                    b.HasOne("HipHopPizzaandWings.Models.MenuItem", null)
                        .WithMany()
                        .HasForeignKey("MenuItemsMenuItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HipHopPizzaandWings.Models.Order", null)
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OrderOrderType", b =>
                {
                    b.HasOne("HipHopPizzaandWings.Models.Order", null)
                        .WithMany()
                        .HasForeignKey("OrdersOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HipHopPizzaandWings.Models.OrderType", null)
                        .WithMany()
                        .HasForeignKey("TypeOrderTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HipHopPizzaandWings.Models.Order", b =>
                {
                    b.Navigation("paymentType");
                });

            modelBuilder.Entity("HipHopPizzaandWings.Models.OrderStatus", b =>
                {
                    b.Navigation("Order");
                });
#pragma warning restore 612, 618
        }
    }
}
