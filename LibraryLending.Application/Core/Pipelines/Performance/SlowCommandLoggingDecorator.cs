using LibraryLending.Application.Core.Abstractions.Messaging;
using LibraryLending.SharedKernel.Results;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace LibraryLending.Application.Core.Pipelines.Performance;


public sealed class SlowCommandLoggingDecorator<TCommand, TResponse>
    : ICommandHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : Result
{
    private readonly ICommandHandler<TCommand, TResponse> _inner;
    private readonly ILogger<SlowCommandLoggingDecorator<TCommand, TResponse>> _logger;
    private readonly int _defaultThresholdMs;

    public SlowCommandLoggingDecorator(
        ICommandHandler<TCommand, TResponse> inner,
        ILogger<SlowCommandLoggingDecorator<TCommand, TResponse>> logger,
        int defaultThresholdMs = 500)
    {
        _inner = inner;
        _logger = logger;
        _defaultThresholdMs = defaultThresholdMs;
    }

    public async Task<TResponse> Handle(TCommand command, CancellationToken ct)
    {
        var sw = Stopwatch.StartNew();

        var result = await _inner.Handle(command, ct);

        sw.Stop();

        var threshold = command is IHasSlowLogThreshold custom
            ? custom.SlowLogThresholdMs
            : _defaultThresholdMs;

        if (sw.ElapsedMilliseconds > threshold)
        {
            _logger.LogWarning(
                "Slow command {CommandName}: {ElapsedMs} ms (threshold {ThresholdMs} ms). Success={Success}",
                typeof(TCommand).Name,
                sw.ElapsedMilliseconds,
                threshold,
                result.IsSuccess);
        }

        return result;
    }
}
