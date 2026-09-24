using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarMapeoTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "estado",
                table: "incidencia",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "descripcion",
                table: "Categorias",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "id_categoria", "descripcion", "nombre" },
                values: new object[,]
                {
                    { 1, "Luminarias apagadas o dañadas", "Alumbrado público" },
                    { 2, "Pozos y roturas de pavimento", "Bacheo y calles" },
                    { 3, "Basurales o falta de recolección", "Residuos" },
                    { 4, "Poda o caída de ramas", "Arbolado urbano" },
                    { 5, "Semáforos fuera de servicio", "Semáforos y señalización" },
                    { 6, "Otras incidencias urbanas", "Otros" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_email",
                table: "usuarios",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_usuarios_email",
                table: "usuarios");

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "id_categoria",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "id_categoria",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "id_categoria",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "id_categoria",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "id_categoria",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "id_categoria",
                keyValue: 6);

            migrationBuilder.AlterColumn<int>(
                name: "estado",
                table: "incidencia",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "descripcion",
                table: "Categorias",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
