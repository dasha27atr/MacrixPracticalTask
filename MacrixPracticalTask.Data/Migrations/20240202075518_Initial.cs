using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MacrixPracticalTask.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    StreetName = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    HouseNumber = table.Column<int>(type: "INTEGER", maxLength: 500, nullable: false),
                    ApartmentNumber = table.Column<int>(type: "INTEGER", maxLength: 1000, nullable: false),
                    PostalCode = table.Column<int>(type: "INTEGER", maxLength: 1000000, nullable: false),
                    Town = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
