using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prueba2.Data;
using Prueba2.DTO;

namespace Prueba2.CQRS.Features.Personas.Queries.GePersonasByNombreApellido
{
    public class GetPersonasByNombreApellidoQueryHandler : IRequestHandler<GetPersonasByNombreApellidoQuery, PersonaDTO>
    {
        private readonly EfdatabaseFirstContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<GetPersonasByNombreApellidoQuery> _validator;

        public GetPersonasByNombreApellidoQueryHandler(EfdatabaseFirstContext context, IMapper mapper, IValidator<GetPersonasByNombreApellidoQuery> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<PersonaDTO> Handle(GetPersonasByNombreApellidoQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }

            var persona = await _context.Personas
                .FirstOrDefaultAsync(p => p.Nombre == request.Nombre && p.Apellido == request.Apellido);

            if (persona == null)
            {
                throw new Exception(validationResult.ToString());
            }

            var personaDTO = _mapper.Map<PersonaDTO>(persona);

            return personaDTO;
        }
    
    }
}
