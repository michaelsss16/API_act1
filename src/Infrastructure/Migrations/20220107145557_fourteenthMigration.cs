using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class fourteenthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CupomId",
                table: "Vendas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "PorcentagemDeDesconto",
                table: "Vendas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ValorComDesconto",
                table: "Vendas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CupomId",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "PorcentagemDeDesconto",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "ValorComDesconto",
                table: "Vendas");
        }
    }
}
