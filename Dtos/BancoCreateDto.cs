using System.ComponentModel.DataAnnotations;

namespace BoletosApi.Dtos
{
    public class BancoCreateDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Codigo { get; set; }

        [Required]
        public decimal PercentualJuros { get; set; }
    }
}
