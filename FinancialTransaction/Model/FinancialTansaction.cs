namespace FinancialTransactionApp.Model
{
    public class FinancialTransactionType
    {
        public int Id { get; set; }
        public required string Type { get; set; } // "Income" or "Expense"
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
    }
}
