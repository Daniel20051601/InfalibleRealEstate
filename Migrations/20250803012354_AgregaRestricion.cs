using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfalibleRealEstate.Migrations
{
    /// <inheritdoc />
    public partial class AgregaRestricion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_EstadosUsuarios_EstadoUsuarioId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_EstadosUsuarios_EstadoUsuarioId",
                table: "AspNetUsers",
                column: "EstadoUsuarioId",
                principalTable: "EstadosUsuarios",
                principalColumn: "EstadoUsuarioId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_EstadosUsuarios_EstadoUsuarioId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_EstadosUsuarios_EstadoUsuarioId",
                table: "AspNetUsers",
                column: "EstadoUsuarioId",
                principalTable: "EstadosUsuarios",
                principalColumn: "EstadoUsuarioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
