using LaboratorioBack.Data;
using LaboratorioBack.DataBL;
using LaboratorioBack.AutoMap;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

//es mio 
var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
builder.Services.AddControllers();
//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(); //no esta 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//inicio config identity 156
builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<UserManager<IdentityUser>>();
builder.Services.AddScoped<SignInManager<IdentityUser>>();

builder.Services.AddAuthentication().AddJwtBearer(opciones =>
{
    opciones.MapInboundClaims = false;

    opciones.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["keyjwt"]!)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(opciones =>
{
    opciones.AddPolicy("isadmin", policy => policy.RequireClaim("isadmin", "true"));
});

//fin config identity 156

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});


var hostsAccepted = builder.Configuration.GetValue<string>("HostsAccepted")!.Split(',');
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(optionsCORS =>
    {
        optionsCORS.WithOrigins(hostsAccepted).AllowAnyMethod().AllowAnyHeader()
        .WithExposedHeaders("records-total");
    });
});


//cache 80
builder.Services.AddOutputCache(options =>
{
    options.DefaultExpirationTimeSpan = TimeSpan.FromDays(1);
});

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfiles>());

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IServiceUsers, ServiceUsers>();
////builder.Services.AddTransient<ISaludDignaBL>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); //middleware

app.UseStaticFiles();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.UseOutputCache(); //cache

app.Run();