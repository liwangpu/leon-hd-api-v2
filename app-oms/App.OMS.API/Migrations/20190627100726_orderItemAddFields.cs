using Microsoft.EntityFrameworkCore.Migrations;

namespace App.OMS.API.Migrations
{
    public partial class orderItemAddFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "unit",
                schema: "omsapp",
                table: "order_item",
                newName: "product_unit");

            migrationBuilder.AddColumn<string>(
                name: "product_brand",
                schema: "omsapp",
                table: "order_item",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "product_description",
                schema: "omsapp",
                table: "order_item",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "product_icon",
                schema: "omsapp",
                table: "order_item",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "product_spec_description",
                schema: "omsapp",
                table: "order_item",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "product_spec_icon",
                schema: "omsapp",
                table: "order_item",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "product_brand",
                schema: "omsapp",
                table: "order_item");

            migrationBuilder.DropColumn(
                name: "product_description",
                schema: "omsapp",
                table: "order_item");

            migrationBuilder.DropColumn(
                name: "product_icon",
                schema: "omsapp",
                table: "order_item");

            migrationBuilder.DropColumn(
                name: "product_spec_description",
                schema: "omsapp",
                table: "order_item");

            migrationBuilder.DropColumn(
                name: "product_spec_icon",
                schema: "omsapp",
                table: "order_item");

            migrationBuilder.RenameColumn(
                name: "product_unit",
                schema: "omsapp",
                table: "order_item",
                newName: "unit");
        }
    }
}
