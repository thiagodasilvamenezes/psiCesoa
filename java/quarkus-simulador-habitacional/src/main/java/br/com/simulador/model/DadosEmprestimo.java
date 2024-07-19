package br.com.simulador.model;

public class DadosEmprestimo {
    private double valorEmprestimo;
    private double taxaJuros;
    private String tipoAmortizacao; // "SAC", "PRICE", "SAM"
    private int prazoTotal;
    private String dataReferencia;
    private Integer numeroPrestacao; // Número da prestação de referência

    // Construtores
    public DadosEmprestimo() {}

    public DadosEmprestimo(double valorEmprestimo, double taxaJuros, String tipoAmortizacao, int prazoTotal, String dataReferencia, Integer numeroPrestacao) {
        this.valorEmprestimo = valorEmprestimo;
        this.taxaJuros = taxaJuros;
        this.tipoAmortizacao = tipoAmortizacao;
        this.prazoTotal = prazoTotal;
        this.dataReferencia = dataReferencia;
        this.numeroPrestacao = numeroPrestacao;
    }

    // Getters e Setters
    public double getValorEmprestimo() {
        return valorEmprestimo;
    }

    public void setValorEmprestimo(double valorEmprestimo) {
        this.valorEmprestimo = valorEmprestimo;
    }

    public double getTaxaJuros() {
        return taxaJuros;
    }

    public void setTaxaJuros(double taxaJuros) {
        this.taxaJuros = taxaJuros;
    }

    public String getTipoAmortizacao() {
        return tipoAmortizacao;
    }

    public void setTipoAmortizacao(String tipoAmortizacao) {
        this.tipoAmortizacao = tipoAmortizacao;
    }

    public int getPrazoTotal() {
        return prazoTotal;
    }

    public void setPrazoTotal(int prazoTotal) {
        this.prazoTotal = prazoTotal;
    }

    public String getDataReferencia() {
        return dataReferencia;
    }

    public void setDataReferencia(String dataReferencia) {
        this.dataReferencia = dataReferencia;
    }

    public Integer getNumeroPrestacao() {
        return numeroPrestacao;
    }

    public void setNumeroPrestacao(Integer numeroPrestacao) {
        this.numeroPrestacao = numeroPrestacao;
    }
}
