using Microsoft.EntityFrameworkCore;
using QuickPay.Api.Models;
namespace QuickPay.Api.Data

{
    public class AppDbContext : DbContext
    {
     public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

       
        public DbSet<User> Users { get; set; }

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<PaymentIdempotency> PaymentIdempotencies { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Wallet>()
                .HasOne(w => w.User)
                .WithOne()
                .HasForeignKey<Wallet>(w => w.UserId);

            modelBuilder.Entity<Wallet>()
                .Property(w => w.Balance)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Transfer>()
                .Property(t => t.Amount)
                .HasPrecision(18, 2);
            
            modelBuilder.Entity<Transfer>()
                .HasOne<Wallet>()
                .WithMany()
                .HasForeignKey(t=>t.FromWalletId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transfer>()
                .HasOne<Wallet>()
                .WithMany()
                .HasForeignKey(t => t.ToWalletId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PaymentIdempotency>()
                .HasIndex(p => p.IdempotencyKey)
                .IsUnique();

            modelBuilder.Entity<PaymentIdempotency>()
                .Property(p=>p.Amount)
                .HasPrecision(18, 2);


        }
      

    }
}
