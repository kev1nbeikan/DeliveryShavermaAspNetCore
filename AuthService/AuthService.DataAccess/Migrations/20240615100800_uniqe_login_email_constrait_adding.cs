using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class uniqe_login_email_constrait_adding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UsersAuth_Email",
                table: "UsersAuth",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoreAuth_Login",
                table: "StoreAuth",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurierAuth_Login",
                table: "CurierAuth",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersAuth_Email",
                table: "UsersAuth");

            migrationBuilder.DropIndex(
                name: "IX_StoreAuth_Login",
                table: "StoreAuth");

            migrationBuilder.DropIndex(
                name: "IX_CurierAuth_Login",
                table: "CurierAuth");
        }
    }
}
