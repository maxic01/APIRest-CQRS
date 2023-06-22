using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prueba2.CQRS.Features.Personas.Commands.CreatePersona;
using Prueba2.CQRS.Features.Personas.Commands.DeletePersona;
using Prueba2.CQRS.Features.Personas.Commands.UpdatePersona;
using Prueba2.CQRS.Features.Personas.Queries.GePersonasByNombreApellido;
using Prueba2.CQRS.Features.Personas.Queries.GetAllPersonas;
using Prueba2.CQRS.Features.Personas.Queries.GetPersonasById;
using Prueba2.DTO;
using Prueba2.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Prueba2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<PersonaDTO>> GetPersonas()
        {
            var personas = await _mediator.Send(new GetAllPersonasQuery());

            return personas;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PersonaDTO>> GetPersona(long id)
        {
            try
            {
                var persona = await _mediator.Send(new GetPersonasByIdQuery(id));
                return persona;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Filter")]
        public async Task<ActionResult<PersonaDTO>> GetPersonaByNombreApellido(string nombre, string apellido)
        {
            try
            {
                var query = new GetPersonasByNombreApellidoQuery
                {
                    Nombre = nombre,
                    Apellido = apellido
                };
                var persona = await _mediator.Send(query);
                return persona;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> CreatePersona(CreatePersonaCommand command)
        {
            try
            {
                var respuesta = await _mediator.Send(command);
                return CreatedAtAction(nameof(CreatePersona), respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdatePersona(UpdatePersonaCommand command)
        {
            try
            {
                var respuesta = await _mediator.Send(command);
                return CreatedAtAction(nameof(UpdatePersona), respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeletePersona(long id)
        {
            var command = new DeletePersonaCommand(id);
            try
            {
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

