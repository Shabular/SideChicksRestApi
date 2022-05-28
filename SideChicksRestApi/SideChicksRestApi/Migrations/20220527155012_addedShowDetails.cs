using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SideChicksRestApi.Migrations
{
    public partial class addedShowDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Shows",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "Shows");
        }
    }
}
