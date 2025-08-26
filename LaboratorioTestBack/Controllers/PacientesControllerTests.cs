using Microsoft.AspNetCore.OutputCaching;
using LaboratorioBack.Models;
using LaboratorioBack.Controllers;
using LaboratorioBack.DTOs;
using Microsoft.AspNetCore.Mvc;
using LaboratorioBack.Model;

namespace LaboratorioTestBack.Controllers
{
    [TestClass]
    public sealed class PacientesControllerTests :BaseTest
    {
        //Paso 1: Preparar 
        //Paso 2: Probar 
        //Paso 3: Verificar
        //[TestMethod]
        //public async Task GetEstudios()
        //{
        //    //Paso 1: Preparar 
        //    var nameBD = Guid.NewGuid().ToString();
        //    var _context = BuildContex(nameBD);
        //    var _mapper = ConfigAutoMapper();
        //    IOutputCacheStore outputCacheStore = null!;

        //    _context.Estudios.AddRange(
        //        new Estudio() { EstudioId = 1, NameEstudio = "Estudio 1", Date = DateTime.Now },
        //        new Estudio() { EstudioId = 2, NameEstudio = "Estudio 2", Date = DateTime.Now }
        //    );

        //    await _context.SaveChangesAsync();

        //    var _context2 = BuildContex(nameBD);

        //    var controller = new EstudiosController(_context2, outputCacheStore, _mapper);
        //    //Paso 2: Probar 
        //    var result = await controller.Index(new EstudiosFilter { PageNumber = 1 });
        //    //Paso 3: Verificar
        //    Assert.IsNotNull(result);
        //    //Assert.IsInstanceOfType(result, typeof(IEnumerable<EstudioDto>));
        //    Assert.AreEqual(expected:2, actual: result.Count);
        //}

        //[TestMethod]
        //public async Task GetReturn404NotExist()
        //{
        //    //Paso 1: Preparar 
        //    var nameBD = Guid.NewGuid().ToString();
        //    var _context = BuildContex(nameBD);
        //    var _mapper = ConfigAutoMapper();
        //    IOutputCacheStore outputCacheStore = null!;
        //    var controller = new PacientesController(_context, _mapper, outputCacheStore);
        //    var id = 1;

        //    //Paso 2: Probar 
        //    var answer = await controller.Get(id);

        //    //Paso 3: Verificar
        //    var result = answer.Result as StatusCodeResult;

        //    //Assert.IsInstanceOfType(result, typeof(IEnumerable<EstudioDto>));
        //    Assert.AreEqual(expected: 404, actual: result!.StatusCode);
        //}

        //[TestMethod]
        //public async Task GetReturnPacienteIfExist()
        //{
        //    //Paso 1: Preparar 
        //    var nameBD = Guid.NewGuid().ToString();
        //    var _context = BuildContex(nameBD);
        //    var _mapper = ConfigAutoMapper();
        //    IOutputCacheStore outputCacheStore = null!;

        //    _context.Pacientes.AddRange(
        //        new Paciente() { PacienteId = 1, Nombre = "Paciente 1", Generoid = 1, Status = true },
        //        new Paciente() { PacienteId = 2, Nombre = "Paciente 2", Generoid = 2, Status = true }
        //    );
        //    await _context.SaveChangesAsync();
        //    var _context2 = BuildContex(nameBD);

        //    var controller = new PacientesController(_context, _mapper, outputCacheStore);
        //    var id = 1;

        //    //Paso 2: Probar 
        //    var answer = await controller.Get(id);

        //    //Paso 3: Verificar
        //    var result = answer.Value;

        //    //Assert.IsInstanceOfType(result, typeof(IEnumerable<EstudioDto>));
        //    Assert.AreEqual(expected: id, actual: result!.PacienteId);
        //}
    }
}