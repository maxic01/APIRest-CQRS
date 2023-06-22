using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prueba2.Data;
using Prueba2.DTO;

namespace Prueba2.CQRS.Features.Personas.Queries.GetAllPersonas
{
    public class GetAllPersonasQueryHandler : IRequestHandler<GetAllPersonasQuery, List<PersonaDTO>>
    {
        private readonly EfdatabaseFirstContext _context;
        private readonly IMapper _mapper;

        public GetAllPersonasQueryHandler(EfdatabaseFirstContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PersonaDTO>> Handle(GetAllPersonasQuery request, CancellationToken cancellationToken)
        {
            var personas = await _context.Personas.ToListAsync();

            var data = _mapper.Map<List<PersonaDTO>>(personas);

            return data;
        }
    }
}
