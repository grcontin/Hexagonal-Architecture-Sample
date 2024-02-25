using FluentResults;
using MediatR.Pipeline;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Sample.SharedKernel.MediatR.Handlers
{
    public class CommandExceptionHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
    where TRequest : IRequest<TResponse>
    where TResponse : ResultBase<TResponse>, new()
    where TException : Exception
    {
        private readonly ILogger<CommandExceptionHandler<TRequest, TResponse, TException>> _logger;
        public CommandExceptionHandler(ILogger<CommandExceptionHandler<TRequest, TResponse, TException>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
        {
            var excpetionResume = exception.Demystify();

            _logger.LogError(excpetionResume, "Something went wrong while handling request of type {@requestType}", typeof(TRequest));

            var response = new TResponse();

            response.WithError(excpetionResume.Message);

            state.SetHandled(response);

            return Task.CompletedTask;
        }
    }
}
