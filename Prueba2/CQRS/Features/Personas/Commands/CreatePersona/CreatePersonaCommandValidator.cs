using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Prueba2.Data;

namespace Prueba2.CQRS.Features.Personas.Commands.CreatePersona
{
    public class CreatePersonaCommandValidator : AbstractValidator<CreatePersonaCommand>
    {
        private readonly EfdatabaseFirstContext _context;

        public CreatePersonaCommandValidator(EfdatabaseFirstContext context)
        {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacio")
                .NotNull().WithMessage("{PropertyName} no puede ser nulo")
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacio")
                .MinimumLength(4).WithMessage("{PropertyName} debe tener al menos 4 letras");

            RuleFor(p => p.Apellido)
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacio")
                .NotNull().WithMessage("{PropertyName} no puede ser nulo")
                .MinimumLength(4).WithMessage("{PropertyName} debe tener al menos 4 letras");

            RuleFor(p => p.TipoDocumentoId)
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacio")
                .NotNull().WithMessage("{PropertyName} no puede ser nulo")
                .Must(value => value == 1 || value == 2).WithMessage("{PropertyName} solo puede ser 1 o 2");

            RuleFor(p => p)
                .MustAsync(PersonaUnique).WithMessage("{PropertyName} Esta persona ya existe");

            _context = context;
        }

        private async Task<bool> PersonaUnique(CreatePersonaCommand command, CancellationToken token)
        {
            bool isUnique = await _context.Personas.AnyAsync(p => p.Nombre == command.Nombre && p.Apellido  == command.Apellido);

            return !isUnique;
        }
    }
}
