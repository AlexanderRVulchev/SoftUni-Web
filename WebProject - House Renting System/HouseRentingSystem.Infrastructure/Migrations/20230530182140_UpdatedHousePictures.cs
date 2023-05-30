using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Infrastructure.Migrations
{
    public partial class UpdatedHousePictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://images.coolhouseplans.com/plans/44207/44207-b600.jpg");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://i0.wp.com/smallhouse-design.com/wp-content/uploads/2020/04/Cool-House-Plans-316-Square-Meters-4-Bedrooms.jpg?fit=1880%2C1180&ssl=1");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://media.architecturaldigest.com/photos/61b0ce48dccdb75fa170f8f7/16:9/w_2560%2Cc_limit/PurpleCherry_Williams_0012.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de5703db-e23b-4d13-b0a7-5b9b8d6a6d9c", "AQAAAAEAACcQAAAAEMWCHSFoQVaZZP5MLW9HVPz3xviYZfz3GF+15WBcmZyCmm7Bwit0j8ICLyZLu8RGpQ==", "d2f2f739-f30d-4bf9-a17a-15be3be2227f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7f098f51-d4e6-4b89-929b-bec4b7d72fb4", "AQAAAAEAACcQAAAAEHMajeQcHU+/QDFqvGIfqTjHvyOb/9kcb06n/+pS7Jkz+lE/ul1CuYkb4dCL3iAEVQ==", "4525ff14-8ad0-4c5e-a96f-5b2b59263d58" });

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://www.luxury-architecture.net/wpcontent/uploads/2017/12/1513217889-7597-FAIRWAYS-010.jpg");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a&o=&hp=1");

            migrationBuilder.UpdateData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg");
        }
    }
}
