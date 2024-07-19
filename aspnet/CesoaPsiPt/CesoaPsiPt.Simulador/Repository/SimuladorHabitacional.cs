using CesoaPsiPt.Simulador.Model;
using Microsoft.AspNetCore.Mvc;

namespace CesoaPsiPt.Simulador.Repository
{
	public class SimuladorHabitacional : ISimuladorHabitacional
	{
		public async Task<ActionResult<ResponseSimulacaoHabitacional>> simularhabitacional(RequestSimulacaoHabitacional reqSimulacao)
		{

            try
            {
                return reqSimulacao.TabelaJuros.ToUpper() switch
                {
                    "SAC" => await CalcularSAC(reqSimulacao),
                    "PRICE" => await CalcularPrice(reqSimulacao),
                    "SAM" => await CalcularSAM(reqSimulacao),
                    _ => throw new ArgumentException("Tipo de amortização inválido")
                };

            }
            catch (Exception ex)
            {
                // Log the error or handle it accordingly
                return new ActionResult<ResponseSimulacaoHabitacional>(
                    new ResponseSimulacaoHabitacional { ErrorMessage = $"Ocorreu um erro: {ex.Message}" }
                );
            }
        }


        private async Task<ActionResult<ResponseSimulacaoHabitacional>> CalcularSAC(RequestSimulacaoHabitacional req)
        {

            try 
            { 
            double P = req.ValorEmprestimo;
            double i = req.TaxaJuros / 1200.0; // Taxa mensal
            int n = req.PrazoTotal;

            double A = P / n;  // Amortização constante
            double saldoDevedor = P;
            var response = new ResponseSimulacaoHabitacional();
            response.Prestacoes = new List<Parcela>();
            for (int t = 1; t <= n; t++)
            {
                double J = saldoDevedor * i; // Juros decrescentes
                double P_t = A + J;
                saldoDevedor -= A;

                response.Prestacoes.Add(new Parcela
                {
                    NumeroPrestacao = t,
                    ValorPrestacao = P_t,
                    ValorJuros = J,
                    ValorAmortizacao = A,
                    SaldoDevedor = saldoDevedor
                });
            }

                return await Task.FromResult(new ActionResult<ResponseSimulacaoHabitacional>(response));

            }
            catch (Exception ex)
            {

                return await Task.FromResult(new ActionResult<ResponseSimulacaoHabitacional>(new ResponseSimulacaoHabitacional { ErrorMessage = $"Ocorreu um erro: {ex.Message}" }));
            }
        }

        private async Task<ActionResult<ResponseSimulacaoHabitacional>> CalcularPrice(RequestSimulacaoHabitacional req)
        {

            try
            {
                double P = req.ValorEmprestimo;
                double i = req.TaxaJuros / 1200.0; // Taxa mensal
                int n = req.PrazoTotal;

                double P_t = P * (i * Math.Pow(1 + i, n)) / (Math.Pow(1 + i, n) - 1);
                double saldoDevedor = P;
                var response = new ResponseSimulacaoHabitacional();

                response.Prestacoes = new List<Parcela>();
                for (int t = 1; t <= n; t++)
                {
                    double J = saldoDevedor * i;
                    double A = P_t - J;  // Amortização é o total menos os juros
                    saldoDevedor -= A;

                    response.Prestacoes.Add(new Parcela
                    {
                        NumeroPrestacao = t,
                        ValorPrestacao = P_t,
                        ValorJuros = J,
                        ValorAmortizacao = A,
                        SaldoDevedor = saldoDevedor
                    });
                }

                return await Task.FromResult(new ActionResult<ResponseSimulacaoHabitacional>(response));

            }
            catch (Exception ex)
            {

                return await Task.FromResult(new ActionResult<ResponseSimulacaoHabitacional>(new ResponseSimulacaoHabitacional { ErrorMessage = $"Ocorreu um erro: {ex.Message}" }));
            }
        }
        private async Task<ActionResult<ResponseSimulacaoHabitacional>> CalcularSAM(RequestSimulacaoHabitacional req)
        {
            try
            {
                double P = (double)req.ValorEmprestimo;
                double i = (double)req.TaxaJuros / 1200.0; // Taxa mensal
                int n = req.PrazoTotal;

                double amortizacaoSAC = P / n;
                double saldoDevedor = P;
                double amortizacaoInicial = amortizacaoSAC * 0.75; // Começa com 75% da amortização do SAC
                double incrementoAmortizacao = (amortizacaoSAC - amortizacaoInicial) / n; // Ajusta para alcançar o SAC no final

                var response = new ResponseSimulacaoHabitacional();
                response.Prestacoes = new List<Parcela>();
                for (int t = 1; t <= n; t++)
                {
                    double amortizacao = amortizacaoInicial + incrementoAmortizacao * t;
                    double juros = saldoDevedor * i;
                    double prestacao = amortizacao + juros;

                    saldoDevedor -= amortizacao;

                    response.Prestacoes.Add(new Parcela
                    {
                        NumeroPrestacao = t,
                        ValorPrestacao = prestacao,
                        ValorJuros = juros,
                        ValorAmortizacao = amortizacao,
                        SaldoDevedor = saldoDevedor
                    });
                }

                return await Task.FromResult(new ActionResult<ResponseSimulacaoHabitacional>(response));

            }
            catch (Exception ex)
            {
                
                return await Task.FromResult(new ActionResult<ResponseSimulacaoHabitacional>(new ResponseSimulacaoHabitacional { ErrorMessage = $"Ocorreu um erro: {ex.Message}" }));
            }
        }





    }
}
