using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Migrations
{
    /// <inheritdoc />
    public partial class CreateTikiReviews_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TikiProductImages_TikiProducts_TikiProductId",
                table: "TikiProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_TikiReviews_TikiProducts_TikiProductId",
                table: "TikiReviews");

            migrationBuilder.RenameColumn(
                name: "TikiProductId",
                table: "TikiReviews",
                newName: "tikiProductId");

            migrationBuilder.RenameIndex(
                name: "IX_TikiReviews_TikiProductId",
                table: "TikiReviews",
                newName: "IX_TikiReviews_tikiProductId");

            migrationBuilder.RenameColumn(
                name: "TikiProductId",
                table: "TikiProductImages",
                newName: "tikiProductId");

            migrationBuilder.RenameIndex(
                name: "IX_TikiProductImages_TikiProductId",
                table: "TikiProductImages",
                newName: "IX_TikiProductImages_tikiProductId");

            migrationBuilder.AlterColumn<Guid>(
                name: "tikiProductId",
                table: "TikiReviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "tikiProductId",
                table: "TikiProductImages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TikiProductImages_TikiProducts_tikiProductId",
                table: "TikiProductImages",
                column: "tikiProductId",
                principalTable: "TikiProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TikiReviews_TikiProducts_tikiProductId",
                table: "TikiReviews",
                column: "tikiProductId",
                principalTable: "TikiProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TikiProductImages_TikiProducts_tikiProductId",
                table: "TikiProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_TikiReviews_TikiProducts_tikiProductId",
                table: "TikiReviews");

            migrationBuilder.RenameColumn(
                name: "tikiProductId",
                table: "TikiReviews",
                newName: "TikiProductId");

            migrationBuilder.RenameIndex(
                name: "IX_TikiReviews_tikiProductId",
                table: "TikiReviews",
                newName: "IX_TikiReviews_TikiProductId");

            migrationBuilder.RenameColumn(
                name: "tikiProductId",
                table: "TikiProductImages",
                newName: "TikiProductId");

            migrationBuilder.RenameIndex(
                name: "IX_TikiProductImages_tikiProductId",
                table: "TikiProductImages",
                newName: "IX_TikiProductImages_TikiProductId");

            migrationBuilder.AlterColumn<Guid>(
                name: "TikiProductId",
                table: "TikiReviews",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "TikiProductId",
                table: "TikiProductImages",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_TikiProductImages_TikiProducts_TikiProductId",
                table: "TikiProductImages",
                column: "TikiProductId",
                principalTable: "TikiProducts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TikiReviews_TikiProducts_TikiProductId",
                table: "TikiReviews",
                column: "TikiProductId",
                principalTable: "TikiProducts",
                principalColumn: "Id");
        }
    }
}
