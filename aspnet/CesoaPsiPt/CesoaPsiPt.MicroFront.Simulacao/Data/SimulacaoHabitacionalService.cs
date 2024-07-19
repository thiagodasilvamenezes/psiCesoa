using System.Net.Http.Json;

namespace CesoaPsiPt.MicroFront.Simulacao.Data
{
    public class SimulacaoHabitacionalService
    {
        private readonly HttpClient _httpClient;

        public SimulacaoHabitacionalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<SimulacaoHabitacional> SimularEmprestimo(SimulacaoRequest request)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("http://localhost:6201/api/SimuladorHabtacional/simular", request);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<SimulacaoHabitacional>();
                }
                else
                {
                    // Log ou manipule erros de resposta aqui
                    Console.Error.WriteLine($"Erro na API: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Log ou manipule exceções aqui
                Console.Error.WriteLine($"Erro ao fazer requisição: {ex.Message}");
                return null;
            }
        }



    }

}