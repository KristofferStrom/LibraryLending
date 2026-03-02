namespace LibraryLending.Infrastructure.Persistence.EfCore.Entities;

internal sealed class MemberEntity
{
    public Guid Id { get; set; }

    public string MemberNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public bool IsActive { get; set; }
}
