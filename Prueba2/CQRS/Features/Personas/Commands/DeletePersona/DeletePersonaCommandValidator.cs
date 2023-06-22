using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Prueba2.Data;

namespace Prueba2.CQRS.Features.Personas.Commands.DeletePersona
{
    public class DeletePersonaCommandValidator : AbstractValidator<DeletePersonaCommand>
    {
        private readonly EfdatabaseFirstContext _context;

        public DeletePersonaCommandValidator(EfdatabaseFirstContext context)
        {
            RuleFor(p => p.id)
                .NotNull()
                .NotEmpty()
                .MustAsync(PersonaExists).WithMessage("La persona no existe");

            _context = context;

        }

        private async Task<bool> PersonaExists(long id, CancellationToken token)
        {
            bool isUnique = await _context.Personas.AnyAsync(p => p.Id == id);

            return isUnique;
        }
    }
}
