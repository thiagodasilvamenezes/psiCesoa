package br.com.simulador.controller;

import java.util.List;

import br.com.simulador.model.DadosEmprestimo;
import br.com.simulador.services.ServicoEmprestimo;
import jakarta.ws.rs.Consumes;
import jakarta.ws.rs.POST;
import jakarta.ws.rs.Path;
import jakarta.ws.rs.Produces;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response; // Importação da classe List


@Path("/emprestimo")
public class SimulacaoResource {
    @POST
    @Consumes(MediaType.APPLICATION_JSON)
    @Produces(MediaType.APPLICATION_JSON)
    public Response obterJuros(DadosEmprestimo entrada) {
        ServicoEmprestimo servico = new ServicoEmprestimo();
        try {
            // Prioriza dataReferencia se ambos forem fornecidos
            if (entrada.getDataReferencia() != null && !entrada.getDataReferencia().isEmpty()) {
                Double juros = servico.calcularJurosPorData(entrada);
                return Response.ok(juros).build(); // Retorna apenas o valor do juro
            } else if (entrada.getNumeroPrestacao() != null) {
                Double juros = servico.calcularJurosPorPrestacao(entrada);
                return Response.ok(juros).build(); // Retorna apenas o valor do juro
            } else {
                List<Double> juros = servico.calcularJurosPrimeirasEUltimas(entrada);
                return Response.ok(juros).build(); // Retorna a lista de juros
            }
        } catch (IllegalArgumentException e) {
            return Response.status(Response.Status.BAD_REQUEST)
                    .entity("Erro ao processar os juros: " + e.getMessage())
                    .build();
        }
    }
}

