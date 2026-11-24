namespace QuickPay.Api.Models
{
    public class PaymentIdempotency
    {

   
            public int Id { get; set; }

            public string IdempotencyKey { get; set; } = string.Empty;

            public int FromUserId { get; set; }

            public int ToUserId { get; set; }

            public decimal Amount { get; set; }

            public int? TransferId { get; set; }

            public string Status { get; set; } = "Pending";

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        }
    
}
