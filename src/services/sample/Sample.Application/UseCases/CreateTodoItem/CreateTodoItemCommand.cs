using Sample.SharedKernel.MediatR;

namespace Sample.Application.UseCases.CreateTodoItem
{
    public class CreateTodoItemCommand : Command<CreateTodoItemCommandResult>
    {
        public string Name { get; private set; }
        public bool IsComplete { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public CreateTodoItemCommand(string name)
        {
            Name = name;
            IsComplete = false;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
