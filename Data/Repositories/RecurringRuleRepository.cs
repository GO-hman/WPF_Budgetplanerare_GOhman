using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Budgetplanerare_GOhman.Models;

namespace WPF_Budgetplanerare_GOhman.Data.Repositories
{
    public class RecurringRuleRepository : IRecurringRuleRepository
    {
        private readonly ApplicationDbContext dbContext;

        public RecurringRuleRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddAsync(RecurringRule recurringRule)
        {
            await dbContext.RecurringRules.AddAsync(recurringRule);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddExceptionAsync(RecurrenceException exception)
        {
            await dbContext.RecurrenceExceptions.AddAsync(exception);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            dbContext.RecurringRules.Remove(dbContext.RecurringRules.First(rr => rr.Id == id));
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<RecurringRule>> GetAllAsync()
        {
            return await dbContext.RecurringRules.Include(t => t.BudgetTransaction).ToListAsync();
        }

        public async Task<RecurringRule> GetByIdAsync(Guid id)
        {
            return await dbContext.RecurringRules.FirstOrDefaultAsync(rr => rr.Id == id);
        }

        public async Task<IEnumerable<RecurringRule>> GetByMonthAsync(DateTime month)
        {
            var start = new DateTime(month.Year, month.Month, 1);
            var end = start.AddMonths(1);

            var monthly = await dbContext.RecurringRules
                .Include(t => t.BudgetTransaction)
                .Where(r => r.StartDate <= end && (r.EndDate == null || r.EndDate >= start))
                .Where(r => r.Frequency != Frequency.Årsvis
                         || r.BudgetTransaction.EffectiveDate.Month == month.Month)
                .ToListAsync();

            return monthly;
        }

        public async Task UpdateAsync(RecurringRule rr)
        {
            var existing = await dbContext.RecurringRules.FirstOrDefaultAsync(bt => bt.Id == rr.Id);
            if (rr != null && existing != null)
            {
                dbContext.Entry(existing).CurrentValues.SetValues(rr);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
