import { createContext, useEffect, useState } from 'react';
import { Container } from 'react-bootstrap';
import { Outlet } from 'react-router-dom';
import './style.css'
import { Card } from './types/Card';
import { cardService } from './services/cardService';

interface ThemeContextType {
  theme: string;
  setTheme: React.Dispatch<React.SetStateAction<string>>
}

interface CardsContextType {
  learnCards: Card[],
  knownCards: Card[],
  learnedCards: Card[]
}


export const ThemeContext = createContext<ThemeContextType | null>(null)
export const CardsContext = createContext<CardsContextType | null>(null)

function App() {
  const [theme, setTheme] = useState('light')
  const [learnCards, setLearnCards] = useState<Card[]>([])
  const [knownCards, setKnownCards] = useState<Card[]>([])
  const [learnedCards, setLearnedCards] = useState<Card[]>([])

  useEffect(() => {
    const fetchCards = async () => {
      try {
        const allCards = await cardService.getAllCards()

        setLearnCards(allCards.filter(card => card.cardStatus === 0)) //To Learn
        setKnownCards(allCards.filter(card => card.cardStatus === 1)) //Known
        setLearnedCards(allCards.filter(card => card.cardStatus === 2)) //Learned
      } catch (error) {
        console.log("Error receiving cards:", error)
      }
    }

    fetchCards()
  }, [learnCards])

  return (
    <ThemeContext.Provider value={{ theme, setTheme }}>
      <CardsContext.Provider value={{ learnCards, knownCards, learnedCards }}>
        <Container fluid style={{ padding: '0' }} id={theme}>
          <Outlet />
        </Container>
      </CardsContext.Provider>
    </ThemeContext.Provider>
  );

}
export default App;