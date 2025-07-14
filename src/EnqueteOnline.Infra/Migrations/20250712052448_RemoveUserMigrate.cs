using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnqueteOnline.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enquetes_Usuarios_UsuarioId",
                table: "Enquetes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votos_Usuarios_UsuarioId",
                table: "Votos");

            migrationBuilder.DropIndex(
                name: "IX_Enquetes_UsuarioId",
                table: "Enquetes");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Enquetes");

            migrationBuilder.AddForeignKey(
                name: "FK_Votos_Usuarios_UsuarioId",
                table: "Votos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votos_Usuarios_UsuarioId",
                table: "Votos");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "Enquetes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enquetes_UsuarioId",
                table: "Enquetes",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enquetes_Usuarios_UsuarioId",
                table: "Enquetes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Votos_Usuarios_UsuarioId",
                table: "Votos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
