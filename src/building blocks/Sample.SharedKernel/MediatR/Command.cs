using FluentResults;
using MediatR;

namespace Sample.SharedKernel.MediatR
{

    public abstract class Command<TComandResult> : IRequest<Result<TComandResult>>
    {

    }
}
