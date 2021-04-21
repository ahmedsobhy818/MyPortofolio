using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class OwnerUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PortofolioItem",
                keyColumn: "Id",
                keyValue: new Guid("2279fd18-1cec-4888-bb90-9aac98279b6f"));

            migrationBuilder.DeleteData(
                table: "Owner",
                keyColumn: "Id",
                keyValue: new Guid("4570edce-5924-46af-b36a-d9cc8fbe3d57"));

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: new Guid("daef9fc1-9227-4674-98fb-a3a41716ef6d"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Owner",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Owner");

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "Country", "Number", "Street" },
                values: new object[] { new Guid("daef9fc1-9227-4674-98fb-a3a41716ef6d"), "Cairo", "Egypt", 1, "Ibrahim Bik Al Kabeer" });

            migrationBuilder.InsertData(
                table: "Owner",
                columns: new[] { "Id", "AddressId", "Avatar", "FullName", "Job" },
                values: new object[] { new Guid("4570edce-5924-46af-b36a-d9cc8fbe3d57"), new Guid("daef9fc1-9227-4674-98fb-a3a41716ef6d"), "avatar.jpg", "Ahmed Sobhy", ".Net Full Stack Developer" });

            migrationBuilder.InsertData(
                table: "PortofolioItem",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "OwnerId" },
                values: new object[] { new Guid("2279fd18-1cec-4888-bb90-9aac98279b6f"), "responsive website using latest microsof technologies", "portofolio1.jpg", "Asp.net web development", new Guid("4570edce-5924-46af-b36a-d9cc8fbe3d57") });
        }
    }
}
