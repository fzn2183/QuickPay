type BalanceCardProps = {
    balance : number
}

function BalanceCard({balance}:BalanceCardProps){
return(
<div className="balance-card">
    <p>Total Wallet Balance</p>
    <h2>{balance}</h2>
    <p>Available Balance</p>
</div>

)

}
export default BalanceCard