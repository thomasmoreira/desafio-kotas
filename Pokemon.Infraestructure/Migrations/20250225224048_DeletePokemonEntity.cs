using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pokemon.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class DeletePokemonEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonCaptures_Pokemons_PokemonId",
                table: "PokemonCaptures");

            migrationBuilder.DropTable(
                name: "Pokemons");

            migrationBuilder.DropIndex(
                name: "IX_PokemonCaptures_PokemonId",
                table: "PokemonCaptures");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Evolucoes = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    SpriteBase64 = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonCaptures_PokemonId",
                table: "PokemonCaptures",
                column: "PokemonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonCaptures_Pokemons_PokemonId",
                table: "PokemonCaptures",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
