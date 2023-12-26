using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagmentWithIdentity.Migrations
{
    public partial class AssignAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [Security].[UserRole] (UserId,RoleId)   SELECT '66429bb4-faef-4940-b7b9-fe35ae7ea6b4',Id FROM [Security].[Roles]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Security].[UserRole] Where UserId='66429bb4-faef-4940-b7b9-fe35ae7ea6b4' ");
        }
    }
}
