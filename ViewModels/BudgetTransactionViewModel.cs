using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Budgetplanerare_GOhman.Command;
using WPF_Budgetplanerare_GOhman.Data;
using WPF_Budgetplanerare_GOhman.Data.Repositories;
using WPF_Budgetplanerare_GOhman.Models;
using WPF_Budgetplanerare_GOhman.Services;

namespace WPF_Budgetplanerare_GOhman.ViewModels
{
    public class BudgetTransactionViewModel : ViewModelBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly BudgetTransactionService transactionService;

        public DelegateCommand AddCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand SaveCommand { get; }
        public DelegateCommand AbortCommand { get; }
        public DelegateCommand CalculateBudgetCommand { get; }
        public DelegateCommand GenerateForecastCommand { get; }

        private ObservableCollection<BudgetTransactionItemViewModel> _budgetTransactions = new();
        private ObservableCollection<Category> _categories = new();


        public Array TransactionTypes { get; } = Enum.GetValues(typeof(TransactionType));
        public Array RecurrenceOptions { get; } = Enum.GetValues(typeof(Frequency));
        private decimal _monthlyTotals;
        public bool HasSelectedTransaction => SelectedBudgetTransaction != null;

        public BudgetTransactionItemViewModel NewBudgetTransaction;

        public decimal TotalAmount => BudgetTransactions.Sum(x => x.Amount);


        public BudgetTransactionViewModel(ApplicationDbContext dbContext, BudgetTransactionService transactionService)
        {
            this.dbContext = dbContext;
            this.transactionService = transactionService;
            AddCommand = new DelegateCommand(AddTransaction);
            DeleteCommand = new DelegateCommand(DeleteTransaction, CanDelete);
            SaveCommand = new DelegateCommand(SaveTransaction);
            AbortCommand = new DelegateCommand(AbortTransaction);
            CalculateBudgetCommand = new DelegateCommand(CalculateBudget);
            GenerateForecastCommand = new DelegateCommand(GenerateForecast);
            BudgetTransactionsChangeEvent();
        }

        private async void GenerateForecast(object? obj)
        {
            List<DateTime> dateTimes = new List<DateTime>();
            var start = ForecastFromDate;
            var end = ForecastToDate;
            var current = new DateTime(start.Year, start.Month, 1);
            MonthlyForecastTotals.Clear();

            while(current <= end)
            {
                dateTimes.Add(current);
                current = current.AddMonths(1);
            }

            foreach (var date in dateTimes)
            {
                var transactions = await transactionService.GetTransactionsForMonthAsync(date);
                decimal monthExpenses = 0;
                decimal monthIncomes = 0;

                foreach (var transaction in transactions)
                {
                    var monthTransaction = new BudgetTransactionItemViewModel(transaction);

                    if(monthTransaction.TransactionType == TransactionType.Inkomst)
                    {
                        monthIncomes += monthTransaction.Amount;
                    }
                    else
                    {
                        monthExpenses += monthTransaction.Amount;
                    }

                }
                MonthlyForecastTotals.Add(new MonthlyTotalsViewModel
                {
                    Month = date,
                    TotalExpenses = monthExpenses,
                    TotalIncomes = monthIncomes
                });
            }
        }

        public void BudgetTransactionsChangeEvent()
        {
            BudgetTransactions.CollectionChanged += (s, e) =>
            {
                RaisePropertyChanged(nameof(TotalAmount));
                if (e.NewItems != null)
                    foreach (BudgetTransactionItemViewModel item in e.NewItems)
                        item.PropertyChanged += (_, __) => RaisePropertyChanged(nameof(TotalAmount));
            };
        }

        private decimal _workhoursPerYear;
        public decimal WorkhoursPerYear
        {
            get { return _workhoursPerYear; }
            set
            {
                _workhoursPerYear = value;
                RaisePropertyChanged();
            }
        }

        private decimal _yearlyIncome;
        public decimal YearlyIncome
        {
            get { return _yearlyIncome; }
            set
            {
                _yearlyIncome = value;
                RaisePropertyChanged();
            }
        }

