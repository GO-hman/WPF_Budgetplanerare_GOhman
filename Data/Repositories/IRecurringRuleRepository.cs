using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Budgetplanerare_GOhman.Models;

namespace WPF_Budgetplanerare_GOhman.Data.Repositories
{
    public interface IRecurringRuleRepository
    {
        Task AddAsync(RecurringRule recurringRule);
        Task<IEnumerable<RecurringRule>> GetAllAsync();
        Task<RecurringRule> GetByIdAsync(Guid id);
        Task UpdateAsync(RecurringRule rr);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<RecurringRule>> GetByMonthAsync(DateTime month);
        Task AddExceptionAsync(RecurrenceException exception);
    }
}
