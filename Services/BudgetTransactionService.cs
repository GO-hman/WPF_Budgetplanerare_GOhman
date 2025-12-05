using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Budgetplanerare_GOhman.Data;
using WPF_Budgetplanerare_GOhman.Data.Repositories;
using WPF_Budgetplanerare_GOhman.Models;
using WPF_Budgetplanerare_GOhman.ViewModels;

namespace WPF_Budgetplanerare_GOhman.Services
{
    public class BudgetTransactionService
    {
        private readonly IBudgetTransactionRepository transactionRepo;
        private readonly IRecurringRuleRepository recurringRuleRepo;

        public BudgetTransactionService(IBudgetTransactionRepository transactionRepo, IRecurringRuleRepository recurringRuleRepo)
        {
            this.transactionRepo = transactionRepo;
            this.recurringRuleRepo = recurringRuleRepo;
        }

        public async Task<IEnumerable<BudgetTransaction>> GetAllAsync()
        {
            return await transactionRepo.GetAllAsync();
        }

        public async Task<IEnumerable<BudgetTransaction>> GetTransactionsForMonthAsync(DateTime month)
        {
            var transactions = await transactionRepo.GetByMonthAsync(month);
            var recurringrules = await recurringRuleRepo.GetByMonthAsync(month);

            foreach(var rule in recurringrules)
            {
                var effectiveDate = new DateTime(month.Year, month.Month,
                    Math.Min(rule.StartDate.Day, DateTime.DaysInMonth(month.Year, month.Month)));

                bool isException = rule.RecurrenceExceptions.Any(e =>
                    e.Date.Year == effectiveDate.Year &&
                    e.Date.Month == effectiveDate.Month);

                if (isException)
                    continue;

                bool exist = transactions.Any(t => 
                    t.Id == rule.BudgetTransactionId && 
                    t.EffectiveDate.Month == month.Month && 
                    t.EffectiveDate.Year == month.Year);

                if (!exist)
                {
                    var parentTransaction = await transactionRepo.GetByIdAsync(rule.BudgetTransactionId);

                    var newTransaction = new BudgetTransaction
                    {
                        Amount = parentTransaction.Amount,
                        Note = parentTransaction.Note,
                        Category = parentTransaction != null ? parentTransaction.Category : null,
                        EffectiveDate = new DateTime(month.Year, month.Month, rule.StartDate.Day),
                        TransactionType = parentTransaction.TransactionType,
                        IsRecurring = true,
                        IsRecurrence = true,
                        RecurringRuleId = rule.Id,
                        RecurringRule = rule
                    };

                    transactions.Add(newTransaction);
                }
            }
            return transactions;
        }

        public async Task<bool> IsInDatabase(Guid id)
        {
            var transaction = await transactionRepo.GetByIdAsync(id);
            return transaction != null;
        }

        public async Task AddOrUpdateTransactionAsync(BudgetTransaction budgetTransaction)
        {
            var existing = await IsInDatabase(budgetTransaction.Id);
            if(existing == false)
            {
                await transactionRepo.AddAsync(budgetTransaction);

            }
            else
            {
                await transactionRepo.UpdateAsync(budgetTransaction);
                if(budgetTransaction.IsRecurring && budgetTransaction.RecurringRule != null)
                {
                    var existingRule = await recurringRuleRepo.GetByIdAsync(budgetTransaction.RecurringRule.Id);
                    if(existingRule == null)
                    {
                        await recurringRuleRepo.AddAsync(budgetTransaction.RecurringRule);
                    }
                    else
                    {
                        await recurringRuleRepo.UpdateAsync(budgetTransaction.RecurringRule);
                    }
                }
            }
        }
        public async Task RemoveFromDatabase(Guid id)
        {
            await transactionRepo.DeleteAsync(id);

        }

        public async Task DeleteRecurrenceRuleSingleMonth(DateTime month, Guid? recId)
        {
            if (recId == null || recId == Guid.Empty)
                return;

            var recRule = await recurringRuleRepo.GetByIdAsync(recId.Value);
            if(recRule == null)
            {
                return;
            }

            var exception = new RecurrenceException{
                RecurringRuleId = recRule.Id,
                Date = month,
            };

            await recurringRuleRepo.AddExceptionAsync(exception);
        }
    }
}
