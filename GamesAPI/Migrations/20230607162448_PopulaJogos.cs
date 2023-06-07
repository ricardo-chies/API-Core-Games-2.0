using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesAPI.Migrations
{
    public partial class PopulaJogos : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Jogos (Nome, Descricao, Nota, Preco, ImagemUrl, AnoLancamento, DataCadastro, CategoriaId) " +
                "VALUES ('The Last of Us Part II', 'Jogo de ação e aventura pós-apocalíptico', 9.7, 59.99, 'https://exemplo.com/lastofus2.jpg', '2020', now(), 1)");
            mb.Sql("INSERT INTO Jogos (Nome, Descricao, Nota, Preco, ImagemUrl, AnoLancamento, DataCadastro, CategoriaId) " +
                "VALUES('FIFA 22', 'Jogo de futebol com os times e jogadores atualizados', 8.5, 59.99, 'https://exemplo.com/fifa22.jpg', '2022', now(), 2)");
            mb.Sql("INSERT INTO Jogos (Nome, Descricao, Nota, Preco, ImagemUrl, AnoLancamento, DataCadastro, CategoriaId) " +
                "VALUES('The Witcher 3: Wild Hunt', 'RPG de mundo aberto com narrativa envolvente', 9.9, 29.99, 'https://exemplo.com/witcher3.jpg', '2015', now(), 3)");
            mb.Sql("INSERT INTO Jogos (Nome, Descricao, Nota, Preco, ImagemUrl, AnoLancamento, DataCadastro, CategoriaId) " +
                "VALUES ('Call of Duty: Warzone', 'Battle royale intenso com gráficos impressionantes', 8.9, 0.00, 'https://exemplo.com/warzone.jpg', '2020', now(), 4)");
            mb.Sql("INSERT INTO Jogos (Nome, Descricao, Nota, Preco, ImagemUrl, AnoLancamento, DataCadastro, CategoriaId) " +
                "VALUES ('League of Legends', 'O popular MOBA com uma grande base de jogadores', 9.5, 0.00, 'https://exemplo.com/leagueoflegends.jpg', '2009', now(), 5)");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Jogos");
        }
    }
}
