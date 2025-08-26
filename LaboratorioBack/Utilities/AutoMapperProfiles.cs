using LaboratorioBack.DTOs;
using LaboratorioBack.Model;
using LaboratorioBack.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace LaboratorioBack.AutoMap
{
    public class AutoMapperProfiles :Profile
    {
        public AutoMapperProfiles() {

            ConfigMapperPacientes();
            ConfigMapperGeneros();
            ConfigMapperEstudios();
            ConfigMapperUsers();
        }

        private void ConfigMapperPacientes() {
            // de PacienteDTO -> A  Paciente
            CreateMap<PacienteDTO, Paciente>();
            CreateMap<Paciente, PacienteDTO>();
        }

        private void ConfigMapperGeneros()
        {
            CreateMap<Genero, GeneroDTO>();
        }

        private void ConfigMapperEstudios()
        {
            //forMember 
            CreateMap<Estudio, EstudioDTO>();
        }

        private void ConfigMapperUsers()
        {
            //forMember 
            CreateMap<IdentityUser, UserDTO>();
        }
    }
}
