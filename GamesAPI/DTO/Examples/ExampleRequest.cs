using Swashbuckle.AspNetCore.Filters;

namespace GamesAPI.DTO.Examples
{
    public class ExampleRequest
    {
    }

    public class ExampleRequestCategoria : IExamplesProvider<CategoriaDTO>
    {
        public CategoriaDTO GetExamples()
        {
            return new CategoriaDTO
            {
                Nome = "Ação e Aventura",
                ImagemUrl = "acao.jpg"
            };
        }
    }

    public class ExampleRequestJogo : IExamplesProvider<JogoDTO>
    {
        public JogoDTO GetExamples()
        {
            return new JogoDTO
            {
                JogoId = 1,
                Nome = "The Last of Us - Part 2",
                Descricao = "Jogo de ação e aventura pós-apocalíptico",
                Nota = (decimal?)9.7,
                Preco = (decimal)59.99,
                ImagemUrl = "https://exemplo.com/lastofus2.jpg",
                AnoLancamento = "2020",
                CategoriaId = 1
            };
        }
    }
}
