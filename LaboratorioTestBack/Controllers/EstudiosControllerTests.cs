using Microsoft.AspNetCore.OutputCaching;
using LaboratorioBack.Models;
using LaboratorioBack.Controllers;
using LaboratorioBack.DTOs;
using Microsoft.EntityFrameworkCore;
using LaboratorioBack.Model;

namespace LaboratorioTestBack.Controllers
{
    [TestClass]
    public sealed class EstudiosControllerTests : BaseTest
    {
        //Paso 1: Preparar
        //Paso 2: Probar
        //Paso 3: Verificar
        [TestMethod]
        public async Task GetEstudios()
        {
            //Paso 1: Preparar 
            var nameBD = Guid.NewGuid().ToString();
            var _context = BuildContex(nameBD);
            var _mapper = ConfigAutoMapper();
            IOutputCacheStore outputCacheStore = null!;

            // Insertar datos de prueba
            _context.Pacientes.AddRange(
                new Paciente { PacienteId = 1, Nombre = "Nombre", Generoid = 1, NSS = 12457896354, CURP = "GODE561231HDFRRN09", Edad = 1, Status = true },
                new Paciente { PacienteId = 2, Nombre = "Nombre", Generoid = 1, NSS = 12457896354, CURP = "GODE561231HDFRRN09", Edad = 1, Status = true }
            );

            _context.Estudios.AddRange(
                new Estudio() { EstudioId = 1, Date = DateTime.Now,
                    NameEstudio = "Estudio 1", 
                    Resultado = 5,
                    PacienteId = 1
                },
                new Estudio(){ EstudioId = 1, Date = DateTime.Now, NameEstudio = "Estudio 1", Resultado = 5, PacienteId = 2 }
            );

            await _context.SaveChangesAsync();

            var _context2 = BuildContex(nameBD);

            if (_context2 is null)
            {
                Console.WriteLine("es null");
            }
            else
            {
                Console.WriteLine("no es null");
            }

            Console.WriteLine(_context2);

            var controller = new EstudiosController(_context2, outputCacheStore, _mapper);
            //Paso 2: Probar 
            var result = await controller.Index(new EstudiosFilter { PageNumber = 1 });
            //Paso 3: Verificar
            Assert.IsNotNull(result);
            //Assert.IsInstanceOfType(result, typeof(IEnumerable<EstudioDto>));
            Assert.AreEqual(expected: 1, actual: result.Count);
        }
    }
}