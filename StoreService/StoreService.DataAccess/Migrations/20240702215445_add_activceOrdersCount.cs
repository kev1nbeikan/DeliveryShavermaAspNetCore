using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_activceOrdersCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActiveOrdersCount",
                table: "Stores",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveOrdersCount",
                table: "Stores");
        }
    }
}
