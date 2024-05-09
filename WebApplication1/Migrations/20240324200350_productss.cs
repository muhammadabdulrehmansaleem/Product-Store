using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class productss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0c8a080-0346-4eeb-a24a-4ffbe5d0e30d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbdc77c8-3748-46fc-a63d-9171c7859bf1");

            migrationBuilder.AddColumn<byte[]>(
                name: "Shape",
                table: "Products",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7f571d5b-66ec-46da-83b4-dd7bfc58112c", "2", "User", "User" },
                    { "dd3f8318-f92b-4bd4-96e1-6de092f2e330", "1", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f571d5b-66ec-46da-83b4-dd7bfc58112c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd3f8318-f92b-4bd4-96e1-6de092f2e330");

            migrationBuilder.DropColumn(
                name: "Shape",
                table: "Products");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a0c8a080-0346-4eeb-a24a-4ffbe5d0e30d", "1", "Admin", "Admin" },
                    { "bbdc77c8-3748-46fc-a63d-9171c7859bf1", "2", "User", "User" }
                });
        }
    }
}
