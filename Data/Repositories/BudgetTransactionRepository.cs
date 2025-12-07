using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Budgetplanerare_GOhman.Models;

namespace WPF_Budgetplanerare_GOhman.Data.Repositories
{
    internal class BudgetTransactionRepository : IBudgetTransactionRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BudgetTransactionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddAsync(BudgetTransaction budgetTransaction)
        {
            await dbContext.BudgetTransactions.AddAsync(budgetTransaction);
            await dbContext.SaveChangesAsync();

        }

        public async Task DeleteAsync(Guid id)
        {
            dbContext.BudgetTransactions.Remove(dbContext.BudgetTransactions.First(bt => bt.Id == id));
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<BudgetTransaction>> GetAllAsync()
        {
            return await dbContext.BudgetTransactions.Include(bt => bt.Category).Include(bt => bt.RecurringRule).ToListAsync();
        }

        public async Task<BudgetTransaction> GetByIdAsync(Guid id)
        {
            return await dbContext.BudgetTransactions.FirstOrDefaultAsync(bt => bt.Id == id);
        }

        public async Task<List<BudgetTransaction>> GetByMonthAsync(DateTime month)
        {
            var start = new DateTime(month.Year, month.Month, 1);
            var end = start.AddMonths(1);

            return await dbContext.BudgetTransactions
                .Include(t => t.RecurringRule)  
                .Where(t => t.EffectiveDate >= start && t.EffectiveDate < end)
                .ToListAsync();
        }

        public async Task UpdateAsync(BudgetTransaction budgetTransaction)
        {
            var existing = await dbContext.BudgetTransactions.Include(r=>r.RecurringRule).FirstOrDefaultAsync(bt => bt.Id == budgetTransaction.Id);
            if (budgetTransaction != null && existing != null)
            {
                if (budgetTransaction.IsRecurring && budgetTransaction.RecurringRule != null)
                {
                    var existingRule = await dbContext.RecurringRules
                        .FirstOrDefaultAsync(r => r.Id == budgetTransaction.RecurringRule.Id);

                    if (existingRule != null)
                    {
                        dbContext.Entry(existingRule).CurrentValues.SetValues(budgetTransaction.RecurringRule);
                    }
                    else
                    {
                        existing.RecurringRule = budgetTransaction.RecurringRule;
                    }
                }
                if(!budgetTransaction.IsRecurring && existing.RecurringRule != null)
                {
                    dbContext.RecurringRules.Remove(existing.RecurringRule);
                }
                dbContext.Entry(existing).CurrentValues.SetValues(budgetTransaction);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
