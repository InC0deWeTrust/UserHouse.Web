using Microsoft.EntityFrameworkCore.Migrations;
using System;
using UserHouse.Infrastructure.Entities.Permissions;
using UserHouse.Infrastructure.Entities.Roles;

namespace UserHouse.Data.Migrations
{
    public partial class PopulateRolesAndPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"INSERT INTO Users (Id,FirstName,LastName,DateOfBirth) VALUES('1', 'SuperAdminUser', 'SuperAdminUser', '{DateTime.Now.ToString("yyyy-MM-dd")}')");
            migrationBuilder.Sql($"INSERT INTO Users (Id,FirstName,LastName,DateOfBirth) VALUES('2', 'AdminUser', 'AdminUser', '{DateTime.Now.ToString("yyyy-MM-dd")}')");
            migrationBuilder.Sql($"INSERT INTO Users (Id,FirstName,LastName,DateOfBirth) VALUES('3', 'BasicUser', 'BasicUser', '{DateTime.Now.ToString("yyyy-MM-dd")}')");
            migrationBuilder.Sql($"INSERT INTO Roles (Id,RoleName) VALUES('1', '{RoleEnum.Super}')");
            migrationBuilder.Sql($"INSERT INTO Roles (Id,RoleName) VALUES('2', '{RoleEnum.Admin}')");
            migrationBuilder.Sql($"INSERT INTO Roles (Id,RoleName) VALUES('3', '{RoleEnum.Basic}')");
            migrationBuilder.Sql($"INSERT INTO Permissions (Id,PermissionName) VALUES('1', '{PermissionEnum.Read}')");
            migrationBuilder.Sql($"INSERT INTO Permissions (Id,PermissionName) VALUES('2', '{PermissionEnum.Create}')");
            migrationBuilder.Sql($"INSERT INTO Permissions (Id,PermissionName) VALUES('3', '{PermissionEnum.Delete}')");
            migrationBuilder.Sql($"INSERT INTO Permissions (Id,PermissionName) VALUES('4', '{PermissionEnum.Update}')");
            migrationBuilder.Sql($"INSERT INTO UserRoles (Id,UserId,RoleId) VALUES('1', '1', '1')");
            migrationBuilder.Sql($"INSERT INTO UserRoles (Id,UserId,RoleId) VALUES('2', '2', '2')");
            migrationBuilder.Sql($"INSERT INTO UserRoles (Id,UserId,RoleId) VALUES('3', '3', '3')");
            migrationBuilder.Sql($"INSERT INTO RolePermissions (Id,RoleId,PermissionId) VALUES('1', '1', '1')");
            migrationBuilder.Sql($"INSERT INTO RolePermissions (Id,RoleId,PermissionId) VALUES('2', '1', '2')");
            migrationBuilder.Sql($"INSERT INTO RolePermissions (Id,RoleId,PermissionId) VALUES('3', '1', '3')");
            migrationBuilder.Sql($"INSERT INTO RolePermissions (Id,RoleId,PermissionId) VALUES('4', '1', '4')");
            migrationBuilder.Sql($"INSERT INTO RolePermissions (Id,RoleId,PermissionId) VALUES('5', '2', '1')");
            migrationBuilder.Sql($"INSERT INTO RolePermissions (Id,RoleId,PermissionId) VALUES('6', '2', '2')");
            migrationBuilder.Sql($"INSERT INTO RolePermissions (Id,RoleId,PermissionId) VALUES('7', '3', '1')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM Users WHERE Id='1';");
            migrationBuilder.Sql($"DELETE FROM Users WHERE Id='2';");
            migrationBuilder.Sql($"DELETE FROM Users WHERE Id='3';");
            migrationBuilder.Sql($"DELETE FROM Roles WHERE Id='1';");
            migrationBuilder.Sql($"DELETE FROM Roles WHERE Id='2';");
            migrationBuilder.Sql($"DELETE FROM Roles WHERE Id='3';");
            migrationBuilder.Sql($"DELETE FROM Permissions WHERE Id='1';");
            migrationBuilder.Sql($"DELETE FROM Permissions WHERE Id='2';");
            migrationBuilder.Sql($"DELETE FROM Permissions WHERE Id='3';");
            migrationBuilder.Sql($"DELETE FROM Permissions WHERE Id='4';");
            migrationBuilder.Sql($"DELETE FROM UserRoles WHERE Id='1';");
            migrationBuilder.Sql($"DELETE FROM UserRoles WHERE Id='2';");
            migrationBuilder.Sql($"DELETE FROM UserRoles WHERE Id='3';");
            migrationBuilder.Sql($"DELETE FROM RolePermissions WHERE Id='1';");
            migrationBuilder.Sql($"DELETE FROM RolePermissions WHERE Id='2';");
            migrationBuilder.Sql($"DELETE FROM RolePermissions WHERE Id='3';");
            migrationBuilder.Sql($"DELETE FROM RolePermissions WHERE Id='4';");
            migrationBuilder.Sql($"DELETE FROM RolePermissions WHERE Id='5';");
            migrationBuilder.Sql($"DELETE FROM RolePermissions WHERE Id='6';");
            migrationBuilder.Sql($"DELETE FROM RolePermissions WHERE Id='7';");
        }
    }
}
