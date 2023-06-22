using AutoMapper;
using FluentValidation;
using MediatR;
using Prueba2.CQRS.Features.Personas.Commands.UpdatePersona;
using Prueba2.Data;
using Prueba2.DTO;
using Prueba2.Exceptions;
using Prueba2.Models;

namespace Prueba2.CQRS.Features.Personas.Commands.CreatePersona
{
    public class CreatePersonaCommandHandler : IRequestHandler<CreatePersonaCommand, PersonaDTO>
    {
        private readonly EfdatabaseFirstContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<CreatePersonaCommand> _validator;

        public CreatePersonaCommandHandler(EfdatabaseFirstContext context, IMapper mapper, IValidator<CreatePersonaCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<PersonaDTO> Handle(CreatePersonaCommand request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }

            var personaToCreate = _mapper.Map<Persona>(request);

            await _context.AddAsync(personaToCreate);
            await _context.SaveChangesAsync();

            var personaDTO = _mapper.Map<PersonaDTO>(personaToCreate);

            return personaDTO;
        }
    }
}
