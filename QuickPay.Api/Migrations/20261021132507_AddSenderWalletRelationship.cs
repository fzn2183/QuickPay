using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickPay.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSenderWalletRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Transfers_FromWalletId",
                table: "Transfers",
                column: "FromWalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Wallets_FromWalletId",
                table: "Transfers",
                column: "FromWalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Wallets_FromWalletId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_FromWalletId",
                table: "Transfers");
        }
    }
}
