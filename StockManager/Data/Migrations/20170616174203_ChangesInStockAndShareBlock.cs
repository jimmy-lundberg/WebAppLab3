using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StockManager.Data.Migrations
{
    public partial class ChangesInStockAndShareBlock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShareBlocks_Stocks_StockId",
                table: "ShareBlocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShareBlocks",
                table: "ShareBlocks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ShareBlocks");

            migrationBuilder.DropColumn(
                name: "BuyPrice",
                table: "ShareBlocks");

            migrationBuilder.DropColumn(
                name: "SellPrice",
                table: "ShareBlocks");

            migrationBuilder.RenameColumn(
                name: "StockId",
                table: "ShareBlocks",
                newName: "ParentStockId");

            migrationBuilder.RenameIndex(
                name: "IX_ShareBlocks_StockId",
                table: "ShareBlocks",
                newName: "IX_ShareBlocks_ParentStockId");

            migrationBuilder.AddColumn<int>(
                name: "OwnerPortfolioId",
                table: "ShareBlocks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShareBlocks",
                table: "ShareBlocks",
                columns: new[] { "OwnerPortfolioId", "ParentStockId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ShareBlocks_StockPortfolios_OwnerPortfolioId",
                table: "ShareBlocks",
                column: "OwnerPortfolioId",
                principalTable: "StockPortfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShareBlocks_Stocks_ParentStockId",
                table: "ShareBlocks",
                column: "ParentStockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShareBlocks_StockPortfolios_OwnerPortfolioId",
                table: "ShareBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_ShareBlocks_Stocks_ParentStockId",
                table: "ShareBlocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShareBlocks",
                table: "ShareBlocks");

            migrationBuilder.DropColumn(
                name: "OwnerPortfolioId",
                table: "ShareBlocks");

            migrationBuilder.RenameColumn(
                name: "ParentStockId",
                table: "ShareBlocks",
                newName: "StockId");

            migrationBuilder.RenameIndex(
                name: "IX_ShareBlocks_ParentStockId",
                table: "ShareBlocks",
                newName: "IX_ShareBlocks_StockId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ShareBlocks",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<decimal>(
                name: "BuyPrice",
                table: "ShareBlocks",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SellPrice",
                table: "ShareBlocks",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShareBlocks",
                table: "ShareBlocks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShareBlocks_Stocks_StockId",
                table: "ShareBlocks",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
