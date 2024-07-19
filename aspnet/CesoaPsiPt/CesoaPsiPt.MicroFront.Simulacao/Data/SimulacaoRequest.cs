using System.ComponentModel.DataAnnotations;

namespace CesoaPsiPt.MicroFront.Simulacao.Data
{
    public class SimulacaoRequest
    {
        [Range(60000, double.MaxValue, ErrorMessage = "O valor do empréstimo deve ser maior que 60.000.")]
        public double ValorEmprestimo { get; set; }
        [Range(12, 420, ErrorMessage = "O prazo deve ser entre 12 e 420 meses.")]
        public int PrazoTotal { get; set; }

        [Required(ErrorMessage = "O valor da taxa de juros é obrigatório.")]
        public double TaxaJuros { get; set; }
        [Required(ErrorMessage = "A seleção da tabela de juros é obrigatória.")]
        public string TabelaJuros { get; set; }
    }
}
