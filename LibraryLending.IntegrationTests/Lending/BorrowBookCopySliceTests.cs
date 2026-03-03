using FluentAssertions;
using LibraryLending.Application.Features.Lending.BorrowBookCopy;
using LibraryLending.Infrastructure.Persistence.EfCore.Entities;
using LibraryLending.Infrastructure.Persistence.EfCore.Repositories;
using LibraryLending.IntegrationTests.TestInfrastructure;

namespace LibraryLending.IntegrationTests.Lending;


public class BorrowBookCopySliceTests
{
    [Fact]
    public async Task Borrow_HappyPath_PersistsLoan_AndUpdatesCopyStatus()
    {
        await using var db = new SqliteInMemoryDb();

        // --- Arrange: seed i EN context ---
        var memberId = Guid.NewGuid();
        var bookId = Guid.NewGuid();
        var copyId = Guid.NewGuid();
        var policyId = Guid.NewGuid();

        await using (var seedCtx = db.CreateDbContext())
        {
            seedCtx.Members.Add(new MemberEntity
            {
                Id = memberId,
                MemberNumber = "M-0001",
                FirstName = "Ada",
                LastName = "Lovelace",
                IsActive = true
            });

            seedCtx.Books.Add(new BookEntity
            {
                Id = bookId,
                Title = "DDD Quickly",
                Isbn = "1234567890123"
            });

            seedCtx.BookCopies.Add(new BookCopyEntity
            {
                Id = copyId,
                BookId = bookId,
                Barcode = "BC-0001",

                // Anpassa om din Status lagras på annat sätt:
                Status = 0
            });

            seedCtx.LendingPolicies.Add(new LendingPolicyEntity
            {
                Id = policyId,
                LoanDays = 14,
                IsActive = true
            });

            await seedCtx.SaveChangesAsync();
        }

        // --- Act: kör handler i NY context (ingen tracking-krock) ---
        await using (var ctx = db.CreateDbContext())
        {
            var members = new MemberRepository(ctx);
            var bookCopies = new BookCopyRepository(ctx);
            var lendingPolicies = new LendingPolicyRepository(ctx);
            var loans = new LoanRepository(ctx);
            var uow = new UnitOfWork(ctx);

            var handler = new BorrowBookCopyHandler(members, bookCopies, lendingPolicies, loans, uow);
            var command = new BorrowBookCopyCommand("M-0001", "BC-0001");

            var result = await handler.Handle(command, CancellationToken.None);
            result.IsSuccess.Should().BeTrue();
        }

        // --- Assert: verifiera i NY context för “rent” läge ---
        await using (var assertCtx = db.CreateDbContext())
        {
            assertCtx.Loans.Count().Should().Be(1);

            var persistedCopy = await assertCtx.BookCopies.FindAsync(copyId);
            persistedCopy.Should().NotBeNull();

            persistedCopy!.Status.Should().Be(1);
        }
    }
}