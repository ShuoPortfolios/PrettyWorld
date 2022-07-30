using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrettyWorld.Migrations
{
    public partial class Sound : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Acting",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Immersion",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Plot",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Scene",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sound",
                table: "Movies",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Acting",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Immersion",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Plot",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Scene",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Sound",
                table: "Movies");
        }
    }
}
