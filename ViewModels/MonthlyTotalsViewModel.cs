using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Budgetplanerare_GOhman.ViewModels
{
    public class MonthlyTotalsViewModel
    {
        public DateTime Month { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal TotalIncomes { get; set; }
        public decimal Sum => TotalIncomes - TotalExpenses;
    }
}
