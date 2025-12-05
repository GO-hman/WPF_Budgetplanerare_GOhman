using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using WPF_Budgetplanerare_GOhman.Data;
using WPF_Budgetplanerare_GOhman.Data.Repositories;
using WPF_Budgetplanerare_GOhman.Services;
using WPF_Budgetplanerare_GOhman.ViewModels;

namespace WPF_Budgetplanerare_GOhman
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider _serviceProvider;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            Resources.MergedDictionaries.Add(
            new ResourceDictionary
            {
                Source = new Uri("Resources/Global.xaml", UriKind.Relative)
            });

            var services = new ServiceCollection();

            ConfigureServices(services);

            _serviceProvider = services.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();


            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();

            services.AddScoped<IBudgetTransactionRepository, BudgetTransactionRepository>();
            services.AddScoped<IRecurringRuleRepository, RecurringRuleRepository>();
            services.AddScoped<BudgetTransactionViewModel>();
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<BudgetTransactionService>();
        }
    }

}
