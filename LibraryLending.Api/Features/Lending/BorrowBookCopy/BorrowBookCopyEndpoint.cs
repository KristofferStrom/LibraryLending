using LibraryLending.Api.Extensions;
using LibraryLending.Application.Core.Abstractions.Messaging;
using LibraryLending.Application.Features.Lending.BorrowBookCopy;

namespace LibraryLending.Api.Features.Lending.BorrowBookCopy;

public static class BorrowBookCopyEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapPost("/borrow", async (
            BorrowBookCopyRequest request,
            IDispatcher dispatcher,
            CancellationToken ct) =>
        {
            var result = await dispatcher.Send(
                new BorrowBookCopyCommand(request.MemberNumber, request.CopyBarcode),
                ct);

            return result.ToHttp(loanId => loanId);
        })
        .WithName("BorrowBookCopy");
    }
}
