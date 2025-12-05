using System.ComponentModel.DataAnnotations;

namespace WPF_Budgetplanerare_GOhman.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        public TransactionType TransactionType { get; set; }
    }
}
