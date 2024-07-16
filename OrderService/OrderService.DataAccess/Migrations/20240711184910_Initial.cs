using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CanceledOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LastStatus = table.Column<int>(type: "integer", nullable: false),
                    ReasonOfCanceled = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CanceledDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WhoCanceled = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourierId = table.Column<Guid>(type: "uuid", nullable: false),
                    StoreId = table.Column<Guid>(type: "uuid", nullable: false),
                    Basket = table.Column<string>(type: "jsonb", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CookingTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    DeliveryTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CookingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Cheque = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanceledOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrentOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    StoreAddress = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    ClientAddress = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    CourierNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    ClientNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourierId = table.Column<Guid>(type: "uuid", nullable: false),
                    StoreId = table.Column<Guid>(type: "uuid", nullable: false),
                    Basket = table.Column<string>(type: "jsonb", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CookingTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    DeliveryTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CookingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Cheque = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LastOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourierId = table.Column<Guid>(type: "uuid", nullable: false),
                    StoreId = table.Column<Guid>(type: "uuid", nullable: false),
                    Basket = table.Column<string>(type: "jsonb", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CookingTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    DeliveryTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CookingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Cheque = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LastOrders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CanceledOrders_ClientId",
                table: "CanceledOrders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_CanceledOrders_CourierId",
                table: "CanceledOrders",
                column: "CourierId");

            migrationBuilder.CreateIndex(
                name: "IX_CanceledOrders_StoreId",
                table: "CanceledOrders",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentOrders_ClientId",
                table: "CurrentOrders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentOrders_CourierId",
                table: "CurrentOrders",
                column: "CourierId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentOrders_StoreId",
                table: "CurrentOrders",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_LastOrders_ClientId",
                table: "LastOrders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_LastOrders_CourierId",
                table: "LastOrders",
                column: "CourierId");

            migrationBuilder.CreateIndex(
                name: "IX_LastOrders_StoreId",
                table: "LastOrders",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CanceledOrders");

            migrationBuilder.DropTable(
                name: "CurrentOrders");

            migrationBuilder.DropTable(
                name: "LastOrders");
        }
    }
}
