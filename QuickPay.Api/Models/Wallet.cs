using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace QuickPay.Api.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        //Which user have this wallet
        public int UserId { get; set; }
        //Balance
        public decimal Balance { get;set; }
        //Navigation Property
        public User? User { get; set; } = null;
    }
}
