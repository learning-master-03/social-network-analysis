using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Migrations
{
    /// <inheritdoc />
    public partial class CreateTikiReviews_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TikiCategoryId",
                table: "TikiProductLinks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "TikiProductLinks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TikiProductLinks_TikiCategoryId",
                table: "TikiProductLinks",
                column: "TikiCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TikiProductLinks_TikiCategories_TikiCategoryId",
                table: "TikiProductLinks",
                column: "TikiCategoryId",
                principalTable: "TikiCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TikiProductLinks_TikiCategories_TikiCategoryId",
                table: "TikiProductLinks");

            migrationBuilder.DropIndex(
                name: "IX_TikiProductLinks_TikiCategoryId",
                table: "TikiProductLinks");

            migrationBuilder.DropColumn(
                name: "TikiCategoryId",
                table: "TikiProductLinks");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "TikiProductLinks");
        }
    }
}
