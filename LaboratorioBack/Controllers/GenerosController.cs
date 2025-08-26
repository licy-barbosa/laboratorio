using AutoMapper;
using AutoMapper.QueryableExtensions;
using LaboratorioBack.Data;
using LaboratorioBack.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace LaboratorioBack.Controllers
{
    [Route("api/generos")]
    [ApiController] //validaciones
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GenerosController : Controller
    {
        public IMapper _mapper { get; }
        private readonly ApplicationDbContext _context;

        public GenerosController(ApplicationDbContext _context, IMapper mapper) { 
            this._context = _context;
            this._mapper = mapper;
        }

        [HttpGet]
        [OutputCache]
        public async Task<List<GeneroDTO>> Index()
        {
            return await  _context.Generos
                .ProjectTo<GeneroDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}