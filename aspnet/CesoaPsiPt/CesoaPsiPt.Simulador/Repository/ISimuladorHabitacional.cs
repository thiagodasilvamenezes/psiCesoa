using CesoaPsiPt.Simulador.Model;
using Microsoft.AspNetCore.Mvc;

namespace CesoaPsiPt.Simulador.Repository
{
	public interface ISimuladorHabitacional
	{
		public Task<ActionResult<ResponseSimulacaoHabitacional>> simularhabitacional(RequestSimulacaoHabitacional reqSimulacao);
		


	}
}
