using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Budgetplanerare_GOhman.Models;

namespace WPF_Budgetplanerare_GOhman.ViewModels
{
    public class TransactionOccurrenceViewModel
    {
        public BudgetTransaction Source { get; }
        public DateTimeOffset OccurrenceDate { get; }

        public decimal Amount => Source.Amount;
        public Category? Category => Source.Category;
        public TransactionType TransactionType => Source.TransactionType;

        public TransactionOccurrenceViewModel(BudgetTransaction source, DateTimeOffset occurrenceDate)
        {
            Source = source;
            OccurrenceDate = occurrenceDate;
        }
    }
}
