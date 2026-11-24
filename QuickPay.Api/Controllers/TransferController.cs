using Microsoft.AspNetCore.Mvc;
using QuickPay.Api.DTO;
using QuickPay.Api.Services;

namespace QuickPay.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferController : ControllerBase
    {
        private readonly ITransferService _service;
        public TransferController(ITransferService service)
        {
            _service = service;
        }
        [HttpPost("send")]
        public async Task <IActionResult> SendMoney([FromBody] TransferMoneyDTO Dto)
        {
            try
            {
                var result = await _service.TransferMoneyAsync(Dto.FromUserId, Dto.ToUserId, Dto.Amount, Dto.Idempotencykey);
                return Ok(result); 
            }
            catch (ArgumentException ex)
            {

                return BadRequest(ex.Message);  

            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            catch (InvalidOperationException rx)
            {
                return Conflict(rx.Message);
                    
            }

        }
        [HttpGet("history/{userId}")]
        public async Task <IActionResult> TransferHistory(int userId)
        {
            try
            {
                var history = await _service.GetTransferHistoryAsync(userId);
                return Ok(history); 
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);

            }
        }

    }
}
