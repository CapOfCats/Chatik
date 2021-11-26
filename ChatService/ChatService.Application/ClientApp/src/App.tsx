import React, { useState } from 'react'
import Chat from './components/Chat'
import './styles/globals.sass'

function App() {
  const [isOpen, setIsOpen] = useState(false)

  const handleToggleChatButton = () => setIsOpen(prev => !prev)

  return (
    <div>
      <button onClick={handleToggleChatButton}>Открыть/закрыть чат</button>
      <Chat isOpen={isOpen} setIsOpen={setIsOpen} />
    </div>
  )
}

export default App
