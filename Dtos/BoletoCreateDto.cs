using System.ComponentModel.DataAnnotations;

namespace BoletosApi.Dtos
{
    public class BoletoCreateDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string NomePagador { get; set; }

        [Required]
        public string CpfCnpjPagador { get; set; }

        [Required]
        public string NomeBeneficiario { get; set; }

        [Required]
        public string CpfCnpjBeneficiario { get; set; }

        [Required]
        public decimal Valor { get; set; }

        [Required]
        public DateTime DataVencimento { get; set; }

        public string? Observacao { get; set; }

        [Required]
        public Guid BancoId { get; set; }
    }
}
