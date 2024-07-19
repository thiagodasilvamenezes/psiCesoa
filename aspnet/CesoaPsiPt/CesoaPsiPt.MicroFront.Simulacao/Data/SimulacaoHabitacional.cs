namespace CesoaPsiPt.MicroFront.Simulacao.Data
{

    public class SimulacaoHabitacional
    {
        public double ValorEmprestimo { get; set; }
        public double TaxaJuros { get; set; }
        public int Periodo { get; set; }
        public List<Parcela> Prestacoes { get; set; }
    }

    public class Parcela
    {
        public int NumeroPrestacao { get; set; }
        public double ValorPrestacao { get; set; }
        public double ValorJuros { get; set; }
        public double ValorAmortizacao { get; set; }
        public double SaldoDevedor { get; set; }
    }

}
