using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamesAPI.Models
{
    [Table("Jogos")]
    public class Jogo
    {
        [Key]
        public int JOGO_ID { get; set; }
        [Required]
        [MaxLength(80)]
        public string? NOME { get; set; }
        [Required]
        [MaxLength(300)]
        public string? DESCRICAO { get; set; }
        [Required]
        [Column(TypeName = "decimal(2,2)")]
        public decimal? NOTA { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal PRECO { get; set; }
        [Required]
        [MaxLength(300)]
        public string? IMAGEM_URL { get; set; }
        [Required]
        [MaxLength(4)]
        public string? ANO_LANCAMENTO { get; set; }
        public DateTime DATA_CADASTRO { get; set; }
        public int CATEGORIA_ID { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
