using LibraryLending.Api.Abstractions;
using LibraryLending.Api.Features.Lending.BorrowBookCopy;

namespace LibraryLending.Api.Features.Lending;


public sealed class LendingModule : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/lending")
            .WithTags("Lending");

        BorrowBookCopyEndpoint.Map(group);
        //ReturnBookCopyEndpoints.Map(group);
    }
}
