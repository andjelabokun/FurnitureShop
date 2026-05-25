import { useState, useEffect } from 'react'
import api from './services/api'

function App() {
  const [boje, setBoje] = useState([])

  useEffect(() => {
    api.get('/Boje')
      .then(response => {
        setBoje(response.data)
        console.log('Uspešno povezano!', response.data)
      })
      .catch(error => {
        console.log('Greška:', error)
      })
  }, [])

  return (
    <div>
      <h1>Test konekcije</h1>
      <ul>
        {boje.map(b => (
          <li key={b.bojaID}>{b.naziv}</li>
        ))}
      </ul>
    </div>
  )
}

export default App
