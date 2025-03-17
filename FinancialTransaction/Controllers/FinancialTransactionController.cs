using System.ComponentModel.DataAnnotations;
using FinancialTransaction.Accessors;
using FinancialTransaction.Model;
using FinancialTransactionApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace FinancialTransaction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FinancialTransactionController : ControllerBase
    {
        private readonly IFianancialTransactionAcessor _fianancialTransactionAcessor;
        private readonly ILogger<FinancialTransactionController> _logger;
        private readonly IMemoryCache _memoryCache;

        public FinancialTransactionController(ILogger<FinancialTransactionController> logger, IFianancialTransactionAcessor fianancialTransactionAcessor, IMemoryCache memoryCache)
        {
            _logger = logger;
            _fianancialTransactionAcessor = fianancialTransactionAcessor;
            _memoryCache = memoryCache;
        }



        [HttpGet(Name = "GetFinancialTransaction")]
        public async Task<IEnumerable<FinancialTransactionType>> Get([FromQuery, Required] int id)
        {
            try 
            {
                if (!_memoryCache.TryGetValue(id, out IEnumerable<FinancialTransactionType>? result))
                {
                    result = await _fianancialTransactionAcessor.FindAsync(id);

                    if (result !=null)
                    {
                        var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(5));
                        _memoryCache.Set(id, cacheEntryOptions);
                    }

                }

                return result;
            }
            catch (Exception exception)
            {
                var error = exception.GetBaseException().Message;
                return null;
            }


        }




        [HttpPost(Name ="PostFinancialTransaction")]
        public async Task<IActionResult> Post(FinancialTransactionType transaction)
        {
            bool done= await _fianancialTransactionAcessor.SaveAsync(transaction);
            if (done)
                return Ok("Transaction saved successfully");
            return BadRequest("transaction failed to save");  
        }

        [HttpPut (Name ="PutFinancialTransaction")]
        public async Task<IActionResult> Put(FinancialTransactionType transaction)
        {
            if (await _fianancialTransactionAcessor.UpdateAsync(transaction))
                return Ok("Trasaction successfully Updated");

            return BadRequest("Transaction does not exist");
            
        }

        [HttpDelete(Name ="DeleteFinancialTransaction")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _fianancialTransactionAcessor.DeleteAsync(id))
                return Ok("Trasaction successfully deleted");

            return BadRequest("Transaction does not exist");
        }
    }
}
