using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Budgetplanerare_GOhman.Data;

namespace WPF_Budgetplanerare_GOhman.Models
{
    public class BudgetTransaction
    {


        [Key]
        public Guid Id { get; set; } = new Guid();

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public string? Note { get; set; }
        public DateTime EffectiveDate { get; set; }
        public TransactionType TransactionType { get; set; }
        public CategoryEnum Category { get; set; }
        public bool IsRecurring { get; set; }

        public bool IsRecurrence { get; set; } = false;

        public Guid? RecurringRuleId { get; set; }
        public RecurringRule? RecurringRule { get; set; }
    }
    public enum TransactionType
    {
        Inkomst,
        Utgift
    }
}
