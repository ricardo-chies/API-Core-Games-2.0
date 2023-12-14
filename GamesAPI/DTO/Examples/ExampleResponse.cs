using Swashbuckle.AspNetCore.Filters;

namespace GamesAPI.DTO.Examples
{
    public class ExampleResponse
    {
    }
    public class ExampleResponseJogo200 : IExamplesProvider<List<JogoDTO>>
    {
        public List<JogoDTO> GetExamples() 
        {
            return new List<JogoDTO> 
            { 
                new JogoDTO
                {
                    JogoId = 5,
                    Nome = "League of Legends",
                    Descricao = "O popular MOBA com uma grande base de jogadores.",
                    Nota = (decimal?)9.5,
                    Preco = (decimal)0.0,
                    ImagemUrl = "https://exemplo.com/leagueoflegends.jpg",
                    AnoLancamento = "2009",
                    CategoriaId = 5
                } 
            };
        }
    }

    public class ExampleResponseCategoria200 : IExamplesProvider<List<CategoriaDTO>>
    {
        public List<CategoriaDTO> GetExamples()
        {
            return new List<CategoriaDTO>
            {
                new CategoriaDTO
                {
                    CategoriaId = 5,
                    Nome = "MOBA",
                    ImagemUrl = "MOBA.jpg",
                    Jogos = new List<Models.Jogo>()
                }
            };
        }
    }

    //public class ExampleResponseCategoriaJogos200 : IExamplesProvider<List<CategoriaDTO>>
    //{
    //    public List<CategoriaDTO> GetExamples()
    //    {
    //        return new List<CategoriaDTO>
    //        {   
    //            new CategoriaDTO
    //            {
    //                CategoriaId = 5,
    //                Nome = "MOBA",
    //                ImagemUrl = "MOBA.jpg",
    //                Jogos = new List<Models.Jogo>
    //                {
    //                    new Models.Jogo
    //                    {
    //                        JogoId = 5,
    //                        Nome = "League of Legends",
    //                        Descricao = "O popular MOBA com uma grande base de jogadores.",
    //                        Nota = (decimal?)9.5,
    //                        Preco = (decimal)0.0,
    //                        ImagemUrl = "https://exemplo.com/leagueoflegends.jpg",
    //                        AnoLancamento = "2009",
    //                        CategoriaId = 5
    //                    }
    //                }
    //            }
    //        };
    //    }
    //}
}
