using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InfalibleRealEstate.Migrations
{
    /// <inheritdoc />
    public partial class AgregaLasTablas2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "AspNetUsers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "AspNetUsers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreCategoria = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "EstadoPropiedad",
                columns: table => new
                {
                    EstadoPropiedadId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreEstado = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoPropiedad", x => x.EstadoPropiedadId);
                });

            migrationBuilder.CreateTable(
                name: "SuscripcionNoticia",
                columns: table => new
                {
                    SuscripcionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FechaSuscripcion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    activa = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuscripcionNoticia", x => x.SuscripcionId);
                    table.ForeignKey(
                        name: "FK_SuscripcionNoticia_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Propiedad",
                columns: table => new
                {
                    PropiedadId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Precio = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Moneda = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Ciudad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EstadoProvincia = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TipoTransaccion = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    CategoriaId = table.Column<int>(type: "integer", nullable: false),
                    AdministradorId = table.Column<string>(type: "text", nullable: true),
                    FechaPublicacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EstadoPropiedadId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propiedad", x => x.PropiedadId);
                    table.ForeignKey(
                        name: "FK_Propiedad_AspNetUsers_AdministradorId",
                        column: x => x.AdministradorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Propiedad_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Propiedad_EstadoPropiedad_EstadoPropiedadId",
                        column: x => x.EstadoPropiedadId,
                        principalTable: "EstadoPropiedad",
                        principalColumn: "EstadoPropiedadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImagenPropiedad",
                columns: table => new
                {
                    ImagenId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PropiedadId = table.Column<int>(type: "integer", nullable: false),
                    UrlImagen = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Orden = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagenPropiedad", x => x.ImagenId);
                    table.ForeignKey(
                        name: "FK_ImagenPropiedad_Propiedad_PropiedadId",
                        column: x => x.PropiedadId,
                        principalTable: "Propiedad",
                        principalColumn: "PropiedadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropiedadDetalle",
                columns: table => new
                {
                    PropiedadId = table.Column<int>(type: "integer", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Habitaciones = table.Column<int>(type: "integer", nullable: true),
                    Banos = table.Column<decimal>(type: "numeric", nullable: true),
                    Parqueo = table.Column<int>(type: "integer", nullable: true),
                    MetrosCuadrados = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropiedadDetalle", x => x.PropiedadId);
                    table.ForeignKey(
                        name: "FK_PropiedadDetalle_Propiedad_PropiedadId",
                        column: x => x.PropiedadId,
                        principalTable: "Propiedad",
                        principalColumn: "PropiedadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImagenPropiedad_PropiedadId",
                table: "ImagenPropiedad",
                column: "PropiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_Propiedad_AdministradorId",
                table: "Propiedad",
                column: "AdministradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Propiedad_CategoriaId",
                table: "Propiedad",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Propiedad_EstadoPropiedadId",
                table: "Propiedad",
                column: "EstadoPropiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_SuscripcionNoticia_UsuarioId",
                table: "SuscripcionNoticia",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagenPropiedad");

            migrationBuilder.DropTable(
                name: "PropiedadDetalle");

            migrationBuilder.DropTable(
                name: "SuscripcionNoticia");

            migrationBuilder.DropTable(
                name: "Propiedad");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "EstadoPropiedad");

            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "AspNetUsers");
        }
    }
}
