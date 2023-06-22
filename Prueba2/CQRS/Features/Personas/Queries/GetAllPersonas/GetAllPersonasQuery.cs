using MediatR;
using Prueba2.DTO;

namespace Prueba2.CQRS.Features.Personas.Queries.GetAllPersonas
{
    public record GetAllPersonasQuery : IRequest<List<PersonaDTO>>;
}
