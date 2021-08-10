using Microsoft.EntityFrameworkCore.Migrations;

namespace BibliotecaAPI.Migrations
{
    public partial class SecondCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "puntuacion",
                table: "Libros",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "FotografiaPath",
                table: "Autores",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "puntuacion",
                table: "Libros");

            migrationBuilder.DropColumn(
                name: "FotografiaPath",
                table: "Autores");
        }
    }
}
