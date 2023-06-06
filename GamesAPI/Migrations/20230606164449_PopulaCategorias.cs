using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesAPI.Migrations
{
    public partial class PopulaCategorias : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Categorias(NOME, IMAGEM_URL) Values('Ação e Aventura', 'Acao.jpg')");
            mb.Sql("Insert into Categorias(NOME, IMAGEM_URL) Values('Esporte', 'Esporte.jpg')");
            mb.Sql("Insert into Categorias(NOME, IMAGEM_URL) Values('RPG', 'RPG.jpg')");
            mb.Sql("Insert into Categorias(NOME, IMAGEM_URL) Values('FPS', 'FPS.jpg')");
            mb.Sql("Insert into Categorias(NOME, IMAGEM_URL) Values('MOBA', 'MOBA.jpg')");
            mb.Sql("Insert into Categorias(NOME, IMAGEM_URL) Values('Luta', 'Luta.jpg')");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categorias");
        }
    }
}
