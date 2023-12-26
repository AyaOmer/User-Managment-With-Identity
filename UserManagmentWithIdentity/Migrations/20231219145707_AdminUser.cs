using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagmentWithIdentity.Migrations
{
    public partial class AdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [Security].[Users] ([Id], [FullName], [ProfilePicture], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'66429bb4-faef-4940-b7b9-fe35ae7ea6b4', N'Aya Omar', NULL, N'Admin', N'ADMIN', N'Admin@yahoo.comm', N'ADMIN@YAHOO.COMM', 0, N'AQAAAAEAACcQAAAAEFBXXIDL7cXYRJv3OMq473/gteEL4+uNOr3MJFL9PdJ9c2aL3x/0C0KD9CsKaj5yng==', N'NUIAQPMP4EZLOJ3POOA5M6PXMJLXIC7U', N'65bde995-bc9d-4b0c-bb20-123356764132', NULL, 0, 0, NULL, 1, 0)\r\n");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FORM [Security].[Users] WHERE Id='66429bb4-faef-4940-b7b9-fe35ae7ea6b4'");
        }
    }
}
