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

            migrationBuilder.AddColumn<int>(
                name: "PortfolioId",
                table: "ShareBlocks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerPortfolioId",
                table: "ShareBlocks",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShareBlocks",
                table: "ShareBlocks",
                columns: new[] { "PortfolioId", "StockId" });

            migrationBuilder.CreateIndex(
                name: "IX_ShareBlocks_OwnerPortfolioId",
                table: "ShareBlocks",
                column: "OwnerPortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShareBlocks_StockPortfolios_OwnerPortfolioId",
                table: "ShareBlocks",
                column: "OwnerPortfolioId",
                principalTable: "StockPortfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShareBlocks_StockPortfolios_OwnerPortfolioId",
                table: "ShareBlocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShareBlocks",
                table: "ShareBlocks");

            migrationBuilder.DropIndex(
                name: "IX_ShareBlocks_OwnerPortfolioId",
                table: "ShareBlocks");

            migrationBuilder.DropColumn(
                name: "PortfolioId",
                table: "ShareBlocks");

            migrationBuilder.DropColumn(
                name: "OwnerPortfolioId",
                table: "ShareBlocks");

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
        }
    }
}
