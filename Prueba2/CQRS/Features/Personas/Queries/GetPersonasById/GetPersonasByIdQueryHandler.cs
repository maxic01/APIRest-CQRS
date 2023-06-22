using AutoMapper;
using FluentValidation;
using MediatR;
using Prueba2.CQRS.Features.Personas.Commands.CreatePersona;
using Prueba2.Data;
using Prueba2.DTO;
using Prueba2.Exceptions;

namespace Prueba2.CQRS.Features.Personas.Queries.GetPersonasById
{
    public class GetPersonasByIdQueryHandler : IRequestHandler<GetPersonasByIdQuery, PersonaDTO>
    {
        private readonly EfdatabaseFirstContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<GetPersonasByIdQuery> _validator;

        public GetPersonasByIdQueryHandler(EfdatabaseFirstContext context, IMapper mapper, IValidator<GetPersonasByIdQuery> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<PersonaDTO> Handle(GetPersonasByIdQuery request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }

            var persona = await _context.Personas.FindAsync(request.Id);

            var personaDTO = _mapper.Map<PersonaDTO>(persona);

            return personaDTO;
        }
    }
}
