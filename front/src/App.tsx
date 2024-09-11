import { createContext, useState } from 'react';
import { Container } from 'react-bootstrap';
import { Outlet } from 'react-router-dom';
import './style.css'

interface ThemeContextType {
  theme: string;
  setTheme: (theme: string) => void;
}


export const ThemeContext = createContext<ThemeContextType | null>(null)

function App() {
  const [theme, setTheme] = useState('light')

  return (
    <ThemeContext.Provider value={{ theme, setTheme }}>
      <Container fluid style={{ padding: '0' }} id={theme}>
        <Outlet />
      </Container>
    </ThemeContext.Provider>
  );

}
export default App;