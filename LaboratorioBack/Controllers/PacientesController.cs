using LaboratorioBack.Data;
using LaboratorioBack.DataBL;
using LaboratorioBack.DTOs;
using LaboratorioBack.Model;
using LaboratorioBack.Utilities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;

namespace LaboratorioBack.Controllers
{
    [Route("api/pacientes")]
    [ApiController] //validaciones
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PacientesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IOutputCacheStore outputCacheStore;
        private readonly IServiceUsers serviceUsers;
        private const string cacheTag = "pacientes";

        public PacientesController(ApplicationDbContext _context, IMapper mapper, IOutputCacheStore outputCacheStore, IServiceUsers serviceUsers)
        {
            this._context = _context;
            this._mapper = mapper;
            this.outputCacheStore = outputCacheStore;
            this.serviceUsers = serviceUsers;
        }

        [HttpGet]
        [OutputCache(Tags = [cacheTag])]
        public async Task<List<PacienteDTO>> Get([FromQuery] PaginationDTO pagination)
        {
            var queryble = _context.Pacientes.Where(p=> p.Status).Include(p=> p.Genero);

            await HttpContext.InjectPaginationParameters(queryble);

            return await queryble
                .OrderBy(p => p.Nombre)
                .Page(pagination)
                .ProjectTo<PacienteDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        [HttpGet("{id:int}", Name = "GetByID")]
        [OutputCache(Tags = [cacheTag])]
        public async Task<ActionResult<PacienteDTO>> Get(int id)
        {
            var paciente = await _context.Pacientes
                .ProjectTo<PacienteDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.PacienteId == id);

            if (paciente is null)
            {
                return NotFound();
            }

            return Ok(paciente);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromBody] PacienteDTO pacienteDTO) {

            var userId = await serviceUsers.GetUserId();

            var paciente = _mapper.Map<Paciente>(pacienteDTO);
            //
            _context.Add(paciente);
            await _context.SaveChangesAsync();
            await outputCacheStore.EvictByTagAsync(cacheTag, default);

            var result =  CreatedAtRoute("GetByID", new { PacienteId = paciente.PacienteId}, paciente);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] PacienteDTO pacienteDto)
        {
            var paciente = await _context.Pacientes.AnyAsync(p => p.PacienteId == id);
            if (paciente)
            {
                pacienteDto.PacienteId = id;
                var newPaciente = _mapper.Map<Paciente>(pacienteDto);
                _context.Update(newPaciente);
                await _context.SaveChangesAsync();
                await outputCacheStore.EvictByTagAsync(cacheTag, default);
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.PacienteId == id);
            if (paciente is not null)
            {
                paciente.Status = false;
                _context.Update(paciente);
                await _context.SaveChangesAsync();
                await outputCacheStore.EvictByTagAsync(cacheTag, default);
                return NoContent();
            }

            return NotFound();
        }
    }
}