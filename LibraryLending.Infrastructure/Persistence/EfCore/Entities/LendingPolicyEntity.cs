namespace LibraryLending.Infrastructure.Persistence.EfCore.Entities;

internal sealed class LendingPolicyEntity
{
    public Guid Id { get; set; }
    public int LoanDays { get; set; }
    public bool IsActive { get; set; }
}
