using Microsoft.EntityFrameworkCore.Migrations;

namespace App.OSS.API.Migrations
{
    public partial class initDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ossapp");

            migrationBuilder.CreateTable(
                name: "file_asset",
                schema: "ossapp",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    size = table.Column<long>(nullable: false),
                    file_ext = table.Column<string>(nullable: true),
                    file_state = table.Column<int>(nullable: false),
                    creator = table.Column<string>(nullable: true),
                    modifier = table.Column<string>(nullable: true),
                    created_time = table.Column<long>(nullable: false),
                    modified_time = table.Column<long>(nullable: false),
                    url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_file_asset", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "file_asset",
                schema: "ossapp");
        }
    }
}
