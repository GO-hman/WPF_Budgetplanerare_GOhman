using Microsoft.EntityFrameworkCore;

using WPF_Budgetplanerare_GOhman.Models;

namespace WPF_Budgetplanerare_GOhman.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<BudgetTransaction> BudgetTransactions { get; set; }
        public DbSet<RecurringRule> RecurringRules { get; set; }
        public DbSet<RecurrenceException> RecurrenceExceptions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BudgetPlannerDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Guids för transaktioner
            var trans1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var trans2 = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var trans3 = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var trans4 = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var trans5 = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var trans6 = Guid.Parse("66666666-6666-6666-6666-666666666666");
            var trans7 = Guid.Parse("77777777-7777-7777-7777-777777777777");
            var trans8 = Guid.Parse("88888888-8888-8888-8888-888888888888");
            var trans9 = Guid.Parse("99999999-9999-9999-9999-999999999999");
            var trans10 = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

            // Guids för återkommande regler
            var rule1 = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var rule2 = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
            var rule3 = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");


            modelBuilder.Entity<BudgetTransaction>().HasData(
                new BudgetTransaction { Id = trans1, Amount = 5000, Note = "Övertid", EffectiveDate = new DateTime(2025, 1, 25), TransactionType = TransactionType.Inkomst, IsRecurring = false, Category = CategoryEnum.Övertid },
                new BudgetTransaction { Id = trans2, Amount = 1200, Note = "Hyra", EffectiveDate = new DateTime(2025, 1, 1), TransactionType = TransactionType.Utgift, IsRecurring = true, RecurringRuleId = rule1, Category = CategoryEnum.Hyra },
                new BudgetTransaction { Id = trans3, Amount = 800, Note = "Mat", EffectiveDate = new DateTime(2025, 1, 5), TransactionType = TransactionType.Utgift, Category = CategoryEnum.Mat },
                new BudgetTransaction { Id = trans4, Amount = 400, Note = "Transport", EffectiveDate = new DateTime(2025, 1, 10), TransactionType = TransactionType.Utgift, Category = CategoryEnum.Transport },
                new BudgetTransaction { Id = trans5, Amount = 300, Note = "Elräkning", EffectiveDate = new DateTime(2025, 1, 15), TransactionType = TransactionType.Utgift, Category = CategoryEnum.Husdrift },
                new BudgetTransaction { Id = trans6, Amount = 200, Note = "Hemförsäkring", EffectiveDate = new DateTime(2025, 1, 20), TransactionType = TransactionType.Utgift, IsRecurring = true, RecurringRuleId = rule3, Category = CategoryEnum.Hyra },
                new BudgetTransaction { Id = trans7, Amount = 1500, Note = "Frilansjobb", EffectiveDate = new DateTime(2025, 2, 1), TransactionType = TransactionType.Inkomst, Category = CategoryEnum.Hobbyverksamhet },
                new BudgetTransaction { Id = trans8, Amount = 600, Note = "Underhållning", EffectiveDate = new DateTime(2025, 2, 5), TransactionType = TransactionType.Utgift, Category = CategoryEnum.Fritid },
                new BudgetTransaction { Id = trans9, Amount = 1000, Note = "Sparande", EffectiveDate = new DateTime(2025, 2, 10), TransactionType = TransactionType.Utgift, Category = CategoryEnum.Övrigt },
                new BudgetTransaction { Id = trans10, Amount = 2500, Note = "Årsbonus", EffectiveDate = new DateTime(2025, 12, 31), TransactionType = TransactionType.Inkomst, IsRecurring = true, RecurringRuleId = rule2, Category = CategoryEnum.Övrigt }
            );

            modelBuilder.Entity<RecurringRule>().HasData(
                new RecurringRule { Id = rule1, Frequency = Frequency.Månadsvis, StartDate = new DateTime(2025, 1, 1), BudgetTransactionId = trans2 },
                new RecurringRule { Id = rule2, Frequency = Frequency.Årsvis, StartDate = new DateTime(2025, 12, 31), BudgetTransactionId = trans10 },
                new RecurringRule { Id = rule3, Frequency = Frequency.Månadsvis, StartDate = new DateTime(2025, 1, 1), BudgetTransactionId = trans6 }

            );


            modelBuilder.Entity<BudgetTransaction>()
                .HasOne(t => t.RecurringRule)
                .WithOne(r => r.BudgetTransaction)
                .HasForeignKey<RecurringRule>(t => t.BudgetTransactionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RecurringRule>()
                 .HasMany(r => r.RecurrenceExceptions)
                 .WithOne(e => e.RecurringRule)
                 .HasForeignKey(e => e.RecurringRuleId)
                 .OnDelete(DeleteBehavior.Cascade);
        
            modelBuilder.Entity<BudgetTransaction>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
          
            modelBuilder.Entity<BudgetTransaction>()
                .Property(t => t.EffectiveDate)
                .IsRequired();
        }
    }
}
