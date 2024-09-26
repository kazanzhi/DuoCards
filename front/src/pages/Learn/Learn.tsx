import { CardsContext } from "../../App";
import { cardService } from "../../services/cardService";
import { Card } from "../../types/Card";
import { LearnCards } from "./LearnCards";
import { LearnHeader } from "./LearnHeader";
import { useContext, useEffect, useState } from "react";

export function Learn() {
    const cardsContext = useContext(CardsContext)
    const { learnCards } = cardsContext || { learnCards: [] }

    const [cards, setCards] = useState<Card[]>([])
    const [cardId, setCardId] = useState<number>()

    useEffect(() => {
        if (learnCards) {
            setCards(learnCards);
        }
    }, [learnCards]); // зависимость от learnCards

    return (
        <main style={{ display: 'flex', flexDirection: 'column', minHeight: '100vh', backgroundColor: '#F3F9FF' }}>
            <LearnHeader cardId={cardId} />
            <LearnCards cards={cards} setCards={setCards} setCardId={setCardId} />
        </main >
    );
}