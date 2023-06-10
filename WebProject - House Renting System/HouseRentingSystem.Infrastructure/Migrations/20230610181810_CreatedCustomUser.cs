using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Infrastructure.Migrations
{
    public partial class CreatedCustomUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af722ef4-6a97-4390-b61b-bdc917a52023", "Teodor", "Leslie", "AQAAAAEAACcQAAAAECuMbFlsGHhZi3r3DwufmndiZKHisapo5D3xLm+m82EaCq2RQFn0PGPFgI9QvHz83w==", "ffb9c620-7700-4ed8-ad28-54ef9c2d2649" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fd07f267-629a-4d11-9e8b-28217f76e78b", "Linda", "Michaels", "AQAAAAEAACcQAAAAEIIqQDJzWudSXWhvo6Ou/Zk5viId0aENPf8Dt730CMwk5tqlI9Eqb79stXKfUJfLuQ==", "7571c0c7-2735-4486-9a05-eb9b44ded46b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "96bf84de-44e8-4c52-b88b-614e74115f38", "AQAAAAEAACcQAAAAEFdwkchRJNbguU0zz2G9vRNWiZopbS5+l48lkgwuTyZWjXMAlVEA3wGZAc9JzOHwHg==", "8a603517-f5ca-4f3d-8632-22bcf0e80d8e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ec17a55a-77b9-4be1-960d-ed63f364ab2f", "AQAAAAEAACcQAAAAEM0ml6UyIEwGCvtG4FbyLsTDerUM9KU1GZESEO8Xvm1ukpSspchVZaDla0Iggit7hA==", "7a0e6c94-e646-4b2f-b63c-81a9dd457d93" });
        }
    }
}
