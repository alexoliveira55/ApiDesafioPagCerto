using api.Models.ResultModels;
using api.Models.ServiceModel.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static api.Utils.Utilities;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnticipationRequestController : ControllerBase
    {
        protected readonly IMapper _IMapper;
        private readonly IAnticipationRequestService _IAnticipationRequestService;

        public AnticipationRequestController(IMapper iMapper, IAnticipationRequestService iAnticipationRequestService)
        {
            _IAnticipationRequestService = iAnticipationRequestService;
            _IMapper = iMapper;
        }

        // GET: api/AnticipationRequest
        [HttpGet("get-anticipations-by-status/{status:int}")]
        public async Task<ActionResult<IEnumerable<ResultTransactionAnticipationRequest>>> GetAnticipationRequests(EnStatusAnticipationRequest status)
        {
            return Ok(_IMapper.Map<IEnumerable<ResultTransactionAnticipationRequest>>(await _IAnticipationRequestService.ConsultRequestByStatus(status)));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultAnticipationRequest>> GetAnticipationRequest(long id)
        {
            return Ok(_IMapper.Map<ResultAnticipationRequest>(await _IAnticipationRequestService.GetRequestAnticipationTransactionById(id)));
        }

        [HttpPut("start-analysis-anticipation/{id:long}")]
        public async Task<IActionResult> PutAnticipationStartAnalysis(long id)
        {
            var anticipation = await _IAnticipationRequestService.GetRequestAnticipationTransactionById(id);

            if (anticipation == null) return NotFound(new { sucess = false, id });

            await _IAnticipationRequestService.SetRequestAnticipationInAnalysis(anticipation);

            return NoContent();
        }

        [HttpPut("approve-anticipation-transaction/{id:long}")]
        public async Task<IActionResult> PutTransactionAnticipationApprove(long id, IEnumerable<long> nsuTransactions)
        {
            try
            {
                foreach (var nsu in nsuTransactions)
                {
                    var transactionAnticipation = await _IAnticipationRequestService.GetTransactionAnticipationRequestById(id, nsu);
                    if (transactionAnticipation == null) continue;

                    await _IAnticipationRequestService.ApproveTransactionAntecipationRequest(transactionAnticipation);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro inesperado {ex.Message}");
            }

        }
        [HttpPut("disapprove-anticipation-transaction/{id:long}")]
        public async Task<IActionResult> PutTransactionAnticipationDisapprove(long id, IEnumerable<long> nsuTransactions)
        {
            try
            {
                foreach (var nsu in nsuTransactions)
                {
                    var transactionAnticipation = await _IAnticipationRequestService.GetTransactionAnticipationRequestById(id, nsu);
                    if (transactionAnticipation == null) continue;

                    transactionAnticipation.ResultAnalizeTransaction = EnResultAnalizeAnticipationRequest.Approved.GetHashCode();
                    await _IAnticipationRequestService.DisapproveTransactionAntecipationRequest(transactionAnticipation);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro inesperado {ex.Message}");
            }
        }

        [HttpPost("request-anticipations-transactions")]
        public async Task<ActionResult<ResultAnticipationRequest>> PostAnticipationRequest(IEnumerable<long> nsuTransactions)
        {
            var anticipationRequest = _IMapper.Map<ResultAnticipationRequest>(await _IAnticipationRequestService.RequestAnticipationTransactions(nsuTransactions));

            return CreatedAtAction("GetAnticipationRequest", new { sucess = true, id = anticipationRequest.Id }, anticipationRequest);
        }

    }
}
