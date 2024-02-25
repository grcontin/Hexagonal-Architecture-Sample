using FluentValidation;
using Sample.SharedKernel.FluentValidation;

namespace Sample.Application.UseCases.CreateTodoItem
{
    public class CreateTodoItemCommandValidator : RequestValidator<CreateTodoItemCommand>
    {
        public CreateTodoItemCommandValidator() 
        {
            Validate();
        }
        protected override void Validate()
        {
            RuleFor(command => command.Name).NotNull().NotEmpty().WithMessage("Name cannot be null or empty");
        }
    }
}
