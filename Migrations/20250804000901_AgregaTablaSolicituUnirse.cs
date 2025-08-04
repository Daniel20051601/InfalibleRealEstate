using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InfalibleRealEstate.Migrations
{
    /// <inheritdoc />
    public partial class AgregaTablaSolicituUnirse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SolicitudesUnirse",
                columns: table => new
                {
                    SolicitudUnirseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CorreoElectronico = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    NivelEstudios = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Experiencia = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Profesion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TrabajaActualmente = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Provincia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PorqueNosotros = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesUnirse", x => x.SolicitudUnirseId);
                    table.ForeignKey(
                        name: "FK_SolicitudesUnirse_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesUnirse_UsuarioId",
                table: "SolicitudesUnirse",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitudesUnirse");
        }
    }
}
