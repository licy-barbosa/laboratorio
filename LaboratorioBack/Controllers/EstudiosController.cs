using LaboratorioBack.Data;
using LaboratorioBack.DTOs;
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
    [Route("api/estudios")]
    [ApiController] //validaciones
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EstudiosController : Controller
    {
        public IMapper _mapper { get; }
        private const string cacheTag = "resultados";
        private readonly ApplicationDbContext _context;
        private readonly IOutputCacheStore outputCacheStore;

        public EstudiosController(ApplicationDbContext _context, IOutputCacheStore outputCacheStore, IMapper mapper) { 
            this._context = _context;
            this.outputCacheStore = outputCacheStore;
            _mapper = mapper;
        }

        [HttpGet]
        [OutputCache(Tags = [cacheTag])]
        public async Task<List<EstudioDTO>> Index([FromQuery] EstudiosFilter filter)
        {
            var queryble = _context.Estudios.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.NameEstudio)) {
                queryble = queryble.Where(e => e.NameEstudio.Contains(filter.NameEstudio));
            }
            if (filter.From is not null) {
                queryble = queryble.Where(e=> e.Date >= filter.From.Value);
            }
            if (filter.To is not null)
            {
                queryble = queryble.Where(e => e.Date <= filter.To.Value);
            }

            queryble = queryble.Include(e => e.Paciente);

            await HttpContext.InjectPaginationParameters(queryble);

            return await queryble
                .OrderByDescending(p => p.EstudioId)
                .Page(filter.Pagination)
                .ProjectTo<EstudioDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}