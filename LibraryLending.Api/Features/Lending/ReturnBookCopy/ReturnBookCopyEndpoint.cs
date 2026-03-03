using LibraryLending.Api.Extensions;
using LibraryLending.Application.Core.Abstractions.Messaging;
using LibraryLending.Application.Features.Lending.ReturnBookCopy;

namespace LibraryLending.Api.Features.Lending.ReturnBookCopy;


public static class ReturnBookCopyEndpoint
{
    public static void Map(RouteGroupBuilder group)
    {
        group.MapPost("/return", async (
            ReturnBookCopyRequest request,
            IDispatcher dispatcher,
            CancellationToken ct) =>
        {
            var result = await dispatcher.Send(new ReturnBookCopyCommand(request.CopyBarcode), ct);

            return result.ToHttp();
        })
        .WithName("ReturnBookCopy");
    }
}
