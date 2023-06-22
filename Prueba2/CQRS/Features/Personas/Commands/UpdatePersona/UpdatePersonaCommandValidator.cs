using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Prueba2.CQRS.Features.Personas.Commands.CreatePersona;
using Prueba2.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Prueba2.CQRS.Features.Personas.Commands.UpdatePersona
{
    public class UpdatePersonaCommandValidator : AbstractValidator<UpdatePersonaCommand>
    {
        private readonly EfdatabaseFirstContext _context;

        public UpdatePersonaCommandValidator(EfdatabaseFirstContext context)
        {
            RuleFor(p => p.Id)
                .NotNull()
                .NotEmpty()
                .MustAsync(PersonaExists);

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

        private async Task<bool> PersonaUnique(UpdatePersonaCommand command, CancellationToken token)
        {
            bool isUnique = await _context.Personas.AnyAsync(p => p.Nombre == command.Nombre && p.Apellido == command.Apellido);
            return !isUnique;
        }

        private async Task<bool> PersonaExists(long id, CancellationToken token)
        {
            bool isUnique = await _context.Personas.AnyAsync(p => p.Id == id);

            return isUnique;
        }
    }
}
