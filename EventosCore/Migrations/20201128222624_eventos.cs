using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventosCore.Migrations
{
    public partial class eventos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AeropuertosId = table.Column<int>(type: "int", nullable: false),
                    DescripcionCorta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEvento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImgLocal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgVisitante = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eventos_Aeropuertos_AeropuertosId",
                        column: x => x.AeropuertosId,
                        principalTable: "Aeropuertos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_AeropuertosId",
                table: "Eventos",
                column: "AeropuertosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Eventos");
        }
    }
}
