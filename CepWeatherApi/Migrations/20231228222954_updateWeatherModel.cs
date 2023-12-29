using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CepWeatherApi.Migrations
{
    public partial class updateWeatherModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Fim",
                table: "Weather",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Inicio",
                table: "Weather",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Timezone",
                table: "Weather",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fim",
                table: "Weather");

            migrationBuilder.DropColumn(
                name: "Inicio",
                table: "Weather");

            migrationBuilder.DropColumn(
                name: "Timezone",
                table: "Weather");
        }
    }
}
