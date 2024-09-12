import { useEffect, useState } from 'react'
import { Button, Container, Card, Row, Col } from 'react-bootstrap'
import './LearnCards.css'
import SwipeInstructions from './SwipeInstructions'

type Props = {}

export const LearnCards = (props: Props) => {
    const initialCards = [
        { id: 0, engWord: 'box', ruWord: 'коробка', imgUrl: 'https://www.celladorales.com/wp-content/uploads/2016/12/ShippingBox_sq.jpg' },
        { id: 1, engWord: 'laptop', ruWord: 'ноутбук', imgUrl: 'https://cdn.thewirecutter.com/wp-content/media/2023/06/bestlaptops-2048px-9765.jpg?auto=webp&quality=75&width=1024' },
        { id: 2, engWord: 'phone', ruWord: 'телефон', imgUrl: 'https://m.media-amazon.com/images/I/61gDg-vFhzL.jpg' },
        { id: 3, engWord: 'cup', ruWord: 'стакан', imgUrl: 'https://www.forlifedesignusa.com/cdn/shop/products/521-MND.jpg?v=1582155776' },
        { id: 4, engWord: 'knife', ruWord: 'нож', imgUrl: 'https://www.newwestknifeworks.com/cdn/shop/files/Western_Cadet-1_2561x.jpg?v=1712175620' }
    ]
    const [cards, setCards] = useState(initialCards)
    const [isFlipped, setIsFlipped] = useState<boolean[]>(cards.map(() => false))
    const [isShaking, setIsShaking] = useState<boolean[]>(cards.map(() => false))
    const [xMoveLeft, setXMoveLeft] = useState<boolean[]>(cards.map(() => false))
    const [xMoveRight, setXMoveRight] = useState<boolean[]>(cards.map(() => false))
    const [learnCounter, setLearnCounter] = useState<number[]>(cards.map(() => 0))
    const [zIndexes, setZIndexes] = useState<number[]>(cards.map((_, index) => index));

    const handleFlip = (index: number) => {
        if (isFlipped[index]) {
            // Если карточка уже перевернута, запускаем анимацию тряски
            setIsShaking(prevState => {
                const newShake = [...prevState];
                newShake[index] = true;
                setTimeout(() => {
                    newShake[index] = false;
                    setIsShaking([...newShake]);
                }, 500); // Длительность анимации тряски
                return newShake;
            });
        } else {
            // Если карточка не перевернута, переворачиваем её
            setIsFlipped(prevState => {
                const newFlip = [...prevState];
                newFlip[index] = true;
                return newFlip;
            });
        }
    };

    const handleTriangleClick = (engWord: string) => {
        // Получаем доступ к доступным голосам
        const voices = window.speechSynthesis.getVoices();
        // Выбираем голос по умолчанию (например, первый голос из доступных)
        const defaultVoice = voices[2];
        // Создаем новый объект для синтеза речи
        const utterance = new SpeechSynthesisUtterance(engWord);
        // Устанавливаем выбранный голос для синтеза речи
        utterance.voice = defaultVoice;
        // Запускаем синтез речи
        window.speechSynthesis.speak(utterance);
    }


    const handleKeyDown = (event: React.KeyboardEvent<HTMLElement>, index: number, id: number, card: any) => {
        if (isFlipped[index]) {
            if (event.key === 'ArrowLeft') {
                setXMoveLeft(prevState => {
                    const newState = [...prevState];
                    newState[index] = true;
                    return newState;
                });
                setIsFlipped(prevState => {
                    const newFlipped = [...prevState];
                    newFlipped[index] = false; // Сбрасываем состояние переворота для карточки
                    return newFlipped;
                });

                setTimeout(() => {
                    setZIndexes(prevState => {
                        const newZIndex = [...prevState];
                        newZIndex[index] = -1;  // Устанавливаем низкий z-index для текущей карточки
                        return newZIndex;
                    });
                }, 250); 

                setTimeout(() => {
                    setCards(prevCards => {
                        const newCards = prevCards.filter(currentCard => currentCard.id !== id); // Удаляем карточку из массива
                        newCards.unshift({ ...card, flipped: false });
                        return newCards;
                    });
                    setXMoveLeft(prevState => {
                        const newState = [...prevState];
                        newState[index] = false;
                        return newState;
                    });
                }, 500);
            } else if (event.key === 'ArrowRight') {
                setLearnCounter(prevState => {
                    const newState = [...prevState]
                    newState[id] = newState[id] + 1
                    return newState
                })
                setXMoveRight(prevState => {
                    const newState = [...prevState]
                    newState[index] = !newState[index]
                    return newState
                })
                setIsFlipped(prevState => {
                    const newFlipped = [...prevState];
                    newFlipped[index] = false; // Сбрасываем состояние переворота для карточки
                    return newFlipped;
                });

                setTimeout(() => {
                    setZIndexes(prevState => {
                        const newZIndex = [...prevState];
                        newZIndex[index] = 0;  // Установить zIndex в 0 после задержки
                        return newZIndex;
                    });
                }, 250); // 250 миллисекунд задержки

                setTimeout(() => {
                    setCards(prevCards => {
                        const newCards = prevCards.filter(currentCard => currentCard.id !== id); // Удаляем карточку из массива
                        if (learnCounter[id] < 3)
                            newCards.unshift({ ...card, flipped: false });

                        return newCards;
                    });
                    setXMoveRight(prevState => {
                        const newState = [...prevState];
                        newState[index] = false;
                        return newState;
                    });
                }, 500);
            }
        }
    }

    return (
        <Container fluid style={{ flex: 1, display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
            <div
                style={{
                    perspective: '1000px',
                    width: '650px',
                    height: '650px',
                    display: 'flex',
                    justifyContent: 'center'
                }}
            >
                {isFlipped[cards.length - 1] &&
                    <SwipeInstructions />
                }

                {cards.map((card, index) =>
                    <Card
                        key={card.id}
                        tabIndex={0}
                        onClick={() => handleFlip(index)}
                        onKeyDown={(event) => handleKeyDown(event, index, card.id, card)}
                        className={`card-container ${isShaking[index] ? 'shake' : ''} ${xMoveLeft[index] ? 'moving-object' : ''} ${xMoveRight[index] ? 'moving-right' : ''}`} // Применяем класс тряски
                        style={{
                            width: '100%',
                            height: '100%',
                            position: 'absolute',
                            transformStyle: 'preserve-3d',
                            transition: 'transform 1s',
                            transform: isFlipped[index]
                                ? 'rotateY(180deg)'
                                : cards.length > 4 && index === 3
                                    ? 'rotate(4deg)'  // turn cards if index 3 and cards.length more than 5
                                    : cards.length > 4 && index === 2
                                        ? 'rotate(-4deg)'  // turn cards if index 2 and cards.length more than 5
                                        : 'none',  // for all cards
                            boxShadow: '0 0px 8px rgba(0, 0, 0, 0.1)', // пофиксить тень для всех карточке
                            borderRadius: '20px',
                            outline: 'none',
                            zIndex: zIndexes[index] ? 0 : index
                        }}>

                        {/* Front Side */}
                        <Card.Body
                            className="card-front"
                            style={{
                                position: 'absolute',
                                width: '100%',
                                height: '100%',
                                backfaceVisibility: 'hidden',
                                display: 'flex',
                                alignItems: 'center',
                                justifyContent: 'center',
                                flexDirection: 'column',
                            }}
                        >
                            <Card.Img style={{ height: '45%', width: '50%', objectFit: 'contain', pointerEvents: 'none', overflow: 'hidden' }} src={card.imgUrl} alt={card.ruWord} />
                            <Card.Text style={{ fontSize: '30px' }}>{card.ruWord}</Card.Text>
                        </Card.Body>

                        {/* Back Side */}
                        <Card.Body
                            className="card-back"
                            style={{
                                position: 'absolute',
                                width: '100%',
                                height: '100%',
                                backfaceVisibility: 'hidden',
                                transform: 'rotateY(180deg)',
                                display: 'flex',
                                alignItems: 'center',
                                justifyContent: 'center',
                                flexDirection: 'column'
                            }}
                        >
                            <Card.Img
                                style={{ height: '45%', width: '50%', objectFit: 'contain', pointerEvents: 'none', overflow: 'hidden' }}
                                src={card.imgUrl}
                                alt={card.engWord}
                            />
                            <Row>
                                <Col style={{ display: 'flex', justifyContent: 'center' }}>
                                    <Button onClick={() => handleTriangleClick(card.engWord)} variant="outline-light" style={{ borderRadius: '50%', display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                                        <svg xmlns="http://www.w3.org/2000/svg" style={{ height: '30px' }} fill="#BBDDFF" viewBox="0 0 384 512">
                                            <path d="M73 39c-14.8-9.1-33.4-9.4-48.5-.9S0 62.6 0 80V432c0 17.4 9.4 33.4 
                                                24.5 41.9s33.7 8.1 48.5-.9L361 297c14.3-8.7 23-24.2 23-41s-8.7-32.2-23-41L73 39z"
                                            />
                                        </svg>
                                    </Button>
                                    <Card.Text style={{ fontSize: '30px' }}>{card.engWord}</Card.Text>
                                </Col>
                            </Row>
                        </Card.Body>
                    </Card>
                )}
            </div>
        </Container >
    )
}