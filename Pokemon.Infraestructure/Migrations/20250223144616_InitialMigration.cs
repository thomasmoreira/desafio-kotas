using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pokemon.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PokemonMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Idade = table.Column<int>(type: "INTEGER", nullable: false),
                    Cpf = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PokemonCaptures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PokemonId = table.Column<int>(type: "INTEGER", nullable: false),
                    MasterId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataCaptura = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonCaptures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonCaptures_PokemonMasters_MasterId",
                        column: x => x.MasterId,
                        principalTable: "PokemonMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonCaptures_MasterId",
                table: "PokemonCaptures",
                column: "MasterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonCaptures");

            migrationBuilder.DropTable(
                name: "PokemonMasters");
        }
    }
}
