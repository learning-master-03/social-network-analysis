using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatePuppeterr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PuppeteerConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Headless = table.Column<bool>(type: "bit", nullable: false),
                    Args = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ViewportWidth = table.Column<int>(type: "int", nullable: false),
                    ViewportHeight = table.Column<int>(type: "int", nullable: false),
                    ExecutablePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SlowMo = table.Column<int>(type: "int", nullable: false),
                    Timeout = table.Column<int>(type: "int", nullable: false),
                    UserDataDir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IgnoreHTTPSErrors = table.Column<bool>(type: "bit", nullable: false),
                    Devtools = table.Column<bool>(type: "bit", nullable: false),
                    IgnoreDefaultArgs = table.Column<bool>(type: "bit", nullable: false),
                    IgnoredDefaultArgs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PuppeteerConfigurations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PuppeteerConfigurations");
        }
    }
}
