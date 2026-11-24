
type Transaction = {
  id: number
  name: string
  type: string
  amount: number
}

function TransactionList() {

  const transactions: Transaction[] = [
    { id: 1, name: "Rahul", type: "Received", amount: 500 },
    { id: 2, name: "Aman", type: "Sent", amount: 200 },
    { id: 3, name: "Ali", type: "Received", amount: 1000 }
  ]

  return (
    <div className="transaction-card">

      <h2>Recent Transactions</h2>

      {transactions.map((item) => (
        <div className="transaction-item" key={item.id}>
          <div>
            <h3>{item.name}</h3>
            <p>{item.type}</p>
          </div>

          <h3>
            {item.type === "Received" ? "+" : "-"}₹{item.amount}
          </h3>
        </div>
      ))}

    </div>
  )
}

export default TransactionList
