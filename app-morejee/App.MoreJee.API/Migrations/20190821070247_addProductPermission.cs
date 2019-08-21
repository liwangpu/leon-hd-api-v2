using Microsoft.EntityFrameworkCore.Migrations;

namespace App.MoreJee.API.Migrations
{
    public partial class addProductPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product_permission",
                schema: "morejeeapp",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    product_id = table.Column<string>(nullable: true),
                    organization_id = table.Column<string>(nullable: true),
                    product_permission_group_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_permission", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_product_permission_organization_id",
                schema: "morejeeapp",
                table: "product_permission",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_permission_product_id",
                schema: "morejeeapp",
                table: "product_permission",
                column: "product_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_permission",
                schema: "morejeeapp");
        }
    }
}
