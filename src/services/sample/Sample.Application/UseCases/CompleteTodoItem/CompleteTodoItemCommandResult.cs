namespace Sample.Application.UseCases.CompleteTodoItem
{
    public class CompleteTodoItemCommandResult
    {
        public Guid Id { get; private set; }

        public CompleteTodoItemCommandResult(Guid id)
        {
            Id = id;
        }
    }
}
