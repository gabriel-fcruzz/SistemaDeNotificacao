using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaDeNotificacao.Data.Migrations
{
    /// <inheritdoc />
    public partial class NotificacoesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataEnvio",
                table: "Notificacoes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Enviado",
                table: "Notificacoes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataEnvio",
                table: "Notificacoes");

            migrationBuilder.DropColumn(
                name: "Enviado",
                table: "Notificacoes");
        }
    }
}
