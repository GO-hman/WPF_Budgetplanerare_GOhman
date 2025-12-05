using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Budgetplanerare_GOhman.Models
{
    public class UserSettings
    {
        public Guid Id { get; set; }
        public decimal YearlyIncome { get; set; }
        public decimal YearlyWorkHours { get; set; }
    }
}
