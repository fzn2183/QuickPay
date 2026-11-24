using System.Globalization;

namespace QuickPay.Api.DTO
{
    public class TransferHistoryDTO
    {
        public int id { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get;set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Type { get; set; }

    }
}
