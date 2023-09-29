using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture
{
    public class DBContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Covenant> Covenants { get; set; }
        public DbSet<Counterparty> Counterparties { get; set; }
        public DbSet<CovenantCondition> CovenantConditions { get; set; }
        public DbSet<CovenantResult> CovenantResults { get; set; }
        public DbSet<ResultNote> ResultNotes { get; set; }
        public DbSet<FinancialData> FinancialDatas { get; set; }
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<CovenantCondition>()
                .HasOne(b => b.Covenant)
                .WithMany(a => a.CovenantConditions)
                .HasForeignKey(b => b.IdCovenant)
                .IsRequired();

            modelBuilder.Entity<CovenantResult>()
                .HasOne(b => b.Covenant)
                .WithMany(d => d.CovenantResults)
                .HasForeignKey(b => b.IdCovenant)
                .IsRequired();

            modelBuilder.Entity<ResultNote>()
                .HasOne(r => r.CovenantResult)
                .WithMany(n => n.ResultNotes)
                .HasForeignKey(r => r.IdCovenantResult)
                .IsRequired();*/
        }

    }
}
