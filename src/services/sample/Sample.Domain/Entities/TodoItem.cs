using Sample.SharedKernel.Core;

namespace Sample.Domain.Entities
{
    public class TodoItem : Entity
    {
        public string Name { get; private set; }
        public bool IsComplete { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public TodoItem(string name)
        {
            Name = name;
            IsComplete = false;
            CreatedAt = DateTime.UtcNow;
        }


        public void SetStatus(bool isComplete) => IsComplete = isComplete;

        public void SetUpdateDate() => UpdatedAt = DateTime.UtcNow;
    }
}
