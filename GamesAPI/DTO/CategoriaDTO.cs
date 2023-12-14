using GamesAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace GamesAPI.DTO
{
    public class CategoriaDTO
    {
        /// <summary>
        ///  Código interno da categoria
        /// </summary>
        public int CategoriaId { get; set; }
        /// <summary>
        ///  Nome da categoria
        /// </summary>
        public string? Nome { get; set; }
        /// <summary>
        ///  Url da imagem da categoria
        /// </summary>
        public string? ImagemUrl { get; set; }
        /// <summary>
        ///  Coleção de jogos que pertence a categoria
        /// </summary>
        public ICollection<Jogo>? Jogos { get; set; }
    }
}
