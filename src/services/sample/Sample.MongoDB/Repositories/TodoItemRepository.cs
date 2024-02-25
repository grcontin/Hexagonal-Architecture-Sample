using MongoDB.Driver;
using Sample.Domain.Contratcts;
using Sample.Domain.Entities;
using Sample.SharedKernel.MongoDb;

namespace Sample.MongoDB.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly IMongoCollection<TodoItem> _collection;

        public TodoItemRepository(MongoDbContext mongoDbContext) 
        {
            _collection = mongoDbContext.GetCollection<TodoItem>(nameof(TodoItem));
        }
        public async Task CreateAsync(TodoItem todoItem)
        {
            await _collection.InsertOneAsync(todoItem);
        }

        public async Task UpdateAsCompletedAsync(Guid id, bool isComplete)
        {
            var filter = Builders<TodoItem>.Filter.Eq(x => x.Id, id);
            var update = Builders<TodoItem>.Update.Set(x => x.IsComplete, isComplete);
            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task<TodoItem> GetAsync(Guid id)
        {
            var filter = Builders<TodoItem>.Filter.Eq(x => x.Id, id);
            return await _collection.Find(filter).SingleOrDefaultAsync();
        }
    }
}
