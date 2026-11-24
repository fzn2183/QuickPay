import { useState } from 'react'
import BalanceCard from './components/BalanceCard'
import TransactionList from './components/TransactionList'
import SendMoney from './components/SendMoney'
import Sidebar from './components/Sidebar'

import './App.css'
function Welcome(){
  return (<h1>Welcome to QuickPay</h1>)
}


function App() {
  return (
    <div className="app-layout">
      <Sidebar />

      <main className="main-content">
        <h1>Dashboard</h1>
        <p>Welcome back to QuickPay!</p>
        <BalanceCard balance={5000}></BalanceCard>
          <TransactionList/>
          <SendMoney/>


      </main>
    </div>
  )
}


export default App
