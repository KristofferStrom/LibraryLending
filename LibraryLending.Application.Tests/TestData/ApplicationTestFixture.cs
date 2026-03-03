using LibraryLending.Domain.Aggregates.Books.BookCopies;
using LibraryLending.Domain.Aggregates.LendingPolicies;
using LibraryLending.Domain.Aggregates.Loans;
using LibraryLending.Domain.Aggregates.Members;
using LibraryLending.Domain.Shared.Abstractions;
using NSubstitute;

namespace LibraryLending.Application.Tests.TestData;

internal sealed class ApplicationTestFixture
{
    public IMemberRepository Members { get; } = Substitute.For<IMemberRepository>();
    public IBookCopyRepository BookCopies { get; } = Substitute.For<IBookCopyRepository>();
    public ILendingPolicyRepository LendingPolicies { get; } = Substitute.For<ILendingPolicyRepository>();
    public ILoanRepository Loans { get; } = Substitute.For<ILoanRepository>();
    public IUnitOfWork UnitOfWork { get; } = Substitute.For<IUnitOfWork>();
}