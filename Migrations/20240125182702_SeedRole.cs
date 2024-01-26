using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class SeedRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "175cd42c-b124-4db6-b24b-ac4349747b04", "6168c6a0-1caf-4fd5-a0a8-4d86b9e1ee1b", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "537477c1-c765-4b41-9bd1-c002b29a4ffb", "96ba0b47-5048-4a4c-9910-ec9fb4273d48", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "175cd42c-b124-4db6-b24b-ac4349747b04");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "537477c1-c765-4b41-9bd1-c002b29a4ffb");
        }
    }
}
