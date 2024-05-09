using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class productsss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f571d5b-66ec-46da-83b4-dd7bfc58112c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd3f8318-f92b-4bd4-96e1-6de092f2e330");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "03a77107-ca8e-4077-9b57-681004bc0998", "1", "Admin", "Admin" },
                    { "19514ca7-fa94-44ef-b61c-c401a0622e72", "2", "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03a77107-ca8e-4077-9b57-681004bc0998");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19514ca7-fa94-44ef-b61c-c401a0622e72");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7f571d5b-66ec-46da-83b4-dd7bfc58112c", "2", "User", "User" },
                    { "dd3f8318-f92b-4bd4-96e1-6de092f2e330", "1", "Admin", "Admin" }
                });
        }
    }
}
