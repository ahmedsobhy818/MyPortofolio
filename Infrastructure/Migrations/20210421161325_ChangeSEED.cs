using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ChangeSEED : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PortofolioItem",
                keyColumn: "Id",
                keyValue: new Guid("ade9d913-17c2-4d10-b6b9-f91069090fec"));

            migrationBuilder.DeleteData(
                table: "Owner",
                keyColumn: "Id",
                keyValue: new Guid("1864e00e-dbf5-44de-9a47-4ae565769e0b"));

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: new Guid("cd6912bb-7c91-4e32-a3f7-32b65340083e"));

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "Country", "Number", "Street" },
                values: new object[] { new Guid("17587ccc-e504-47f6-b5ef-8e5e0f30665a"), "Cairo", "Egypt", 1, "Ibrahim Bik Al Kabeer" });

            migrationBuilder.InsertData(
                table: "Owner",
                columns: new[] { "Id", "AddressId", "Avatar", "FullName", "Job", "UserId" },
                values: new object[] { new Guid("a1f1315c-2170-4f1c-8c54-bd2748e4d246"), new Guid("17587ccc-e504-47f6-b5ef-8e5e0f30665a"), "dafault.jpg", "go to Home/Index to login", "Dashboard/Create(Edit)Profile - Dashboard/ShowPortofolioItems - Portofolio/Index/Guid? ", null });

            migrationBuilder.InsertData(
                table: "PortofolioItem",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "OwnerId" },
                values: new object[] { new Guid("c8aa18cd-dbf0-4455-b74e-f57be14245de"), "description of portrofolio1", "portofolio1.jpg", "portofolio1", new Guid("a1f1315c-2170-4f1c-8c54-bd2748e4d246") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PortofolioItem",
                keyColumn: "Id",
                keyValue: new Guid("c8aa18cd-dbf0-4455-b74e-f57be14245de"));

            migrationBuilder.DeleteData(
                table: "Owner",
                keyColumn: "Id",
                keyValue: new Guid("a1f1315c-2170-4f1c-8c54-bd2748e4d246"));

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: new Guid("17587ccc-e504-47f6-b5ef-8e5e0f30665a"));

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "Country", "Number", "Street" },
                values: new object[] { new Guid("cd6912bb-7c91-4e32-a3f7-32b65340083e"), "Cairo", "Egypt", 1, "Ibrahim Bik Al Kabeer" });

            migrationBuilder.InsertData(
                table: "Owner",
                columns: new[] { "Id", "AddressId", "Avatar", "FullName", "Job", "UserId" },
                values: new object[] { new Guid("1864e00e-dbf5-44de-9a47-4ae565769e0b"), new Guid("cd6912bb-7c91-4e32-a3f7-32b65340083e"), "avatar.jpg", "Ahmed Sobhy", ".Net Full Stack Developer", null });

            migrationBuilder.InsertData(
                table: "PortofolioItem",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "OwnerId" },
                values: new object[] { new Guid("ade9d913-17c2-4d10-b6b9-f91069090fec"), "responsive website using latest microsof technologies", "portofolio1.jpg", "Asp.net web development", new Guid("1864e00e-dbf5-44de-9a47-4ae565769e0b") });
        }
    }
}
