using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sample.Application.UseCases.CompleteTodoItem;
using Sample.Application.UseCases.CreateTodoItem;
using Sample.SharedKernel.FluentResults;

namespace Sample.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/todoItems")]
    [Produces("application/json")]
    public class TodoItemsController : Controller
    {
        private readonly IMediator _mediator;

        public TodoItemsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateTodoItemCommandResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody] CreateTodoItemCommand command)
        {
            var response = await _mediator.Send(command);

            if (response.IsFailed)
                return BadRequest(response.DisplayErrors());

            return Ok(response);
        }

        [HttpPut("{id}/complete")]
        [ProducesResponseType(typeof(CompleteTodoItemCommandResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody]CompleteTodoItemCommand command)
        {
            command.AttachId(id);

            var response = await _mediator.Send(command);

            if(response.IsFailed)
                return BadRequest(response.DisplayErrors());

            return NoContent();
        }


    }
}
