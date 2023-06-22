using MediatR;
using Prueba2.DTO;

namespace Prueba2.CQRS.Features.Personas.Queries.GePersonasByNombreApellido
{
    public class GetPersonasByNombreApellidoQuery : IRequest<PersonaDTO>
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }
    }
}
