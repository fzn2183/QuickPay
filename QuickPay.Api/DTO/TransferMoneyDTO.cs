using System.ComponentModel.DataAnnotations;

namespace QuickPay.Api.DTO
{
    public class TransferMoneyDTO
    {public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        [Range(typeof(decimal),"0.01","9999999999999")]
        public decimal Amount { get; set; }
        public string Idempotencykey { get; set; } = string.Empty;

    }
}
