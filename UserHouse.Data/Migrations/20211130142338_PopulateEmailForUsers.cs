using Microsoft.EntityFrameworkCore.Migrations;

namespace UserHouse.Data.Migrations
{
    public partial class PopulateEmailForUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"Update Users SET Email='superadmin@gmail.com' WHERE Id='1'");
            migrationBuilder.Sql($"Update Users SET Email='admin@gmail.com' WHERE Id='2'");
            migrationBuilder.Sql($"Update Users SET Email='basicuser@gmail.com' WHERE Id='3'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM Users WHERE Id='1';");
            migrationBuilder.Sql($"DELETE FROM Users WHERE Id='2';");
            migrationBuilder.Sql($"DELETE FROM Users WHERE Id='3';");
        }
    }
}
