using LaboratorioBack.Models;
using LaboratorioBack.Controllers;

namespace LaboratorioTestBack.Controllers
{
    [TestClass]
    public sealed class GenerosControllerTests : BaseTest
    {
        //Paso 1: Preparar 
        //Paso 2: Probar 
        //Paso 3: Verificar
        [TestMethod]
        public async Task GetGeneros()
        {
            //Paso 1: Preparar 
            var nameBD = Guid.NewGuid().ToString();
            var _context = BuildContex(nameBD);
            var _mapper = ConfigAutoMapper();

            _context.Generos.AddRange(
                new Genero() { GeneroId = 1, NameGenero ="Genero 1"},
                new Genero() { GeneroId = 2, NameGenero = "Genero 2"}
            );

            await _context.SaveChangesAsync();

            var _context2 = BuildContex(nameBD);

            var controller = new GenerosController(_context2, _mapper);
            //Paso 2: Probar 
            var result = await controller.Index();
            //Paso 3: Verificar
            Assert.IsNotNull(result);
            Assert.AreEqual(expected: 2, actual: result.Count);
        }
    }
}