using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class sixthmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoVendaDTO_Vendas_VendaId",
                table: "ProdutoVendaDTO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdutoVendaDTO",
                table: "ProdutoVendaDTO");

            migrationBuilder.RenameTable(
                name: "ProdutoVendaDTO",
                newName: "ProdutoVendaDTOs");

            migrationBuilder.RenameColumn(
                name: "ProdutoGuid",
                table: "ProdutoVendaDTOs",
                newName: "ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutoVendaDTO_VendaId",
                table: "ProdutoVendaDTOs",
                newName: "IX_ProdutoVendaDTOs_VendaId");

            migrationBuilder.AlterColumn<Guid>(
                name: "VendaId",
                table: "ProdutoVendaDTOs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdutoVendaDTOs",
                table: "ProdutoVendaDTOs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoVendaDTOs_ProdutoId",
                table: "ProdutoVendaDTOs",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoVendaDTOs_Produtos_ProdutoId",
                table: "ProdutoVendaDTOs",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoVendaDTOs_Vendas_VendaId",
                table: "ProdutoVendaDTOs",
                column: "VendaId",
                principalTable: "Vendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoVendaDTOs_Produtos_ProdutoId",
                table: "ProdutoVendaDTOs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoVendaDTOs_Vendas_VendaId",
                table: "ProdutoVendaDTOs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdutoVendaDTOs",
                table: "ProdutoVendaDTOs");

            migrationBuilder.DropIndex(
                name: "IX_ProdutoVendaDTOs_ProdutoId",
                table: "ProdutoVendaDTOs");

            migrationBuilder.RenameTable(
                name: "ProdutoVendaDTOs",
                newName: "ProdutoVendaDTO");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "ProdutoVendaDTO",
                newName: "ProdutoGuid");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutoVendaDTOs_VendaId",
                table: "ProdutoVendaDTO",
                newName: "IX_ProdutoVendaDTO_VendaId");

            migrationBuilder.AlterColumn<Guid>(
                name: "VendaId",
                table: "ProdutoVendaDTO",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdutoVendaDTO",
                table: "ProdutoVendaDTO",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoVendaDTO_Vendas_VendaId",
                table: "ProdutoVendaDTO",
                column: "VendaId",
                principalTable: "Vendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
