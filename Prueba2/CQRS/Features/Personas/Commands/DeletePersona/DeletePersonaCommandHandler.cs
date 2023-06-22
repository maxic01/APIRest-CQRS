using FluentValidation;
using MediatR;
using Prueba2.CQRS.Features.Personas.Commands.UpdatePersona;
using Prueba2.Data;
using Prueba2.DTO;
using Prueba2.Exceptions;
using Prueba2.Models;

namespace Prueba2.CQRS.Features.Personas.Commands.DeletePersona
{
    public class DeletePersonaCommandHandler : IRequestHandler<DeletePersonaCommand, Persona>
    {
        private readonly EfdatabaseFirstContext _context;
        private readonly IValidator<DeletePersonaCommand> _validator;

        public DeletePersonaCommandHandler(EfdatabaseFirstContext context, IValidator<DeletePersonaCommand> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<Persona> Handle(DeletePersonaCommand request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }

            var personaToDelete = await _context.Personas.FindAsync(request.id);

            _context.Personas.Remove(personaToDelete);
            await _context.SaveChangesAsync();

            return personaToDelete;
        }
    }
}
