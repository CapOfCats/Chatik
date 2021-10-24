import React from 'react'
import Chat from './components/Chat'
import './styles/globals.sass'

function App() {
  return (
    <Chat isOpen={true} setIsOpen={() => {}} />
  )
}

export default App
