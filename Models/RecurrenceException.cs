using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Budgetplanerare_GOhman.Models
{
    public class RecurrenceException
    {
        [Key]
        public Guid Id { get; set; }
        public Guid RecurringRuleId { get; set; }
        public DateTime Date { get; set; }

        public RecurringRule RecurringRule { get; set; }
    }
}