        public decimal MonthlyTotals
        {
            get { return _monthlyTotals; }
            set
            {
                _monthlyTotals = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _selectedDate = DateTime.Now;

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    RaisePropertyChanged(nameof(SelectedDate));
                    _ = LoadMonthlyTransactionsAsync(_selectedDate);
                }
            }
        }

        private DateTime _forecastFromDate = DateTime.Now;
        public DateTime ForecastFromDate
        {
            get { return _forecastFromDate; }
            set
            {
                _forecastFromDate = value;
                RaisePropertyChanged(nameof(ForecastFromDate));
            }
        }

        private DateTime _forecastToDate = DateTime.Now.AddMonths(1);
        public DateTime ForecastToDate
        {
            get { return _forecastToDate; }
            set
            {
                _forecastToDate = value;
                RaisePropertyChanged(nameof(ForecastToDate));
            }
        }
        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                _selectedTabIndex = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<BudgetTransactionItemViewModel> BudgetTransactions
        {
            get { return _budgetTransactions; }
            set
            {
                _budgetTransactions = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<MonthlyTotalsViewModel> _monthlyForecastTotals = new();
        public ObservableCollection<MonthlyTotalsViewModel> MonthlyForecastTotals
        {
            get => _monthlyForecastTotals;
            set
            {
                _monthlyForecastTotals = value;
                RaisePropertyChanged();
            }
        }

        private bool _isExpanderExpanded;
        public bool IsExpanderExpanded
        {
            get { return _isExpanderExpanded; }
            set
            {
                _isExpanderExpanded = value;
                RaisePropertyChanged();
            }
        }
        private BudgetTransactionItemViewModel _selectedBudgetTransaction;

        public BudgetTransactionItemViewModel SelectedBudgetTransaction
        {
            get { return _selectedBudgetTransaction; }
            set
            {
                _selectedBudgetTransaction = value;
                RaisePropertyChanged();
                DeleteCommand.RaiseCanExecuteChanged();
                if (value != null)
                {
                    EditableBudgetTransaction = Clone(this);

                        IsExpanderExpanded = true;
                }
                else
                {
                    EditableBudgetTransaction = null;
                    IsExpanderExpanded = false;
                }
            }
        }


        private BudgetTransactionItemViewModel _editableBudgetTransaction;
        public BudgetTransactionItemViewModel EditableBudgetTransaction
        {
            get { return _editableBudgetTransaction; }
            set
            {
                _editableBudgetTransaction = value;
                RaisePropertyChanged();
            }
        }

        public BudgetTransactionItemViewModel Clone(BudgetTransactionViewModel source)
        {
            if (source.SelectedBudgetTransaction is null)
                return null;
            var clone = new BudgetTransactionItemViewModel(SelectedBudgetTransaction.Model);
            return clone;
        }



        private bool CanDelete(object? parameter) => SelectedBudgetTransaction is not null;

        private async void DeleteTransaction(object? parameter)
        {
            if (SelectedBudgetTransaction is not null)
            {
                if (SelectedBudgetTransaction.Id == Guid.Empty)
                {
                    var recId = SelectedBudgetTransaction.RecurringRule?.Id;
                    if (recId != null)
                    {
                        await transactionService.DeleteRecurrenceRuleSingleMonth(SelectedDate, recId);
                        BudgetTransactions.Remove(SelectedBudgetTransaction);
                        SelectedBudgetTransaction = null;
                        return;
                    }
                }
                await transactionService.RemoveFromDatabase(SelectedBudgetTransaction.Id);
                BudgetTransactions.Remove(SelectedBudgetTransaction);
                await LoadMonthlyTransactionsAsync(SelectedDate);
                SelectedBudgetTransaction = null;
            }
        }

        public async Task InitializeData()
        {
            await GetCategories();
            await LoadMonthlyTransactionsAsync(SelectedDate);

        }

        public async Task GetCategories()
        {
            var categories = await dbContext.Categories.ToListAsync();
            Categories.Clear();

            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }

        public async Task LoadMonthlyTransactionsAsync(DateTime date)
        {
            var allTransactions = await transactionService.GetTransactionsForMonthAsync(date);
            BudgetTransactions.Clear();

            foreach (var transaction in allTransactions)
            {
                BudgetTransactions.Add(new BudgetTransactionItemViewModel(transaction));
            }

            CalculateMonthlyTotal();
        }

        private void CalculateMonthlyTotal()
        {
            decimal incomes = BudgetTransactions.Where(tx => tx.TransactionType == TransactionType.Inkomst)
                .Sum(tx => tx.Amount);
            decimal expenses = BudgetTransactions.Where(tx => tx.TransactionType == TransactionType.Utgift).Sum(tx => tx.Amount);
            MonthlyTotals = incomes - expenses;
        }

        public void AddTransaction(object? parameter)
        {
            SelectedBudgetTransaction = null;
            BudgetTransaction budgetTransaction = new BudgetTransaction
            {
                Id = Guid.NewGuid(),
                Amount = 0,
                Category = Categories.FirstOrDefault() ?? null,
                TransactionType = TransactionType.Utgift,
                EffectiveDate = SelectedDate,
                IsRecurring = false,
            };
            var budgetVM = new BudgetTransactionItemViewModel(budgetTransaction);
            EditableBudgetTransaction = budgetVM;
            IsExpanderExpanded = true;
        }

        public async void SaveTransaction(object? parameter)
        {
            if (EditableBudgetTransaction is null)
                return;

            var model = EditableBudgetTransaction.Model;
            try
            {
                await transactionService.AddOrUpdateTransactionAsync(model);
                await LoadMonthlyTransactionsAsync(SelectedDate);
                SelectedBudgetTransaction = null;
                EditableBudgetTransaction = null;
                IsExpanderExpanded = false;
            }
            catch (DbUpdateException ex)
            {
                return;
            }

        }
        public async void CalculateBudget(object? parameter)
        {
            if (WorkhoursPerYear <= 0 || YearlyIncome <= 0)
                return;

            decimal hourlyRate = YearlyIncome / (WorkhoursPerYear);
            decimal monthlyIncome = Math.Round(YearlyIncome / 12, 2);

            BudgetTransaction budgetTransaction = new BudgetTransaction
            {
                Id = Guid.NewGuid(),
                Amount = monthlyIncome,
                Category = new Category{
                    Name = "Månadslön",
                    TransactionType = TransactionType.Inkomst
                    
                },
                EffectiveDate = new DateTime(2020, 1, 25),
                IsRecurring = true,
                RecurringRule = new RecurringRule
                {
                    Amount = monthlyIncome,
                    Frequency = Frequency.Månadsvis,
                    StartDate = new DateTime(2020, 1, 25)
                }
            };

            try
            {
                await transactionService.AddOrUpdateTransactionAsync(budgetTransaction);
                await LoadMonthlyTransactionsAsync(SelectedDate);
                SelectedBudgetTransaction = null;
                EditableBudgetTransaction = null;
                SelectedTabIndex = 0;
                WorkhoursPerYear = 0;
                YearlyIncome = 0;
            }
            catch (DbUpdateException ex)
            {
                return;
            }
        }

        public void AbortTransaction(object? parameter)
        {
            SelectedBudgetTransaction = null;
            EditableBudgetTransaction = null;
            IsExpanderExpanded = false;
        }
    }
}
