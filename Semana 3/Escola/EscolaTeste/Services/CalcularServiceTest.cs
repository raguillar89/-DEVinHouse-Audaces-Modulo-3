using Escola.Services;

namespace EscolaTeste.Services
{
    public class CalcularServiceTest
    {
        private readonly CalcularService _calcularService;
        public CalcularServiceTest()
        {
            _calcularService = new CalcularService();
        }

        [Test]
        public void CalcularMediaMaiorQueZero()
        {
            var resultado = _calcularService.CalcularMediaMaiorQueZero(2, 3, 4);
            Assert.AreEqual(3, resultado);
        }

        [Test]
        public void CalcularMediaMenorQueZero()
        {
            var resultado = _calcularService.CalcularMediaMenorQueZero(-2, -3, -4);
            Assert.AreEqual(-3, resultado);
        }

        [Test]
        public void CalcularMediaEntreZeroEDez()
        {            
            var resultado = _calcularService.CalcularMediaEntreZeroEDez(1, 4, 10);
            Assert.AreEqual(5, resultado);
        }
    }
}
