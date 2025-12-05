using WPF_Budgetplanerare_GOhman.Data;
using WPF_Budgetplanerare_GOhman.Models;

namespace WPF_Budgetplanerare_GOhman.ViewModels
{
    public class BudgetTransactionItemViewModel : ViewModelBase
    {
        private readonly BudgetTransaction model;
        public BudgetTransaction Model => model;


        public BudgetTransactionItemViewModel(BudgetTransaction model)
        {
            this.model = model;
        }
        public Guid Id
        {
            get { return model.Id; }
            set { model.Id = value; 
                RaisePropertyChanged();
            }
        }

        public decimal Amount
        {
            get { return model.Amount; }
            set { model.Amount = value;
                RaisePropertyChanged();
            }
        }

        public string? Note
        {
            get { return model.Note; }
            set
            {
                model.Note = value;
                RaisePropertyChanged();
            }
        }

        public CategoryEnum Category
        {
            get { return model.Category; }
            set
            {
                model.Category = value;
                RaisePropertyChanged();
            }
        }

        public TransactionType TransactionType
        {
            get { return model.TransactionType; }
            set
            {
                model.TransactionType = value;
                RaisePropertyChanged();
            }
        }
        public DateTime EffectiveDate
        {
            get { return model.EffectiveDate; }
            set
            {
                model.EffectiveDate = value;
                RaisePropertyChanged();
            }
        }
        public bool IsRecurring
        {
            get { return model.IsRecurring; }
            set
            {
                if (model.IsRecurring != value)
                {
                    model.IsRecurring = value;
                    RaisePropertyChanged();

                    if (value && model.RecurringRule == null)
                    {
                        model.RecurringRule = new RecurringRule
                        {
                            Frequency = Frequency.Månadsvis,
                            StartDate = model.EffectiveDate
                        };
                        RaisePropertyChanged(nameof(RecurringRule));
                    }
                }
            }
        }

        public RecurringRule? RecurringRule => model.RecurringRule;

        public bool IsNotRecurrence => !model.IsRecurrence;
        public bool IsRecurrence => model.IsRecurrence;

    }
}
