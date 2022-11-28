using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiLoteria.Migrations
{
    public partial class RPCP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participantes_Rifas_RifaId",
                table: "Participantes");

            migrationBuilder.DropIndex(
                name: "IX_Participantes_RifaId",
                table: "Participantes");

            migrationBuilder.DropColumn(
                name: "RifaId",
                table: "Participantes");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Rifas",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumPrem",
                table: "Rifas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Participantes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "Participantes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Participantes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cartas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ParticipanteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cartas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Premios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenido = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    RifaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RPCP",
                columns: table => new
                {
                    RifaId = table.Column<int>(type: "int", nullable: false),
                    ParticipanteId = table.Column<int>(type: "int", nullable: false),
                    CartasId = table.Column<int>(type: "int", nullable: false),
                    PremioId = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RPCP", x => new { x.RifaId, x.ParticipanteId, x.CartasId, x.PremioId });
                    table.ForeignKey(
                        name: "FK_RPCP_Cartas_CartasId",
                        column: x => x.CartasId,
                        principalTable: "Cartas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RPCP_Participantes_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RPCP_Premios_PremioId",
                        column: x => x.PremioId,
                        principalTable: "Premios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RPCP_Rifas_RifaId",
                        column: x => x.RifaId,
                        principalTable: "Rifas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RPCP_CartasId",
                table: "RPCP",
                column: "CartasId");

            migrationBuilder.CreateIndex(
                name: "IX_RPCP_ParticipanteId",
                table: "RPCP",
                column: "ParticipanteId");

            migrationBuilder.CreateIndex(
                name: "IX_RPCP_PremioId",
                table: "RPCP",
                column: "PremioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RPCP");

            migrationBuilder.DropTable(
                name: "Cartas");

            migrationBuilder.DropTable(
                name: "Premios");

            migrationBuilder.DropColumn(
                name: "NumPrem",
                table: "Rifas");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Participantes");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Rifas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Participantes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "Participantes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<int>(
                name: "RifaId",
                table: "Participantes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Participantes_RifaId",
                table: "Participantes",
                column: "RifaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participantes_Rifas_RifaId",
                table: "Participantes",
                column: "RifaId",
                principalTable: "Rifas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
