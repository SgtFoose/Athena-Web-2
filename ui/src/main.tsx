import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'

// StrictMode removed: react-leaflet MapContainer does not properly clean up on
// the double-mount cycle, resulting in two map instances stacked on each other.
createRoot(document.getElementById('root')!).render(<App />)
