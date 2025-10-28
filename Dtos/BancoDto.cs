namespace BoletosApi.Dtos
{
    public class BancoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public decimal PercentualJuros { get; set; }
    }
}
