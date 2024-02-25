using FluentValidation;

namespace Sample.SharedKernel.FluentValidation
{
    public abstract class RequestValidator<T> : AbstractValidator<T>
    {
        protected abstract void Validate();
    }
}
