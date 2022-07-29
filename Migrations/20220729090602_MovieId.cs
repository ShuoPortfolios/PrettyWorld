using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrettyWorld.Migrations
{
    public partial class MovieId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LiveIn",
                table: "MyProfile",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WatchDate = table.Column<DateTime>(type: "date", nullable: false),
                    MovieType = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Trailer = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Director = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TopCast = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Review = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(2,1)", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieName);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.AlterColumn<string>(
                name: "LiveIn",
                table: "MyProfile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
