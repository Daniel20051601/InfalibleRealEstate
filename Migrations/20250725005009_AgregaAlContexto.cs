using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfalibleRealEstate.Migrations
{
    /// <inheritdoc />
    public partial class AgregaAlContexto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagenPropiedad_Propiedad_PropiedadId",
                table: "ImagenPropiedad");

            migrationBuilder.DropForeignKey(
                name: "FK_Propiedad_AspNetUsers_AdministradorId",
                table: "Propiedad");

            migrationBuilder.DropForeignKey(
                name: "FK_Propiedad_Categoria_CategoriaId",
                table: "Propiedad");

            migrationBuilder.DropForeignKey(
                name: "FK_Propiedad_EstadoPropiedad_EstadoPropiedadId",
                table: "Propiedad");

            migrationBuilder.DropForeignKey(
                name: "FK_PropiedadDetalle_Propiedad_PropiedadId",
                table: "PropiedadDetalle");

            migrationBuilder.DropForeignKey(
                name: "FK_SuscripcionNoticia_AspNetUsers_UsuarioId",
                table: "SuscripcionNoticia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SuscripcionNoticia",
                table: "SuscripcionNoticia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropiedadDetalle",
                table: "PropiedadDetalle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Propiedad",
                table: "Propiedad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImagenPropiedad",
                table: "ImagenPropiedad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstadoPropiedad",
                table: "EstadoPropiedad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categoria",
                table: "Categoria");

            migrationBuilder.RenameTable(
                name: "SuscripcionNoticia",
                newName: "SuscripcionesNoticia");

            migrationBuilder.RenameTable(
                name: "PropiedadDetalle",
                newName: "PropiedadDetalles");

            migrationBuilder.RenameTable(
                name: "Propiedad",
                newName: "Propiedades");

            migrationBuilder.RenameTable(
                name: "ImagenPropiedad",
                newName: "ImagenesPropiedad");

            migrationBuilder.RenameTable(
                name: "EstadoPropiedad",
                newName: "EstadosPropiedad");

            migrationBuilder.RenameTable(
                name: "Categoria",
                newName: "Categorias");

            migrationBuilder.RenameIndex(
                name: "IX_SuscripcionNoticia_UsuarioId",
                table: "SuscripcionesNoticia",
                newName: "IX_SuscripcionesNoticia_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Propiedad_EstadoPropiedadId",
                table: "Propiedades",
                newName: "IX_Propiedades_EstadoPropiedadId");

            migrationBuilder.RenameIndex(
                name: "IX_Propiedad_CategoriaId",
                table: "Propiedades",
                newName: "IX_Propiedades_CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Propiedad_AdministradorId",
                table: "Propiedades",
                newName: "IX_Propiedades_AdministradorId");

            migrationBuilder.RenameIndex(
                name: "IX_ImagenPropiedad_PropiedadId",
                table: "ImagenesPropiedad",
                newName: "IX_ImagenesPropiedad_PropiedadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SuscripcionesNoticia",
                table: "SuscripcionesNoticia",
                column: "SuscripcionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropiedadDetalles",
                table: "PropiedadDetalles",
                column: "PropiedadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Propiedades",
                table: "Propiedades",
                column: "PropiedadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImagenesPropiedad",
                table: "ImagenesPropiedad",
                column: "ImagenId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstadosPropiedad",
                table: "EstadosPropiedad",
                column: "EstadoPropiedadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagenesPropiedad_Propiedades_PropiedadId",
                table: "ImagenesPropiedad",
                column: "PropiedadId",
                principalTable: "Propiedades",
                principalColumn: "PropiedadId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropiedadDetalles_Propiedades_PropiedadId",
                table: "PropiedadDetalles",
                column: "PropiedadId",
                principalTable: "Propiedades",
                principalColumn: "PropiedadId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Propiedades_AspNetUsers_AdministradorId",
                table: "Propiedades",
                column: "AdministradorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Propiedades_Categorias_CategoriaId",
                table: "Propiedades",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Propiedades_EstadosPropiedad_EstadoPropiedadId",
                table: "Propiedades",
                column: "EstadoPropiedadId",
                principalTable: "EstadosPropiedad",
                principalColumn: "EstadoPropiedadId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SuscripcionesNoticia_AspNetUsers_UsuarioId",
                table: "SuscripcionesNoticia",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagenesPropiedad_Propiedades_PropiedadId",
                table: "ImagenesPropiedad");

            migrationBuilder.DropForeignKey(
                name: "FK_PropiedadDetalles_Propiedades_PropiedadId",
                table: "PropiedadDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_Propiedades_AspNetUsers_AdministradorId",
                table: "Propiedades");

            migrationBuilder.DropForeignKey(
                name: "FK_Propiedades_Categorias_CategoriaId",
                table: "Propiedades");

            migrationBuilder.DropForeignKey(
                name: "FK_Propiedades_EstadosPropiedad_EstadoPropiedadId",
                table: "Propiedades");

            migrationBuilder.DropForeignKey(
                name: "FK_SuscripcionesNoticia_AspNetUsers_UsuarioId",
                table: "SuscripcionesNoticia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SuscripcionesNoticia",
                table: "SuscripcionesNoticia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Propiedades",
                table: "Propiedades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropiedadDetalles",
                table: "PropiedadDetalles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImagenesPropiedad",
                table: "ImagenesPropiedad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EstadosPropiedad",
                table: "EstadosPropiedad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorias",
                table: "Categorias");

            migrationBuilder.RenameTable(
                name: "SuscripcionesNoticia",
                newName: "SuscripcionNoticia");

            migrationBuilder.RenameTable(
                name: "Propiedades",
                newName: "Propiedad");

            migrationBuilder.RenameTable(
                name: "PropiedadDetalles",
                newName: "PropiedadDetalle");

            migrationBuilder.RenameTable(
                name: "ImagenesPropiedad",
                newName: "ImagenPropiedad");

            migrationBuilder.RenameTable(
                name: "EstadosPropiedad",
                newName: "EstadoPropiedad");

            migrationBuilder.RenameTable(
                name: "Categorias",
                newName: "Categoria");

            migrationBuilder.RenameIndex(
                name: "IX_SuscripcionesNoticia_UsuarioId",
                table: "SuscripcionNoticia",
                newName: "IX_SuscripcionNoticia_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Propiedades_EstadoPropiedadId",
                table: "Propiedad",
                newName: "IX_Propiedad_EstadoPropiedadId");

            migrationBuilder.RenameIndex(
                name: "IX_Propiedades_CategoriaId",
                table: "Propiedad",
                newName: "IX_Propiedad_CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_Propiedades_AdministradorId",
                table: "Propiedad",
                newName: "IX_Propiedad_AdministradorId");

            migrationBuilder.RenameIndex(
                name: "IX_ImagenesPropiedad_PropiedadId",
                table: "ImagenPropiedad",
                newName: "IX_ImagenPropiedad_PropiedadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SuscripcionNoticia",
                table: "SuscripcionNoticia",
                column: "SuscripcionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Propiedad",
                table: "Propiedad",
                column: "PropiedadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropiedadDetalle",
                table: "PropiedadDetalle",
                column: "PropiedadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImagenPropiedad",
                table: "ImagenPropiedad",
                column: "ImagenId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EstadoPropiedad",
                table: "EstadoPropiedad",
                column: "EstadoPropiedadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categoria",
                table: "Categoria",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagenPropiedad_Propiedad_PropiedadId",
                table: "ImagenPropiedad",
                column: "PropiedadId",
                principalTable: "Propiedad",
                principalColumn: "PropiedadId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Propiedad_AspNetUsers_AdministradorId",
                table: "Propiedad",
                column: "AdministradorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Propiedad_Categoria_CategoriaId",
                table: "Propiedad",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Propiedad_EstadoPropiedad_EstadoPropiedadId",
                table: "Propiedad",
                column: "EstadoPropiedadId",
                principalTable: "EstadoPropiedad",
                principalColumn: "EstadoPropiedadId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropiedadDetalle_Propiedad_PropiedadId",
                table: "PropiedadDetalle",
                column: "PropiedadId",
                principalTable: "Propiedad",
                principalColumn: "PropiedadId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SuscripcionNoticia_AspNetUsers_UsuarioId",
                table: "SuscripcionNoticia",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
