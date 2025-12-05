using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Budgetplanerare_GOhman.Models;

namespace WPF_Budgetplanerare_GOhman.Data.Repositories
{
    public interface IBudgetTransactionRepository
    {
        Task AddAsync(BudgetTransaction budgetTransaction);
        Task<List<BudgetTransaction>> GetAllAsync();
        Task<BudgetTransaction> GetByIdAsync(Guid id);
        Task<List<BudgetTransaction>> GetByMonthAsync(DateTime month);
        Task UpdateAsync(BudgetTransaction bt);
        Task DeleteAsync(Guid id);
    }
}
