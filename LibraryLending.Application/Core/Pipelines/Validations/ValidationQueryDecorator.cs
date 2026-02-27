namespace LibraryLending.Application.Core.Pipelines.Validations;

using Application.Core.Abstractions.Messaging;
using LibraryLending.Application.Core.Abstractions.Validations;
using LibraryLending.SharedKernel.Results;


public sealed class ValidationQueryDecorator<TQuery, TResponse>
    : IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : Result
{
    private readonly IQueryHandler<TQuery, TResponse> _inner;
    private readonly IValidator<TQuery>? _validator;

    public ValidationQueryDecorator(
        IQueryHandler<TQuery, TResponse> inner,
        IValidator<TQuery>? validator = null)
    {
        _inner = inner;
        _validator = validator;
    }

    public Task<TResponse> Handle(TQuery query, CancellationToken ct)
    {

        var error = _validator?.Validate(query);
        if (error is not null)
            return Task.FromResult((TResponse)(object)Result.Failure(error));


        return _inner.Handle(query, ct);
    }
}