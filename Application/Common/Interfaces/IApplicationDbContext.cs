using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<Card> Cards { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Disposition> Dispositions { get; set; }
        DbSet<Loan> Loans { get; set; }
        DbSet<PermanentOrder> PermanentOrder { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<User> User { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}