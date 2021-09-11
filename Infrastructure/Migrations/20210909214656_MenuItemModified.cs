using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class MenuItemModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MenuItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "MenuItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "MenuItem");
        }
    }
}
