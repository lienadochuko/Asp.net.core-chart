using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace study2.Models
{
    public class Transactions
    {
        [Key]
        public int TransactionsId { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int Amount { get; set; }

        [Column(TypeName = "nvarchar(75)")]
        public string Note { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
