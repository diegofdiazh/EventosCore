using Microsoft.EntityFrameworkCore.Migrations;

namespace EventosCore.Migrations
{
    public partial class eventosPrecio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Precio",
                table: "Eventos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Eventos");
        }
    }
}
