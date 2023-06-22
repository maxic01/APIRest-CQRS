using MediatR;
using Prueba2.DTO;

namespace Prueba2.CQRS.Features.Personas.Commands.UpdatePersona
{
    public class UpdatePersonaCommand : IRequest<PersonaDTO>
    {
        public long Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public long TipoDocumentoId { get; set; }

    }
}
