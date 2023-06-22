using AutoMapper;
using FluentValidation;
using MediatR;
using Prueba2.CQRS.Features.Personas.Commands.DeletePersona;
using Prueba2.Data;
using Prueba2.DTO;
using Prueba2.Exceptions;
using Prueba2.Models;

namespace Prueba2.CQRS.Features.Personas.Commands.UpdatePersona
{
    public class UpdatePersonaCommandHandler : IRequestHandler<UpdatePersonaCommand, PersonaDTO>
    {
        private readonly EfdatabaseFirstContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdatePersonaCommand> _validator;

        public UpdatePersonaCommandHandler(EfdatabaseFirstContext context, IMapper mapper, IValidator<UpdatePersonaCommand> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }


        public async Task<PersonaDTO> Handle(UpdatePersonaCommand request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new Exception(validationResult.ToString());
            }

            var personaToUpdate = await _context.Personas.FindAsync(request.Id);

            if(personaToUpdate == null)
            {
                throw new Exception(validationResult.ToString());
            }

            personaToUpdate.Nombre = request.Nombre;
            personaToUpdate.Apellido = request.Apellido;
            personaToUpdate.TipoDocumentoId = request.TipoDocumentoId;

            await _context.SaveChangesAsync();

            var personaDTO = _mapper.Map<PersonaDTO>(personaToUpdate);

            return personaDTO;
        }
    }
}
