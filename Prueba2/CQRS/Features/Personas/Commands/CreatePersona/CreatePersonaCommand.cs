using MediatR;
using Prueba2.DTO;

namespace Prueba2.CQRS.Features.Personas.Commands.CreatePersona
{
    public class CreatePersonaCommand : IRequest<PersonaDTO>
    {
        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; }

        public long TipoDocumentoId { get; set; }
    }
}
