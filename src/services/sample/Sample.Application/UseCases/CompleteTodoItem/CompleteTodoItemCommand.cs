using Sample.SharedKernel.MediatR;

namespace Sample.Application.UseCases.CompleteTodoItem
{
    public class CompleteTodoItemCommand : Command<CompleteTodoItemCommandResult>
    {
        public Guid Id { get; private set; }
        public bool IsComplete { get; private set; }

        public CompleteTodoItemCommand(bool isComplete)
        {
            IsComplete = isComplete;
        }

        public void AttachId(Guid id)
        {
            Id = id;
        }
    }
}
