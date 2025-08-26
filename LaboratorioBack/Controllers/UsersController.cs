using LaboratorioBack.Data;
using LaboratorioBack.DTOs;
using LaboratorioBack.Utilities;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace LaboratorioBack.Controllers
{
    [Route("api/users")]
    [ApiController] //validaciones
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isadmin")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public UsersController(
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, 
            IConfiguration configuration,
            ApplicationDbContext _context,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            context = _context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDTO>>> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = context.Users.AsQueryable();
            await HttpContext.InjectPaginationParameters(queryable);

            var users = await queryable.ProjectTo<UserDTO>(mapper.ConfigurationProvider)
                .OrderBy(u => u.Email)
                .Page(pagination)
                .ToListAsync();

            return users;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticationResponseDTO>> Register([FromBody] CredentialsUserDTO credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new IdentityUser { UserName = credentials.Email, Email = credentials.Email };
            var result = await userManager.CreateAsync(user, credentials.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return await  GenerateToken(user);
        }



        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticationResponseDTO>> Login([FromBody] CredentialsUserDTO credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await userManager.FindByEmailAsync(credentials.Email);
            if (user is null)
            {
                var errors = BuildErrors();
                return BadRequest(errors);
                //return Unauthorized(new { message = "Invalid email or password." });
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, credentials.Password, lockoutOnFailure:false);
            if (!result.Succeeded)
            {
                var errors = BuildErrors();
                return BadRequest(errors);
                //return Unauthorized(new { message = "Invalid email or password." });
            }

            return await GenerateToken(user);
        }

        [HttpPost("addClaimAdmin")]
        public async Task<IActionResult> AddClaimAdmin([FromBody] EditClaimDTO editClaimDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await userManager.FindByEmailAsync(editClaimDTO.Email);
            if (user is null)
            {
                return NotFound();
            }
            var claim = new Claim("isadmin", "true");
            var result = await userManager.AddClaimAsync(user, claim);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return NoContent();
        }

        [HttpPost("removeClaimAdmin")]
        public async Task<IActionResult> RemoveClaimAdmin([FromBody] EditClaimDTO editClaimDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await userManager.FindByEmailAsync(editClaimDTO.Email);
            if (user is null)
            {
                return NotFound();
            }
            var claim = new Claim("isadmin", "true");
            var result = await userManager.RemoveClaimAsync(user, claim);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return NoContent();
        }

        private IEnumerable<IdentityError> BuildErrors()
        {
            var identityError = new List<IdentityError>();
            identityError.Add(new IdentityError { Code = "InvalidLogin", Description = "Invalid email or password." });

            return identityError;
        }

        private async Task<AuthenticationResponseDTO> GenerateToken(IdentityUser identityUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, identityUser.Id),
                new Claim(ClaimTypes.Email, identityUser.Email!)
            };

            var claimsDB = await userManager.GetClaimsAsync(identityUser);

            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyjwt"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(1);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthenticationResponseDTO
            {
                Token = tokenString,
                Expiration = expiration
            };
        }
    }
}