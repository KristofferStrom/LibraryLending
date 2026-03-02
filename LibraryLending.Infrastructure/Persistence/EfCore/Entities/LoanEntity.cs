namespace LibraryLending.Infrastructure.Persistence.EfCore.Entities;

internal sealed class LoanEntity
{
    public Guid Id { get; set; }

    public Guid MemberId { get; set; }
    public Guid BookCopyId { get; set; }

    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnedAt { get; set; }
}
