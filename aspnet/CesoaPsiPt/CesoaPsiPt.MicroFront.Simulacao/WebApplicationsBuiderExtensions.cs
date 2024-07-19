using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using CesoaPsiPt.MicroFront.Simulacao.Data;

namespace CesoaPsiPt.MicroFront.Simulacao
{
    public static class WebApplicationsBuiderExtensions
    {

        public static void RegisterSimuladorHabitacaoServices(this WebApplicationBuilder builder)
        {
            

            builder.Services.AddSingleton<SimulacaoHabitacionalService>();

            builder.Services.AddMudServices();
        }

    }
}
