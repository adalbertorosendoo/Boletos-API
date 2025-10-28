using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoletosApi.Models
{
    public class Boleto
    {
        [Key]
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
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }

        [Required]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime DataVencimento { get; set; }

        public string? Observacao { get; set; }

        [Required]
        public Guid BancoId { get; set; }

        public Banco? Banco { get; set; }
    }
}
