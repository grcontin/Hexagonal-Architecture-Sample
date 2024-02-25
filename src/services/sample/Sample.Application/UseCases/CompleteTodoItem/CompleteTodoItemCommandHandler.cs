using AutoMapper;
using FluentResults;
using MediatR;
using Sample.Domain.Contratcts;
using Sample.Domain.Entities;

namespace Sample.Application.UseCases.CompleteTodoItem
{
    public class CompleteTodoItemCommandHandler : IRequestHandler<CompleteTodoItemCommand, Result<CompleteTodoItemCommandResult>>
    {
        private readonly IMapper _mapper;
        private readonly ITodoItemRepository _todoItemRepository;

        public CompleteTodoItemCommandHandler(IMapper mapper, ITodoItemRepository todoItemRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _todoItemRepository = todoItemRepository ?? throw new ArgumentNullException(nameof(todoItemRepository));
        }

        public async Task<Result<CompleteTodoItemCommandResult>> Handle(CompleteTodoItemCommand command, CancellationToken cancellationToken)
        {
            var existingTodoItem = await _todoItemRepository.GetAsync(command.Id);

            if (existingTodoItem is null)
                return Result.Fail($"Todo Item with Id: {command.Id} not found");

            var todoItem = _mapper.Map<TodoItem>(command);

            todoItem.SetUpdateDate(); // Exemplo de Regra de Negócio

            await _todoItemRepository.UpdateAsCompletedAsync(todoItem.Id, todoItem.IsComplete);

            var response = _mapper.Map<CompleteTodoItemCommandResult>(todoItem);

            return Result.Ok(response);
        }
    }
}
