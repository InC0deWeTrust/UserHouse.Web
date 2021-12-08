using Microsoft.EntityFrameworkCore.Migrations;

namespace UserHouse.Data.Migrations
{
    public partial class PopulatePasswordsForUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"Update Users SET Password='{BCrypt.Net.BCrypt.HashPassword("super")}' WHERE Id='1'");
            migrationBuilder.Sql($"Update Users SET Password='{BCrypt.Net.BCrypt.HashPassword("admin")}' WHERE Id='2'");
            migrationBuilder.Sql($"Update Users SET Password='{BCrypt.Net.BCrypt.HashPassword("basic")}' WHERE Id='3'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM Users WHERE Id='1';");
            migrationBuilder.Sql($"DELETE FROM Users WHERE Id='2';");
            migrationBuilder.Sql($"DELETE FROM Users WHERE Id='3';");
        }
    }
}
