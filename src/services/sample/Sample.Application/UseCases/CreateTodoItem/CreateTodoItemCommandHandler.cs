using AutoMapper;
using FluentResults;
using MediatR;
using Sample.Domain.Contratcts;
using Sample.Domain.Entities;

namespace Sample.Application.UseCases.CreateTodoItem
{
    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, Result<CreateTodoItemCommandResult>>
    {
        private readonly IMapper _mapper;
        private readonly ITodoItemRepository _todoItemRepository;

        public CreateTodoItemCommandHandler(IMapper mapper, ITodoItemRepository todoItemRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _todoItemRepository = todoItemRepository ?? throw new ArgumentNullException(nameof(todoItemRepository));
        }
        public async Task<Result<CreateTodoItemCommandResult>> Handle(CreateTodoItemCommand command, CancellationToken cancellationToken)
        {
            var todoItem = _mapper.Map<TodoItem>(command);

            await _todoItemRepository.CreateAsync(todoItem);

            var response = _mapper.Map<CreateTodoItemCommandResult>(todoItem);

            return Result.Ok(response);
        }
    }
}
