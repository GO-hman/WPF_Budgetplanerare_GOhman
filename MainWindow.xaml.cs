using System.Windows;
using WPF_Budgetplanerare_GOhman.Data;
using WPF_Budgetplanerare_GOhman.ViewModels;

namespace WPF_Budgetplanerare_GOhman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BudgetTransactionViewModel viewModel;
        public MainWindow(ApplicationDbContext dbContext, BudgetTransactionViewModel budgetVM)
        {
            InitializeComponent();
            viewModel = budgetVM;
            DataContext = viewModel;
            Loaded += BudgetView_Loaded;
        }

        private async void BudgetView_Loaded(object sender, RoutedEventArgs e)
        {
            await viewModel.InitializeData();
        }
     
    }


}