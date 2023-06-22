using MediatR;
using Prueba2.DTO;

namespace Prueba2.CQRS.Features.Personas.Queries.GetPersonasById
{
    public record GetPersonasByIdQuery(long Id) : IRequest<PersonaDTO>;
}
