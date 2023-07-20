using Escola.Exceptions;
using Escola.Models;
using Escola.Repository;
using Escola.Repository.Interface;
using Escola.Services;
using Moq;
using Newtonsoft.Json;

namespace EscolaTeste.Services
{
    public class MateriaServiceTest
    {
        [Test]
        public void MateriaDuplicada()
        {
            var materiaRepository = new MateriaRepositoryMock();
            var materiaRepositoryMoq = new Mock<IMateriaRepository>();

            var materia = new Materia()
            {
                Nome = "Matemática"
            };
            var expectedMateria = new Materia()
            {
                Nome = "História"
            };

            materiaRepositoryMoq.Setup(x => x.Inserir(materia)).Returns(() =>
            {
                materia.Nome = "Matemática";
                return materia;
            });

            var materiaService = new MateriaService(materiaRepositoryMoq.Object);
            var result = materiaService.Criar(materia);
            Assert.AreNotEqual(JsonConvert.SerializeObject(expectedMateria), JsonConvert.SerializeObject(result));
        }
    }
}
