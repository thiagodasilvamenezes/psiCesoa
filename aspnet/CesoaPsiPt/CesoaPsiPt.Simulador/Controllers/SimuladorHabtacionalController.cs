using CesoaPsiPt.Simulador.Model;
using CesoaPsiPt.Simulador.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CesoaPsiPt.Simulador.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SimuladorHabtacionalController : ControllerBase
	{
		private readonly ISimuladorHabitacional _simuladorHabitacionalService;

		public SimuladorHabtacionalController(ISimuladorHabitacional simuladorHabitacionalService)
		{
			_simuladorHabitacionalService = simuladorHabitacionalService;
		}


		[HttpPost("simular")]
		//[SwaggerOperation(Summary = "Consultar ")]
		//[SwaggerResponse((int)HttpStatusCode.OK, "Sucesso", typeof(DossieProdutoIntegracaoResponse))]

		public async Task<ActionResult<ResponseSimulacaoHabitacional>> SimularSimples(RequestSimulacaoHabitacional requestSimulacao)
		{
			
			return await _simuladorHabitacionalService.simularhabitacional(requestSimulacao);
		}


	}
}
