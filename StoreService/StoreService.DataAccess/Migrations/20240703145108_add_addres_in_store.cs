using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_addres_in_store : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Stores",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Stores");
        }
    }
}
