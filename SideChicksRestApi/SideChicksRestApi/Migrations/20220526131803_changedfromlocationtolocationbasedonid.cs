using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SideChicksRestApi.Migrations
{
    public partial class changedfromlocationtolocationbasedonid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shows_Locations_LocationId",
                table: "Shows");

            migrationBuilder.DropIndex(
                name: "IX_Shows_LocationId",
                table: "Shows");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Shows_LocationId",
                table: "Shows",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_Locations_LocationId",
                table: "Shows",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
