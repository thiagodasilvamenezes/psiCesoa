namespace CesoaPsiPt.Simulador.Model
{
	public class ResponseSimulacaoHabitacional
	{
      
        public List<Parcela> Prestacoes { get; set; } = new List<Parcela>();
        public string ErrorMessage { get; set; } = string.Empty;
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
