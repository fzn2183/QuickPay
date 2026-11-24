using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using QuickPay.Api.DTO;
using QuickPay.Api.Services;

namespace QuickPay.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {

        private readonly IWalletService _walletService;                         

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost("NewWallet")]
        public async Task<IActionResult> CreateWalletAsync(CreateWalletDTO dto)
        {
            try
            {
                //service ko call karo
                await _walletService.CreateWalletAsync(dto.UserId);
                return StatusCode(201, "Wallet Created succesfully");
            }
            catch (KeyNotFoundException ex)
            {//USer nahi mila toh

                return NotFound( ex.Message);
            }

            catch (InvalidOperationException rx)
            {
                return Conflict( rx.Message);
            }


                
        }
        [HttpPost("deposit")]
        public async Task <IActionResult> DepositMoneyAsync([FromBody] DepositMoneyDTO  dto)
        {
            try
            {
                //Service ko call kr rhe hain 
                await _walletService.DepositMoneyAsync(dto.UserID, dto.amount);
                return Ok("Deposit Successfully");

            }
            catch (ArgumentException ex)
            {
                return BadRequest( ex.Message);

            }
            catch (InvalidOperationException rx)
            {
                return Conflict( rx.Message);

            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpGet("balance/{id}")]
        public async Task<IActionResult> GetWalletBalanceAsync(int id)
        {
            try
            {
                decimal balance = await _walletService.GetWalletBalanceAsync(id);
                return Ok(new {UserId = id,Balance = balance});

            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            
            catch(KeyNotFoundException ex){
                return NotFound(ex.Message);

            }
        }
    }
}
