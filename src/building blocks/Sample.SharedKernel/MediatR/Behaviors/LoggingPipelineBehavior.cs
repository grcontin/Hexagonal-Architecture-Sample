﻿using MediatR;
using Microsoft.Extensions.Logging;

namespace Sample.SharedKernel.MediatR.Behaviors
{
    public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;
        public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger) => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            string requestType = request.GetType().Name;

            _logger.LogInformation("Handling Request {RequestName} ({@Request})", requestType, request);

            var response = await next();

            _logger.LogInformation("Request {RequestName} Handled - response: {@Response}", requestType, response);

            return response;
        }
    }
}
