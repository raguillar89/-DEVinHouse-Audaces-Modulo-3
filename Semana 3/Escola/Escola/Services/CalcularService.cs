namespace Escola.Services
{
    public class CalcularService
    {      
        public CalcularService()
        {
            CalcularMediaMaiorQueZero(2, 3, 4);
            CalcularMediaMenorQueZero(-2, -3, -4);
            CalcularMediaEntreZeroEDez(1, 4, 10);
        }

        public double CalcularMediaMaiorQueZero(params double[] notas)
        {
            double soma = 0;
            foreach (var nota in notas)
            {
                if (nota >= 0)
                {
                    soma += nota;
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"A nota {nota} deve ser maior ou igual a zero.");
                }
            }
            return soma / notas.Count();
        }

        public double CalcularMediaMenorQueZero(params double[] notas)
        {
            double soma = 0;
            foreach (var nota in notas)
            {
                if (nota < 0)
                {
                    soma += nota;
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"A nota {nota} deve ser menor que zero.");
                }
            }
            return soma / notas.Count();
        }

        public double CalcularMediaEntreZeroEDez(params double[] notas)
        {
            double soma = 0;
            foreach (var nota in notas)
            {
                if (nota >= 0 && nota <= 10)
                {
                    soma += nota;
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"A nota {nota} deve ser maior ou igual a zero e menor ou igual a dez.");
                }
            }
            return soma / notas.Count();
        }
    }
}
