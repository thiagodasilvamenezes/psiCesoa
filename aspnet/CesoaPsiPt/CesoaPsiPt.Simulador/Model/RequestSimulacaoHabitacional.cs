using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CesoaPsiPt.Simulador.Model
{
	public class RequestSimulacaoHabitacional
	{
		[Range(50000.00, double.MaxValue, ErrorMessage = "O valor desejado deve ser maior que 50000.00")]
		public double ValorEmprestimo { get; set; }

		[Range(12, 420, ErrorMessage = "A quantidade de parcelas deve ter entre 12 e 420")]
		public int PrazoTotal { get; set; }

		[Range(0.0000001, 400.00, ErrorMessage = "O valor desejado deve ser maior que 199.99.")]
		public double TaxaJuros { get; set; }

        //[RegularExpression("^(sac|SAC|SaC|saC|Sprice|PRICE|sam|SAM)$", ErrorMessage = "A Tabela de Juros deve ser SAC, PRICE, ou SAM")]

        [TableInterestValidation(new string[] { "SAC", "PRICE", "SAM" })]
        public string TabelaJuros { get; set; } = string.Empty;
    }


    public class TableInterestValidationAttribute : ValidationAttribute
    {
        private readonly string[] validValues;

        public TableInterestValidationAttribute(string[] validValues)
        {
            this.validValues = validValues;
            this.ErrorMessage = "A Tabela de Juros deve ser SAC, PRICE, ou SAM";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || value is not string input)
            {
                return new ValidationResult("O valor fornecido é inválido. A Tabela de Juros deve ser SAC, PRICE, ou SAM");
            }

            input = input.ToUpper(); // Converte para maiúsculas para simplificar a comparação

            if (validValues.Any(v => v == input))
            {
                return ValidationResult.Success; // Este é um retorno de 'null' intencional
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }
        }



    }

}
