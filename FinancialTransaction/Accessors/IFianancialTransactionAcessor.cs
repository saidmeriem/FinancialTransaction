
using FinancialTransactionApp.Model;

namespace FinancialTransaction.Accessors
{
    public interface IFianancialTransactionAcessor
    {
        Task<IEnumerable<FinancialTransactionType>> FindAsync(int id);

        Task<bool> SaveAsync(FinancialTransactionType transaction);

        Task<bool> UpdateAsync(FinancialTransactionType transaction);

        Task<bool> DeleteAsync(int id);
    }
}