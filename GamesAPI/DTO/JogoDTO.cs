using GamesAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GamesAPI.DTO
{
    public class JogoDTO
    {
        /// <summary>
        ///  Código interno do jogo.
        /// </summary>
        public int JogoId { get; set; }
        /// <summary>
        ///  Nome do jogo.
        /// </summary>
        public string? Nome { get; set; }
        /// <summary>
        ///  Descrição do jogo.
        /// </summary>
        public string? Descricao { get; set; }
        /// <summary>
        ///  Nota média do jogo dada pelos usuários.
        /// </summary>
        public decimal? Nota { get; set; }
        /// <summary>
        ///  Preço para compra jogo
        /// </summary>
        public decimal Preco { get; set; }
        /// <summary>
        ///  Url da imagem do jogo.
        /// </summary>
        public string? ImagemUrl { get; set; }
        /// <summary>
        ///  Ano que o jogo foi lançado.
        /// </summary>
        public string? AnoLancamento { get; set; }
        /// <summary>
        ///  Código interno da categoria em que o jogo pertence
        /// </summary>
        public int CategoriaId { get; set; }
    }
}
