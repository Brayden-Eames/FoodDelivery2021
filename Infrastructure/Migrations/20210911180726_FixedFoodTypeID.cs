using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class FixedFoodTypeID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_FoodType_FoodTypeId",
                table: "MenuItem");

            migrationBuilder.DropColumn(
                name: "FoodCategoryId",
                table: "MenuItem");

            migrationBuilder.AlterColumn<int>(
                name: "FoodTypeId",
                table: "MenuItem",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_FoodType_FoodTypeId",
                table: "MenuItem",
                column: "FoodTypeId",
                principalTable: "FoodType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_FoodType_FoodTypeId",
                table: "MenuItem");

            migrationBuilder.AlterColumn<int>(
                name: "FoodTypeId",
                table: "MenuItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "FoodCategoryId",
                table: "MenuItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_FoodType_FoodTypeId",
                table: "MenuItem",
                column: "FoodTypeId",
                principalTable: "FoodType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
