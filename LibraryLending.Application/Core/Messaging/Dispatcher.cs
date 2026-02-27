using LibraryLending.Application.Core.Abstractions.Messaging;
using LibraryLending.SharedKernel.Results;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryLending.Application.Core.Messaging;

public sealed class Dispatcher(IServiceProvider sp) : IDispatcher
{

    public Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken ct = default) where TResponse : Result
    {
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResponse));
        dynamic handler = sp.GetRequiredService(handlerType);

        return handler.Handle((dynamic)command, ct);
    }
    public Task<TResponse> Query<TResponse>(IQuery<TResponse> query, CancellationToken ct = default) where TResponse : Result
    {
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse));
        dynamic handler = sp.GetRequiredService(handlerType);

        return handler.Handle((dynamic)query, ct);
    }
}
