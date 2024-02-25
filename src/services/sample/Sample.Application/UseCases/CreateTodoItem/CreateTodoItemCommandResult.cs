namespace Sample.Application.UseCases.CreateTodoItem
{
    public class CreateTodoItemCommandResult
    {
        public Guid Id { get; private set; }

        public CreateTodoItemCommandResult(Guid id)
        {
            Id = id;
        }
    }
}
