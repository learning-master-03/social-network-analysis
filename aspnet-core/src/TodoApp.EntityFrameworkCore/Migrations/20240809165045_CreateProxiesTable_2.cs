using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Migrations
{
    /// <inheritdoc />
    public partial class CreateProxiesTable_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FromUrl",
                table: "ProxyJobs",
                newName: "MainUrl");

            migrationBuilder.AddColumn<int>(
                name: "FileType",
                table: "ProxyJobs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileType",
                table: "ProxyJobs");

            migrationBuilder.RenameColumn(
                name: "MainUrl",
                table: "ProxyJobs",
                newName: "FromUrl");
        }
    }
}
