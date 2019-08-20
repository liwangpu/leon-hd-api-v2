using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.MoreJee.API.Migrations
{
    public partial class initDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "morejeeapp");

            migrationBuilder.CreateTable(
                name: "category",
                schema: "morejeeapp",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    lvalue = table.Column<int>(nullable: false),
                    rvalue = table.Column<int>(nullable: false),
                    parent_id = table.Column<string>(nullable: true),
                    node_type = table.Column<string>(nullable: true),
                    display_index = table.Column<int>(nullable: false),
                    fingerprint = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    icon = table.Column<string>(nullable: true),
                    creator = table.Column<string>(nullable: true),
                    modifier = table.Column<string>(nullable: true),
                    created_time = table.Column<DateTime>(nullable: false),
                    modified_time = table.Column<DateTime>(nullable: false),
                    organization_id = table.Column<string>(nullable: true),
                    resource = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "map",
                schema: "morejeeapp",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    icon = table.Column<string>(nullable: true),
                    creator = table.Column<string>(nullable: true),
                    modifier = table.Column<string>(nullable: true),
                    created_time = table.Column<long>(nullable: false),
                    modified_time = table.Column<long>(nullable: false),
                    organization_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_map", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "material",
                schema: "morejeeapp",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    category_id = table.Column<string>(nullable: true),
                    icon = table.Column<string>(nullable: true),
                    creator = table.Column<string>(nullable: true),
                    modifier = table.Column<string>(nullable: true),
                    created_time = table.Column<long>(nullable: false),
                    modified_time = table.Column<long>(nullable: false),
                    organization_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_material", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "package_map",
                schema: "morejeeapp",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    package = table.Column<string>(nullable: true),
                    dependencies = table.Column<string>(nullable: true),
                    property = table.Column<string>(nullable: true),
                    source_asset_url = table.Column<string>(nullable: true),
                    un_cooked_asset_url = table.Column<string>(nullable: true),
                    win64_cooked_asset_url = table.Column<string>(nullable: true),
                    android_cooked_asset_url = table.Column<string>(nullable: true),
                    ioscooked_asset_url = table.Column<string>(nullable: true),
                    dependency_asset_urls_of_source = table.Column<string>(nullable: true),
                    dependency_asset_urls_of_un_cooked = table.Column<string>(nullable: true),
                    dependency_asset_urls_of_win64_cooked = table.Column<string>(nullable: true),
                    dependency_asset_urls_of_android_cooked = table.Column<string>(nullable: true),
                    dependency_asset_urls_of_ioscooked = table.Column<string>(nullable: true),
                    resource_id = table.Column<string>(nullable: true),
                    resource_type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_package_map", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                schema: "morejeeapp",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    creator = table.Column<string>(nullable: true),
                    modifier = table.Column<string>(nullable: true),
                    created_time = table.Column<long>(nullable: false),
                    modified_time = table.Column<long>(nullable: false),
                    organization_id = table.Column<string>(nullable: true),
                    default_product_spec_id = table.Column<string>(nullable: true),
                    category_id = table.Column<string>(nullable: true),
                    icon = table.Column<string>(nullable: true),
                    max_price = table.Column<decimal>(nullable: false),
                    min_price = table.Column<decimal>(nullable: false),
                    max_partner_price = table.Column<decimal>(nullable: false),
                    min_partner_price = table.Column<decimal>(nullable: false),
                    max_purchase_price = table.Column<decimal>(nullable: false),
                    min_purchase_price = table.Column<decimal>(nullable: false),
                    brand = table.Column<string>(nullable: true),
                    unit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_permission_group",
                schema: "morejeeapp",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    organization_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_permission_group", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "solution",
                schema: "morejeeapp",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    icon = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    creator = table.Column<string>(nullable: true),
                    modifier = table.Column<string>(nullable: true),
                    created_time = table.Column<long>(nullable: false),
                    modified_time = table.Column<long>(nullable: false),
                    organization_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_solution", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "static_mesh",
                schema: "morejeeapp",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    icon = table.Column<string>(nullable: true),
                    creator = table.Column<string>(nullable: true),
                    modifier = table.Column<string>(nullable: true),
                    created_time = table.Column<long>(nullable: false),
                    modified_time = table.Column<long>(nullable: false),
                    organization_id = table.Column<string>(nullable: true),
                    related_product_spec_ids = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_static_mesh", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "texture",
                schema: "morejeeapp",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    icon = table.Column<string>(nullable: true),
                    creator = table.Column<string>(nullable: true),
                    modifier = table.Column<string>(nullable: true),
                    created_time = table.Column<long>(nullable: false),
                    modified_time = table.Column<long>(nullable: false),
                    organization_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_texture", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_spec",
                schema: "morejeeapp",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    creator = table.Column<string>(nullable: true),
                    modifier = table.Column<string>(nullable: true),
                    created_time = table.Column<long>(nullable: false),
                    modified_time = table.Column<long>(nullable: false),
                    organization_id = table.Column<string>(nullable: true),
                    product_id = table.Column<string>(nullable: true),
                    price = table.Column<decimal>(nullable: false),
                    partner_price = table.Column<decimal>(nullable: false),
                    purchase_price = table.Column<decimal>(nullable: false),
                    icon = table.Column<string>(nullable: true),
                    related_static_mesh_ids = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_spec", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_spec_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "morejeeapp",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_permission_item",
                schema: "morejeeapp",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    product_id = table.Column<string>(nullable: true),
                    product_permission_group_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_permission_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_permission_item_product_permission_group_product_pe~",
                        column: x => x.product_permission_group_id,
                        principalSchema: "morejeeapp",
                        principalTable: "product_permission_group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_permission_organ",
                schema: "morejeeapp",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    organization_id = table.Column<string>(nullable: true),
                    product_permission_group_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_permission_organ", x => x.id);
                    table.ForeignKey(
                        name: "fk_product_permission_organ_product_permission_group_product_p~",
                        column: x => x.product_permission_group_id,
                        principalSchema: "morejeeapp",
                        principalTable: "product_permission_group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_product_permission_item_product_permission_group_id",
                schema: "morejeeapp",
                table: "product_permission_item",
                column: "product_permission_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_permission_organ_product_permission_group_id",
                schema: "morejeeapp",
                table: "product_permission_organ",
                column: "product_permission_group_id");

            migrationBuilder.CreateIndex(
                name: "ix_product_spec_product_id",
                schema: "morejeeapp",
                table: "product_spec",
                column: "product_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "category",
                schema: "morejeeapp");

            migrationBuilder.DropTable(
                name: "map",
                schema: "morejeeapp");

            migrationBuilder.DropTable(
                name: "material",
                schema: "morejeeapp");

            migrationBuilder.DropTable(
                name: "package_map",
                schema: "morejeeapp");

            migrationBuilder.DropTable(
                name: "product_permission_item",
                schema: "morejeeapp");

            migrationBuilder.DropTable(
                name: "product_permission_organ",
                schema: "morejeeapp");

            migrationBuilder.DropTable(
                name: "product_spec",
                schema: "morejeeapp");

            migrationBuilder.DropTable(
                name: "solution",
                schema: "morejeeapp");

            migrationBuilder.DropTable(
                name: "static_mesh",
                schema: "morejeeapp");

            migrationBuilder.DropTable(
                name: "texture",
                schema: "morejeeapp");

            migrationBuilder.DropTable(
                name: "product_permission_group",
                schema: "morejeeapp");

            migrationBuilder.DropTable(
                name: "product",
                schema: "morejeeapp");
        }
    }
}
