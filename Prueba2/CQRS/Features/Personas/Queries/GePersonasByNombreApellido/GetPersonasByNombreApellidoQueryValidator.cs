using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Prueba2.Data;

namespace Prueba2.CQRS.Features.Personas.Queries.GePersonasByNombreApellido
{
    public class GetPersonasByNombreApellidoQueryValidator : AbstractValidator<GetPersonasByNombreApellidoQuery>
    {
        private readonly EfdatabaseFirstContext _context;

        public GetPersonasByNombreApellidoQueryValidator(EfdatabaseFirstContext context)
        {
            RuleFor(p => p.Nombre)
            .NotEmpty().WithMessage("{PropertyName} es obligatorio.")
            .MinimumLength(4).WithMessage("{PropertyName} debe tener al menos 4 letras")
            .MustAsync(PersonaExists).WithMessage("No se encontró ninguna persona con el nombre y apellido proporcionado.");

            RuleFor(p => p.Apellido)
                .NotEmpty().WithMessage("{PropertyName} es obligatorio.")
                .MinimumLength(4).WithMessage("{PropertyName} debe tener al menos 4 letras")
                 .MustAsync(PersonaExists).WithMessage("No se encontró ninguna persona con el nombre y apellido proporcionado.");



            _context = context;
        }

        private async Task<bool> PersonaExists(GetPersonasByNombreApellidoQuery query, string field, CancellationToken token)
        {
            return await _context.Personas.AnyAsync(p => field == "Nombre" ? p.Nombre.Contains(query.Nombre) : p.Apellido.Contains(query.Apellido));
        }
    }
}
