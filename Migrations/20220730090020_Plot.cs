using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrettyWorld.Migrations
{
    public partial class Plot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MoviePicture",
                table: "Movies",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoviePicture",
                table: "Movies");
        }
    }
}
