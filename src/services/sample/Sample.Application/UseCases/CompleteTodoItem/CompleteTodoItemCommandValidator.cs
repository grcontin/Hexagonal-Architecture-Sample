using FluentValidation;
using Sample.SharedKernel.FluentValidation;

namespace Sample.Application.UseCases.CompleteTodoItem
{
    public class CompleteTodoItemCommandValidator : RequestValidator<CompleteTodoItemCommand>
    {
        public CompleteTodoItemCommandValidator()
        {
            Validate();
        }
        protected override void Validate()
        {
            RuleFor(command => command.Id).NotEmpty().NotNull().WithMessage("Id cannot be null or empty");
        }
    }
}
