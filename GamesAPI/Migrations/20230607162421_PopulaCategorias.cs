using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesAPI.Migrations
{
    public partial class PopulaCategorias : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Categorias(Nome, ImagemUrl) Values('Ação e Aventura', 'Acao.jpg')");
            mb.Sql("Insert into Categorias(Nome, ImagemUrl) Values('Esporte', 'Esporte.jpg')");
            mb.Sql("Insert into Categorias(Nome, ImagemUrl) Values('RPG', 'RPG.jpg')");
            mb.Sql("Insert into Categorias(Nome, ImagemUrl) Values('FPS', 'FPS.jpg')");
            mb.Sql("Insert into Categorias(Nome, ImagemUrl) Values('MOBA', 'MOBA.jpg')");
            mb.Sql("Insert into Categorias(Nome, ImagemUrl) Values('Luta', 'Luta.jpg')");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categorias");
        }
    }
}
