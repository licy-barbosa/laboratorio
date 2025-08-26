using AutoMapper;
using LaboratorioBack.AutoMap;
using LaboratorioBack.Data;
using Microsoft.EntityFrameworkCore;

namespace LaboratorioTestBack
{
    public class BaseTest
    {
        protected ApplicationDbContext BuildContex(string nameBD) {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
             .UseInMemoryDatabase(nameBD).Options;

            var dbContext = new ApplicationDbContext(options);
            return dbContext;
        }

        protected IMapper ConfigAutoMapper()
        {
            var config = new MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapperProfiles());
            });

            return config.CreateMapper();
        }
    }
}