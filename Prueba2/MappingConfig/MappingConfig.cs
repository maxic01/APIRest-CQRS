using AutoMapper;
using Prueba2.CQRS.Features.Personas.Commands.CreatePersona;
using Prueba2.CQRS.Features.Personas.Commands.UpdatePersona;
using Prueba2.CQRS.Features.Personas.Queries.GePersonasByNombreApellido;
using Prueba2.DTO;
using Prueba2.Models;

namespace Prueba2.MappingConfig
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Persona, PersonaDTO>()
            .ForMember(dest => dest.NombreCompleto, opt => opt.MapFrom(src => src.Nombre + " " + src.Apellido)).ReverseMap();
            
            CreateMap<CreatePersonaCommand, Persona>();
            CreateMap<UpdatePersonaCommand, Persona>().ReverseMap();
        }
    }
}
