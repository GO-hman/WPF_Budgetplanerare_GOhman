using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Budgetplanerare_GOhman.Models
{
    public class RecurringRule
    {
        [Key]
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public Frequency Frequency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Guid BudgetTransactionId { get; set; }
        public BudgetTransaction BudgetTransaction { get; set; }
        public ICollection<RecurrenceException> RecurrenceExceptions { get; set; } = new List<RecurrenceException>();
    }

    public enum Frequency
    {
        Månadsvis,
        Årsvis
    }
}
