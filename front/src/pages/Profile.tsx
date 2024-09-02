import { useState } from "react"
import { Container, Image } from "react-bootstrap";


const initialCards = [
  { id: 1, engWord: 'box', ruWord: 'коробка', imgUrl: 'https://www.celladorales.com/wp-content/uploads/2016/12/ShippingBox_sq.jpg' },
  { id: 3, engWord: 'phone', ruWord: 'телефон', imgUrl: 'https://m.media-amazon.com/images/I/61gDg-vFhzL.jpg' },
  { id: 2, engWord: 'laptop', ruWord: 'ноутбук', imgUrl: 'https://cdn.thewirecutter.com/wp-content/media/2023/06/bestlaptops-2048px-9765.jpg?auto=webp&quality=75&width=1024' }
];

export const Profile = () => {
  const [cards, setCards] = useState(initialCards);
  const [stackIndex, setStackIndex] = useState(initialCards.length - 1); // Индекс верхней карточки в стеке

  const handleCardClick = (index : any) => {
    if(index === stackIndex)
    {
      const newCards = [...cards]
      const lastCard = newCards.pop()
      newCards.unshift(lastCard!)
      setCards(newCards)
      setStackIndex(cards.length - 1)
    }
  };

  return (
    <Container>
      {cards.map((card, index) => (
        <div
          key={index}
          style={{  width: '200px'}} //position: 'absolute', Устанавливаем z-index для стека карточек
          onClick={() => handleCardClick(index)}
        >
          <div className="card" style={{border: '2px solid black', height: '200px'}}>
            <Image src={card.imgUrl} height={'150px'} />
            <div className="card-content">
              <p>{card.engWord} - {index}</p>
            </div>
          </div>
        </div>
      ))}
    </Container>
  );
};
