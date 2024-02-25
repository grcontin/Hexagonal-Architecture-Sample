using Sample.Domain.Entities;

namespace Sample.Domain.Contratcts
{
    public interface ITodoItemRepository
    {
        Task CreateAsync(TodoItem todoItem);
        Task UpdateAsCompletedAsync(Guid id, bool isComplete);
        Task<TodoItem> GetAsync(Guid id);

    }
}
