using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Application.Core.Abstractions.Messaging;

public interface ICommandHandler<in TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : Result
{
    Task<TResponse> Handle(TCommand command, CancellationToken ct);
}
