namespace Sample.SharedKernel.Core
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        protected Entity()
        {
            Id = new Guid();
        }
    }
}
