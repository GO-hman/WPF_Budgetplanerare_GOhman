using Microsoft.EntityFrameworkCore;

using WPF_Budgetplanerare_GOhman.Models;

namespace WPF_Budgetplanerare_GOhman.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<BudgetTransaction> BudgetTransactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RecurringRule> RecurringRules { get; set; }

        public DbSet<RecurrenceException> RecurrenceExceptions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BudgetPlannerDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //AI-seedad data. Tack AI!
            // Guids för kategorier
            var catMat = Guid.Parse("d1111111-1111-1111-1111-111111111111");
            var catHus = Guid.Parse("d2222222-2222-2222-2222-222222222222");
            var catTransport = Guid.Parse("d3333333-3333-3333-3333-333333333333");
            var catFritid = Guid.Parse("d4444444-4444-4444-4444-444444444444");
            var catStreaming = Guid.Parse("d5555555-5555-5555-5555-555555555555");
            var catLon = Guid.Parse("d6666666-6666-6666-6666-666666666666");
            var catBidrag = Guid.Parse("d7777777-7777-7777-7777-777777777777");
            var catHobby = Guid.Parse("d8888888-8888-8888-8888-888888888888");

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = catMat, Name = "Mat", TransactionType = TransactionType.Utgift },
                new Category { Id = catHus, Name = "Hus & drift", TransactionType = TransactionType.Utgift },
                new Category { Id = catTransport, Name = "Transport", TransactionType = TransactionType.Utgift },
                new Category { Id = catFritid, Name = "Fritid", TransactionType = TransactionType.Utgift },
                new Category { Id = catStreaming, Name = "Streaming-tjänster", TransactionType = TransactionType.Utgift },
                new Category { Id = catLon, Name = "Lön/Övertid", TransactionType = TransactionType.Inkomst },
                new Category { Id = catBidrag, Name = "Bidrag", TransactionType = TransactionType.Inkomst },
                new Category { Id = catHobby, Name = "Hobbyverksamhet", TransactionType = TransactionType.Inkomst }
            );

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

            modelBuilder.Entity<BudgetTransaction>().HasData(
                new BudgetTransaction { Id = trans1, Amount = 5000, Note = "Lön", EffectiveDate = new DateTime(2025, 1, 25), TransactionType = TransactionType.Inkomst, IsRecurring = false, CategoryId = catLon },
                new BudgetTransaction { Id = trans2, Amount = 1200, Note = "Hyra", EffectiveDate = new DateTime(2025, 1, 1), TransactionType = TransactionType.Utgift, IsRecurring = true, RecurringRuleId = rule1, CategoryId = catHus },
                new BudgetTransaction { Id = trans3, Amount = 800, Note = "Mat", EffectiveDate = new DateTime(2025, 1, 5), TransactionType = TransactionType.Utgift, CategoryId = catMat },
                new BudgetTransaction { Id = trans4, Amount = 400, Note = "Transport", EffectiveDate = new DateTime(2025, 1, 10), TransactionType = TransactionType.Utgift, CategoryId = catTransport },
                new BudgetTransaction { Id = trans5, Amount = 300, Note = "Elräkning", EffectiveDate = new DateTime(2025, 1, 15), TransactionType = TransactionType.Utgift, CategoryId = catHus },
                new BudgetTransaction { Id = trans6, Amount = 200, Note = "Försäkring", EffectiveDate = new DateTime(2025, 1, 20), TransactionType = TransactionType.Utgift, CategoryId = catHus },
                new BudgetTransaction { Id = trans7, Amount = 1500, Note = "Frilansjobb", EffectiveDate = new DateTime(2025, 2, 1), TransactionType = TransactionType.Inkomst, CategoryId = catHobby },
                new BudgetTransaction { Id = trans8, Amount = 600, Note = "Underhållning", EffectiveDate = new DateTime(2025, 2, 5), TransactionType = TransactionType.Utgift, CategoryId = catFritid },
                new BudgetTransaction { Id = trans9, Amount = 1000, Note = "Sparande", EffectiveDate = new DateTime(2025, 2, 10), TransactionType = TransactionType.Utgift, CategoryId = catHus },
                new BudgetTransaction { Id = trans10, Amount = 2500, Note = "Årsbonus", EffectiveDate = new DateTime(2025, 12, 31), TransactionType = TransactionType.Inkomst, IsRecurring = true, RecurringRuleId = rule2, CategoryId = catLon }
            );

            modelBuilder.Entity<RecurringRule>().HasData(
                new RecurringRule { Id = rule1, Amount = 1200, Frequency = Frequency.Månadsvis, StartDate = new DateTime(2025, 1, 1), BudgetTransactionId = trans2 },
                new RecurringRule { Id = rule2, Amount = 2500, Frequency = Frequency.Årsvis, StartDate = new DateTime(2025, 12, 31), BudgetTransactionId = trans10 }
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

            modelBuilder.Entity<RecurringRule>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            modelBuilder.Entity<BudgetTransaction>()
                .Property(t => t.EffectiveDate)
                .IsRequired();

        }

    }
}
