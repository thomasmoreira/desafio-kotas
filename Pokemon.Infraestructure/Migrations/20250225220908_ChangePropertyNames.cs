using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pokemon.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangePropertyNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "PokemonMasters",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Idade",
                table: "PokemonMasters",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "DataCaptura",
                table: "PokemonCaptures",
                newName: "CaptureDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PokemonMasters",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "PokemonMasters",
                newName: "Idade");

            migrationBuilder.RenameColumn(
                name: "CaptureDate",
                table: "PokemonCaptures",
                newName: "DataCaptura");
        }
    }
}
