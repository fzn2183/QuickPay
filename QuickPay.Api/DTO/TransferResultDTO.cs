namespace QuickPay.Api.DTO
{
    public class TransferResultDTO
    {
        public int TransferId { get;set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public decimal Amount { get; set; }

        public DateTime CreateAt { get;set; }
        public string Status { get; set; } =string.Empty;

    }
}
