using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.OMS.API.Migrations
{
    public partial class initDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "omsapp");

            migrationBuilder.CreateTable(
                name: "customer",
                schema: "omsapp",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    company = table.Column<string>(nullable: true),
                    mail = table.Column<string>(nullable: true),
                    phone = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    creator = table.Column<string>(nullable: true),
                    modifier = table.Column<string>(nullable: true),
                    created_time = table.Column<DateTime>(nullable: false),
                    modified_time = table.Column<DateTime>(nullable: false),
                    organization_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order",
                schema: "omsapp",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    creator = table.Column<string>(nullable: true),
                    modifier = table.Column<string>(nullable: true),
                    created_time = table.Column<DateTime>(nullable: false),
                    modified_time = table.Column<DateTime>(nullable: false),
                    organization_id = table.Column<string>(nullable: true),
                    order_no = table.Column<string>(nullable: true),
                    total_num = table.Column<int>(nullable: false),
                    total_price = table.Column<decimal>(nullable: false),
                    customer_id = table.Column<string>(nullable: true),
                    contact_name = table.Column<string>(nullable: true),
                    contact_phone = table.Column<string>(nullable: true),
                    contact_mail = table.Column<string>(nullable: true),
                    shipping_address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_customer_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "omsapp",
                        principalTable: "customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order_item",
                schema: "omsapp",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    product_id = table.Column<string>(nullable: true),
                    product_name = table.Column<string>(nullable: true),
                    product_spec_id = table.Column<string>(nullable: true),
                    product_spec_name = table.Column<string>(nullable: true),
                    unit = table.Column<string>(nullable: true),
                    num = table.Column<int>(nullable: false),
                    unit_price = table.Column<decimal>(nullable: false),
                    remark = table.Column<string>(nullable: true),
                    order_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_item_order_order_id",
                        column: x => x.order_id,
                        principalSchema: "omsapp",
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_order_customer_id",
                schema: "omsapp",
                table: "order",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_item_order_id",
                schema: "omsapp",
                table: "order_item",
                column: "order_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_item",
                schema: "omsapp");

            migrationBuilder.DropTable(
                name: "order",
                schema: "omsapp");

            migrationBuilder.DropTable(
                name: "customer",
                schema: "omsapp");
        }
    }
}
