namespace LibraryLending.Application.Core.Abstractions.Messaging;

public interface IHasSlowLogThreshold
{
    int SlowLogThresholdMs { get; }
}
