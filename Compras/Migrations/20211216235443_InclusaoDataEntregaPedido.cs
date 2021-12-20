using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Compras.Migrations
{
    public partial class InclusaoDataEntregaPedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PedidoEntregue",
                table: "Pedidos",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PedidoEntregue",
                table: "Pedidos");
        }
    }
}
