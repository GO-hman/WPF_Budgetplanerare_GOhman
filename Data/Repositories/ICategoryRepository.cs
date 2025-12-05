using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Budgetplanerare_GOhman.Models;

namespace WPF_Budgetplanerare_GOhman.Data.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(Guid id);
        Task<Category> GetCategoryByName(string name);

    }
}
