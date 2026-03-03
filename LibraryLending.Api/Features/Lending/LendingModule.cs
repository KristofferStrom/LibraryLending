using LibraryLending.Api.Abstractions;

namespace LibraryLending.Api.Features.Lending;


public sealed class LendingModule : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/lending")
            .WithTags("Lending");

        //BorrowBookCopyEndpoints.Map(group);
        //ReturnBookCopyEndpoints.Map(group);
    }
}
