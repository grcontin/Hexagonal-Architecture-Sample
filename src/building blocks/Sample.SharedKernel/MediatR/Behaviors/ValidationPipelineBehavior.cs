using FluentResults;
using FluentValidation;
using MediatR;

namespace Sample.SharedKernel.MediatR.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ResultBase<TResponse>, new()
    {
        private readonly IValidator<TRequest> _validator;
        public ValidationPipelineBehavior(IValidator<TRequest> validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if(!validationResult.IsValid)
            {
                var invalidRequest = new TResponse();

                invalidRequest.WithErrors(validationResult.Errors.Select(x => x.ErrorMessage));

                return invalidRequest;
            }

            return await next();     
        }
    }
}
