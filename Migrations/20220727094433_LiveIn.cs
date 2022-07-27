using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrettyWorld.Migrations
{
    public partial class LiveIn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyProfile",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProfilePicture = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Introduction = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LiveIn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Mobile = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    WebsiteUrl = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    GithubUrl = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    TwitterUrl = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    InstagramUrl = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    FacebookUrl = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    JavascriptLevel = table.Column<int>(type: "int", nullable: false),
                    HTMLLevel = table.Column<int>(type: "int", nullable: false),
                    CSSLevel = table.Column<int>(type: "int", nullable: false),
                    BootstrapLevel = table.Column<int>(type: "int", nullable: false),
                    AJAXLevel = table.Column<int>(type: "int", nullable: false),
                    CSharpLevel = table.Column<int>(type: "int", nullable: false),
                    JavaLevel = table.Column<int>(type: "int", nullable: false),
                    PythonLevel = table.Column<int>(type: "int", nullable: false),
                    MSSQLLevel = table.Column<int>(type: "int", nullable: false),
                    GitLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyProfile", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyProfile");
        }
    }
}
