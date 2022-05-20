using api.Models.EntityModel;
using api.Models.ResultModels;
using api.Models.ServiceModel.Interfaces;
using api.Models.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionCardController : ControllerBase
    {
        private readonly ITransactionCardService _ITransactionCardService;
        protected readonly IMapper _IMapper;
        public TransactionCardController(IMapper iMapper, ITransactionCardService iTransactionCardService)
        {
            _IMapper = iMapper;
            _ITransactionCardService = iTransactionCardService;
        }

        // GET: api/TransactionCard/5
        [HttpGet("get-transaction-byNsu/{nsu:long=0}")]
        public async Task<ActionResult<TransactionCard>> GetTransactionCard(long nsu)
        {
            var transactionCard = _IMapper.Map<ResultTransactionCard>(await _ITransactionCardService.GetTransactionWithInstallmentByNsu(nsu));

            if (transactionCard == null) return NotFound();

            return Ok(transactionCard);
        }
        [HttpGet("get-transactions-for-anticipation")]
        public async Task<ActionResult<TransactionCard>> GetTransactionCardForAnticipation()
        {
            var transactionCards = _IMapper.Map<ResultTransactionCard>(await _ITransactionCardService.GetTransactionsForAnticipation());

            return Ok(transactionCards);
        }

        // POST: api/TransactionCard
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("make-payment-with-card")]
        public async Task<ActionResult<ResultTransactionCard>> PostTransactionCard(TransactionCardViewModel transactionCardViewModel)
        {
            if (ModelState.IsValid)
            {
                var transactionCard = _IMapper.Map<ResultTransactionCard>(await _ITransactionCardService.MakePaymentWithCard(transactionCardViewModel));
                return CreatedAtAction("GetTransactionCard", new { nsu = transactionCard.Nsu }, transactionCard);
            }
            return BadRequest(ModelState);
        }

    }
}
