using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesAPI.Migrations
{
    public partial class MigracaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CATEGORIA_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOME = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IMAGEM_URL = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CATEGORIA_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Jogos",
                columns: table => new
                {
                    JOGO_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOME = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DESCRICAO = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NOTA = table.Column<decimal>(type: "decimal(14,4)", nullable: false),
                    PRECO = table.Column<decimal>(type: "decimal(14,4)", nullable: false),
                    IMAGEM_URL = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ANO_LANCAMENTO = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DATA_CADASTRO = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CATEGORIA_ID = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogos", x => x.JOGO_ID);
                    table.ForeignKey(
                        name: "FK_Jogos_Categorias_CATEGORIA_ID",
                        column: x => x.CATEGORIA_ID,
                        principalTable: "Categorias",
                        principalColumn: "CATEGORIA_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_CATEGORIA_ID",
                table: "Jogos",
                column: "CATEGORIA_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jogos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
