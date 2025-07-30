using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InfalibleRealEstate.Migrations
{
    /// <inheritdoc />
    public partial class AgregaRegistros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Propiedades_Categorias_CategoriaId",
                table: "Propiedades");

            migrationBuilder.AlterColumn<int>(
                name: "Parqueo",
                table: "PropiedadDetalles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MetrosCuadrados",
                table: "PropiedadDetalles",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Habitaciones",
                table: "PropiedadDetalles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Banos",
                table: "PropiedadDetalles",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "CategoriaId", "Descripcion", "NombreCategoria" },
                values: new object[,]
                {
                    { 1, "Unidad de vivienda en un edificio de apartamentos.", "Apartamento" },
                    { 2, "Vivienda unifamiliar o adosada.", "Casa" },
                    { 3, "Lote de tierra disponible para construcción.", "Terreno" },
                    { 4, "Espacio para negocios y oficinas.", "Local Comercial" },
                    { 5, "Casa de lujo, a menudo con jardín o terreno grande.", "Villa" },
                    { 6, "Apartamento de dos niveles, regularmente con Jacuzzi", "Penthouse" }
                });

            migrationBuilder.InsertData(
                table: "EstadosPropiedad",
                columns: new[] { "EstadoPropiedadId", "Descripcion", "NombreEstado" },
                values: new object[,]
                {
                    { 1, "La propiedad está visible y disponible.", "Activa" },
                    { 3, "La propiedad ha sido vendida y ya no está disponible.", "Vendida" },
                    { 4, "La propiedad ha sido alquilada y ya no está disponible.", "Alquilada" },
                    { 6, "La propiedad ha sido eliminada del sistema.", "Eliminada" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Propiedades_Categorias_CategoriaId",
                table: "Propiedades",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Propiedades_Categorias_CategoriaId",
                table: "Propiedades");

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EstadosPropiedad",
                keyColumn: "EstadoPropiedadId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EstadosPropiedad",
                keyColumn: "EstadoPropiedadId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EstadosPropiedad",
                keyColumn: "EstadoPropiedadId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EstadosPropiedad",
                keyColumn: "EstadoPropiedadId",
                keyValue: 6);

            migrationBuilder.AlterColumn<int>(
                name: "Parqueo",
                table: "PropiedadDetalles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "MetrosCuadrados",
                table: "PropiedadDetalles",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<int>(
                name: "Habitaciones",
                table: "PropiedadDetalles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "Banos",
                table: "PropiedadDetalles",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddForeignKey(
                name: "FK_Propiedades_Categorias_CategoriaId",
                table: "Propiedades",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
