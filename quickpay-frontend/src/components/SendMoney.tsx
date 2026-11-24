
import { useState } from 'react'

function SendMoney() {

  const [receiverId, setReceiverId] = useState('')
  const [amount, setAmount] = useState('')

  function handleSendMoney() {
    if (!receiverId || !amount) {
      alert('Please fill all fields')
      return
    }

    if (Number(amount) <= 0) {
      alert('Amount must be greater than zero')
      return
    }

    alert('Send Money button clicked!')
  }

  return (
    <div className="send-money-card">
      <h2>Send Money</h2>

      <label>Receiver User ID</label>
      <input
        type="number"
        value={receiverId}
        onChange={(e) => setReceiverId(e.target.value)}
        placeholder="Enter receiver ID"
      />

      <label>Amount (₹)</label>
      <input
        type="number"
        value={amount}
        onChange={(e) => setAmount(e.target.value)}
        placeholder="Enter amount"
      />

      <button onClick={handleSendMoney}>
        Send Money
      </button>
    </div>
  )
}

export default SendMoney
