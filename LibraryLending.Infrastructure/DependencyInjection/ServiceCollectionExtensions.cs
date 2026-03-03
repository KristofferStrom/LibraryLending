using LibraryLending.Domain.Aggregates.Books.BookCopies;
using LibraryLending.Domain.Aggregates.LendingPolicies;
using LibraryLending.Domain.Aggregates.Loans;
using LibraryLending.Domain.Aggregates.Members;
using LibraryLending.Domain.Shared.Abstractions;
using LibraryLending.Infrastructure.Persistence.EfCore.Contexts;
using LibraryLending.Infrastructure.Persistence.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryLending.Infrastructure.DependencyInjection;


public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");

        services.AddDbContext<LibraryDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IBookCopyRepository, BookCopyRepository>();
        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddScoped<ILendingPolicyRepository, LendingPolicyRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
