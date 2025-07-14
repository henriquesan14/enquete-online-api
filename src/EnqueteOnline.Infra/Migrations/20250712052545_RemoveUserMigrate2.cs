using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnqueteOnline.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserMigrate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votos_Usuarios_UsuarioId",
                table: "Votos");

            migrationBuilder.DropIndex(
                name: "IX_Votos_UsuarioId",
                table: "Votos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Votos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "Votos",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votos_UsuarioId",
                table: "Votos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votos_Usuarios_UsuarioId",
                table: "Votos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
