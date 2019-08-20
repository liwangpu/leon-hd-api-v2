using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Basic.API.Migrations
{
    public partial class SplitAccountName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_account_name",
                schema: "basicapp",
                table: "account");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "basicapp",
                table: "account",
                newName: "last_name");

            migrationBuilder.AddColumn<string>(
                name: "fist_name",
                schema: "basicapp",
                table: "account",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_account_fist_name",
                schema: "basicapp",
                table: "account",
                column: "fist_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_account_fist_name",
                schema: "basicapp",
                table: "account");

            migrationBuilder.DropColumn(
                name: "fist_name",
                schema: "basicapp",
                table: "account");

            migrationBuilder.RenameColumn(
                name: "last_name",
                schema: "basicapp",
                table: "account",
                newName: "name");

            migrationBuilder.CreateIndex(
                name: "ix_account_name",
                schema: "basicapp",
                table: "account",
                column: "name");
        }
    }
}
