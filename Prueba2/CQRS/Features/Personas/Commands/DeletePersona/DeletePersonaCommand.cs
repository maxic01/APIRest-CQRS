using MediatR;
using Prueba2.DTO;
using Prueba2.Models;

namespace Prueba2.CQRS.Features.Personas.Commands.DeletePersona
{
    public record DeletePersonaCommand (long id) : IRequest<Persona>;
}
