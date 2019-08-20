using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Basic.API.Migrations
{
    public partial class ChangeErrorAccountFirstName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fist_name",
                schema: "basicapp",
                table: "account",
                newName: "first_name");

            migrationBuilder.RenameIndex(
                name: "ix_account_fist_name",
                schema: "basicapp",
                table: "account",
                newName: "ix_account_first_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "first_name",
                schema: "basicapp",
                table: "account",
                newName: "fist_name");

            migrationBuilder.RenameIndex(
                name: "ix_account_first_name",
                schema: "basicapp",
                table: "account",
                newName: "ix_account_fist_name");
        }
    }
}
