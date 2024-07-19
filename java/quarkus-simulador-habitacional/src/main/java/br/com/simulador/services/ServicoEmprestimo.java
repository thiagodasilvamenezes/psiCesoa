package br.com.simulador.services;

import java.text.DecimalFormat;
import java.text.DecimalFormatSymbols;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.time.format.DateTimeParseException;
import java.time.temporal.ChronoUnit;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;
import java.util.stream.Collectors;

import br.com.simulador.model.DadosEmprestimo;
import io.smallrye.mutiny.Uni;

public class ServicoEmprestimo {

    private static final LocalDate DATA_CONCESSAO = LocalDate.of(2024, 6, 1);

   
 public List<Double> calcularJuros(DadosEmprestimo entrada) {
        DecimalFormatSymbols symbols = new DecimalFormatSymbols(Locale.US); // Símbolos para o formato americano (ponto decimal)
        DecimalFormat df = new DecimalFormat("#.00", symbols); // Formatador com ponto decimal

        return Uni.createFrom().item(() -> {
            switch (entrada.getTipoAmortizacao()) {
                case "SAC" -> {
                    return calcularJurosSac(entrada).stream()
                            .map(juro -> Double.parseDouble(df.format(juro).replace(",", "."))) // Substitui vírgula por ponto
                            .collect(Collectors.toList());
                }
                case "PRICE" -> {
                    return calcularJurosPrice(entrada).stream()
                            .map(juro -> Double.parseDouble(df.format(juro).replace(",", "."))) // Substitui vírgula por ponto
                            .collect(Collectors.toList());
                }
                case "SAM" -> {
                    return calcularJurosSam(entrada).stream()
                            .map(juro -> Double.parseDouble(df.format(juro).replace(",", "."))) // Substitui vírgula por ponto
                            .collect(Collectors.toList());
                }
                default -> throw new IllegalArgumentException("Tipo de amortização desconhecido.");
            }
        }).await().indefinitely();
    }

    public Double calcularJurosPorPrestacao(DadosEmprestimo entrada) {
        DecimalFormatSymbols symbols = new DecimalFormatSymbols(Locale.US); // Símbolos para o formato americano (ponto decimal)
        DecimalFormat df = new DecimalFormat("#.00", symbols); // Formatador com ponto decimal
        return Double.parseDouble(df.format(calcularJuros(entrada).get(entrada.getNumeroPrestacao() - 1)).replace(",", "."));
    }

    public Double calcularJurosPorData(DadosEmprestimo entrada) {
        DecimalFormatSymbols symbols = new DecimalFormatSymbols(Locale.US); // Símbolos para o formato americano (ponto decimal)
        DecimalFormat df = new DecimalFormat("#.00", symbols); // Formatador com ponto decimal
        try {
            LocalDate dataReferencia = null;


            if (entrada.getDataReferencia() != null && !entrada.getDataReferencia().isEmpty()) {

                

                    DateTimeFormatter formatter = DateTimeFormatter.ofPattern("MM/yyyy/dd");

                    String dataComDia = entrada.getDataReferencia() + "/01";  

                    dataReferencia = LocalDate.parse(dataComDia, formatter);
            }
            List<Double> todosJuros = calcularJuros(entrada);
            int mesesDecorridos = (int) ChronoUnit.MONTHS.between(DATA_CONCESSAO, dataReferencia);
            if (mesesDecorridos < 0 || mesesDecorridos >= todosJuros.size()) {
                throw new IllegalArgumentException("Data de referência inválida.");
            }
            return Double.parseDouble(df.format(todosJuros.get(mesesDecorridos)).replace(",", "."));
        } catch (DateTimeParseException e) {
            throw new IllegalArgumentException("Formato de data inválido. Use MM/yyyy", e);
        }
    }

    public List<Double> calcularJurosPrimeirasEUltimas(DadosEmprestimo entrada) {
        DecimalFormatSymbols symbols = new DecimalFormatSymbols(Locale.US); // Símbolos para o formato americano (ponto decimal)
        DecimalFormat df = new DecimalFormat("#.00", symbols); // Formatador com ponto decimal

        List<Double> todosJuros = calcularJuros(entrada);

        List<Double> primeirasDez = todosJuros.stream()
                .limit(10)
                .sorted()
                .map(juro -> Double.parseDouble(df.format(juro).replace(",", "."))) // Substitui vírgula por ponto
                .collect(Collectors.toList());

        List<Double> ultimasDez = todosJuros.stream()
                .skip(todosJuros.size() - 10)
                .sorted()
                .map(juro -> Double.parseDouble(df.format(juro).replace(",", "."))) // Substitui vírgula por ponto
                .collect(Collectors.toList());

        List<Double> jurosSelecionados = new ArrayList<>(primeirasDez);
        jurosSelecionados.addAll(ultimasDez);

        return jurosSelecionados;
    }
    private List<Double> calcularJurosSac(DadosEmprestimo entrada) {
        List<Double> juros = new ArrayList<>();
        double saldoDevedor = entrada.getValorEmprestimo();
        double taxaJurosMensal = entrada.getTaxaJuros() / 12 / 100;
        double amortizacao = saldoDevedor / entrada.getPrazoTotal();
        for (int i = 0; i < entrada.getPrazoTotal(); i++) {
            double juro = saldoDevedor * taxaJurosMensal;
            juros.add(juro);
            saldoDevedor -= amortizacao;
        }
        return juros;
    }

    private List<Double> calcularJurosPrice(DadosEmprestimo entrada) {
        List<Double> juros = new ArrayList<>();
        double saldoDevedor = entrada.getValorEmprestimo();
        double taxaJurosMensal = entrada.getTaxaJuros() / 12 / 100;
        double fator = Math.pow(1 + taxaJurosMensal, entrada.getPrazoTotal());
        double prestacao = saldoDevedor * taxaJurosMensal * fator / (fator - 1);
        for (int i = 0; i < entrada.getPrazoTotal(); i++) {
            double juro = saldoDevedor * taxaJurosMensal;
            juros.add(juro);
            double amortizacao = prestacao - juro;
            saldoDevedor -= amortizacao;
        }
        return juros;
    }

    private List<Double> calcularJurosSam(DadosEmprestimo entrada) { // Removido parâmetro dataReferencia
        List<Double> juros = new ArrayList<>();
        double saldoDevedor = entrada.getValorEmprestimo();
        double taxaJurosMensal = entrada.getTaxaJuros() / 12 / 100;
        double amortizacao = saldoDevedor / entrada.getPrazoTotal();

        double fatorPrice = Math.pow(1 + taxaJurosMensal, entrada.getPrazoTotal());
        double prestacaoPrice = saldoDevedor * taxaJurosMensal * fatorPrice / (fatorPrice - 1);
        double decrescimo = (prestacaoPrice - amortizacao - saldoDevedor * taxaJurosMensal) / entrada.getPrazoTotal();

        for (int i = 0; i < entrada.getPrazoTotal(); i++) {
            double juroAtual = saldoDevedor * taxaJurosMensal;
            double prestacaoAtual = amortizacao + juroAtual;

            prestacaoAtual -= i * decrescimo;
            double novoJuro = prestacaoAtual - amortizacao;

            if (novoJuro < 0) {
                novoJuro = 0;
            }

            juros.add(novoJuro);
            saldoDevedor -= amortizacao;
        }
        return juros;
    }
}
