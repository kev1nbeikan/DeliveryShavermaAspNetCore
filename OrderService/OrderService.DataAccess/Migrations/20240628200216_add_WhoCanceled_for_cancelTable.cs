using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_WhoCanceled_for_cancelTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WhoCanceled",
                table: "CanceledOrders",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WhoCanceled",
                table: "CanceledOrders");
        }
    }
}
