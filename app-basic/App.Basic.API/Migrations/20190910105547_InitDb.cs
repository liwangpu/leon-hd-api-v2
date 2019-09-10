using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Basic.API.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "basicapp");

            migrationBuilder.CreateTable(
                name: "access_point",
                schema: "basicapp",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    point_key = table.Column<string>(nullable: true),
                    is_inner = table.Column<int>(nullable: false),
                    apply_oranization_type_ids = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_access_point", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "custom_role",
                schema: "basicapp",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    organization_id = table.Column<string>(nullable: true),
                    access_point_keys = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_custom_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "organization",
                schema: "basicapp",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    lvalue = table.Column<int>(nullable: false),
                    rvalue = table.Column<int>(nullable: false),
                    parent_id = table.Column<string>(nullable: true),
                    fingerprint = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    mail = table.Column<string>(nullable: true),
                    phone = table.Column<string>(nullable: true),
                    active = table.Column<int>(nullable: false),
                    creator = table.Column<string>(nullable: true),
                    modifier = table.Column<string>(nullable: true),
                    created_time = table.Column<long>(nullable: false),
                    modified_time = table.Column<long>(nullable: false),
                    organization_type_id = table.Column<int>(nullable: false),
                    owner_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organization", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "account",
                schema: "basicapp",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    mail = table.Column<string>(nullable: true),
                    phone = table.Column<string>(nullable: true),
                    active = table.Column<int>(nullable: false),
                    creator = table.Column<string>(nullable: true),
                    modifier = table.Column<string>(nullable: true),
                    created_time = table.Column<long>(nullable: false),
                    modified_time = table.Column<long>(nullable: false),
                    legal_person = table.Column<int>(nullable: false, defaultValue: 0),
                    language_type_id = table.Column<int>(nullable: false, defaultValue: 0),
                    organization_id = table.Column<string>(nullable: true),
                    system_role_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_account", x => x.id);
                    table.ForeignKey(
                        name: "fk_account_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "basicapp",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                schema: "basicapp",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    custom_role_id = table.Column<string>(nullable: true),
                    account_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_role", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_role_account_account_id",
                        column: x => x.account_id,
                        principalSchema: "basicapp",
                        principalTable: "account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_account_first_name",
                schema: "basicapp",
                table: "account",
                column: "first_name");

            migrationBuilder.CreateIndex(
                name: "ix_account_legal_person",
                schema: "basicapp",
                table: "account",
                column: "legal_person");

            migrationBuilder.CreateIndex(
                name: "ix_account_mail",
                schema: "basicapp",
                table: "account",
                column: "mail");

            migrationBuilder.CreateIndex(
                name: "ix_account_organization_id",
                schema: "basicapp",
                table: "account",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "ix_account_system_role_id",
                schema: "basicapp",
                table: "account",
                column: "system_role_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_role_account_id",
                schema: "basicapp",
                table: "user_role",
                column: "account_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "access_point",
                schema: "basicapp");

            migrationBuilder.DropTable(
                name: "custom_role",
                schema: "basicapp");

            migrationBuilder.DropTable(
                name: "user_role",
                schema: "basicapp");

            migrationBuilder.DropTable(
                name: "account",
                schema: "basicapp");

            migrationBuilder.DropTable(
                name: "organization",
                schema: "basicapp");
        }
    }
}
