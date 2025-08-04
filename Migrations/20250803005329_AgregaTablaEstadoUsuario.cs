using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InfalibleRealEstate.Migrations
{
    /// <inheritdoc />
    public partial class AgregaTablaEstadoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoUsuarioId",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EstadosUsuarios",
                columns: table => new
                {
                    EstadoUsuarioId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosUsuarios", x => x.EstadoUsuarioId);
                });

            migrationBuilder.InsertData(
                table: "EstadosUsuarios",
                columns: new[] { "EstadoUsuarioId", "Descripcion", "Nombre" },
                values: new object[,]
                {
                    { 1, "El usuario está activo y puede acceder al sistema.", "Activo" },
                    { 2, "El usuario no puede acceder al sistema.", "Inactivo" },
                    { 3, "El usuario ha sido suspendido temporalmente.", "Suspendido" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EstadoUsuarioId",
                table: "AspNetUsers",
                column: "EstadoUsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_EstadosUsuarios_EstadoUsuarioId",
                table: "AspNetUsers",
                column: "EstadoUsuarioId",
                principalTable: "EstadosUsuarios",
                principalColumn: "EstadoUsuarioId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_EstadosUsuarios_EstadoUsuarioId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "EstadosUsuarios");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EstadoUsuarioId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EstadoUsuarioId",
                table: "AspNetUsers");
        }
    }
}
