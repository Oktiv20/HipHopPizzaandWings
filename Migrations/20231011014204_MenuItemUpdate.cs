using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HipHopPizzaandWings.Migrations
{
    public partial class MenuItemUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemOrder_MenuItems_MenuItemId",
                table: "MenuItemOrder");

            migrationBuilder.RenameColumn(
                name: "menuItemName",
                table: "MenuItems",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "MenuItemId",
                table: "MenuItemOrder",
                newName: "MenuItemsMenuItemId");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "OrderCreated",
                value: new DateTime(2023, 10, 10, 17, 42, 4, 83, DateTimeKind.Local).AddTicks(3491));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "OrderCreated",
                value: new DateTime(2023, 10, 10, 19, 42, 4, 83, DateTimeKind.Local).AddTicks(3499));

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemOrder_MenuItems_MenuItemsMenuItemId",
                table: "MenuItemOrder",
                column: "MenuItemsMenuItemId",
                principalTable: "MenuItems",
                principalColumn: "MenuItemId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemOrder_MenuItems_MenuItemsMenuItemId",
                table: "MenuItemOrder");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "MenuItems",
                newName: "menuItemName");

            migrationBuilder.RenameColumn(
                name: "MenuItemsMenuItemId",
                table: "MenuItemOrder",
                newName: "MenuItemId");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "OrderCreated",
                value: new DateTime(2023, 10, 9, 18, 18, 53, 916, DateTimeKind.Local).AddTicks(6124));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "OrderCreated",
                value: new DateTime(2023, 10, 9, 20, 18, 53, 916, DateTimeKind.Local).AddTicks(6132));

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemOrder_MenuItems_MenuItemId",
                table: "MenuItemOrder",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "MenuItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
