using LibraryLending.Application.Core.Abstractions.Messaging;
using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Application.Features.Lending.ReturnBookCopy;

public sealed record ReturnBookCopyCommand(string CopyBarcode) : ICommand<Result>, IHasSlowLogThreshold
{
    public int SlowLogThresholdMs => 200;

}
