using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class delete_check_constraitnt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_CurrentOrder_Status",
                table: "CurrentOrders");

            migrationBuilder.DropCheckConstraint(
                name: "CK_CanceledOrder_LastStatus",
                table: "CanceledOrders");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LastOrders_ClientId",
                table: "LastOrders");

            migrationBuilder.DropIndex(
                name: "IX_LastOrders_CourierId",
                table: "LastOrders");

            migrationBuilder.DropIndex(
                name: "IX_LastOrders_StoreId",
                table: "LastOrders");

            migrationBuilder.DropIndex(
                name: "IX_CurrentOrders_ClientId",
                table: "CurrentOrders");

            migrationBuilder.DropIndex(
                name: "IX_CurrentOrders_CourierId",
                table: "CurrentOrders");

            migrationBuilder.DropIndex(
                name: "IX_CurrentOrders_StoreId",
                table: "CurrentOrders");

            migrationBuilder.DropIndex(
                name: "IX_CanceledOrders_ClientId",
                table: "CanceledOrders");

            migrationBuilder.DropIndex(
                name: "IX_CanceledOrders_CourierId",
                table: "CanceledOrders");

            migrationBuilder.DropIndex(
                name: "IX_CanceledOrders_StoreId",
                table: "CanceledOrders");

            migrationBuilder.AddCheckConstraint(
                name: "CK_CurrentOrder_Status",
                table: "CurrentOrders",
                sql: "\"Status\" >= 0 AND \"Status\" < 5");

            migrationBuilder.AddCheckConstraint(
                name: "CK_CanceledOrder_LastStatus",
                table: "CanceledOrders",
                sql: "\"LastStatus\" >= 1 AND \"LastStatus\" < 5");
        }
    }
}
