using DashboardApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DashboardApi.DataAccess;

/// <summary>
/// Represents a database context for the Finances database
/// </summary>
public class FinancesDbContext : DbContext
{
    /// <summary>
    /// Transactions table
    /// </summary>
    public DbSet<SavedTransaction> SavedTransactions { get; set; } = null!;

    /// <summary>
    /// Contructor
    /// </summary>
    public FinancesDbContext(DbContextOptions<FinancesDbContext> options) : base(options)
    {
    }
}
