namespace LibraryLending.Application.Core.Pipelines.Validations;

using Application.Core.Abstractions.Messaging;
using LibraryLending.Application.Core.Abstractions.Validations;
using LibraryLending.SharedKernel.Results;


public sealed class ValidationCommandDecorator<TCommand, TResponse>
    : ICommandHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : Result
{
    private readonly ICommandHandler<TCommand, TResponse> _inner;
    private readonly IValidator<TCommand>? _validator;

    public ValidationCommandDecorator(
        ICommandHandler<TCommand, TResponse> inner,
         IValidator<TCommand>? validator = null)
    {
        _inner = inner;
        _validator = validator;
    }

    public Task<TResponse> Handle(TCommand command, CancellationToken ct)
    {

        var error = _validator?.Validate(command);
        if (error is not null)
            return Task.FromResult((TResponse)(object)Result.Failure(error));


        return _inner.Handle(command, ct);
    }
}
