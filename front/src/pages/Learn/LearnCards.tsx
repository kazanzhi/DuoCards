import { useEffect, useState } from 'react'
import { Button, Container, Card as CardReact, Row, Col } from 'react-bootstrap'
import './LearnCards.css'
import SwipeInstructions from './SwipeInstructions'
import { cardService } from '../../services/cardService'
import { Card } from '../../types/Card'

interface Props {
    cards: Card[],
    setCards: React.Dispatch<React.SetStateAction<Card[]>>,
    setCardId: React.Dispatch<React.SetStateAction<number | undefined>>
}

export const LearnCards: React.FC<Props> = ({ cards, setCards, setCardId }) => {

    const [isFlipped, setIsFlipped] = useState<boolean[]>(cards.map(() => false))
    const [isShaking, setIsShaking] = useState<boolean[]>(cards.map(() => false))
    const [xMoveLeft, setXMoveLeft] = useState<boolean[]>(cards.map(() => false))
    const [xMoveRight, setXMoveRight] = useState<boolean[]>(cards.map(() => false))
    const [zIndexes, setZIndexes] = useState<number[]>(cards.map((_, index) => index));

    useEffect(() => {
        if (cards.length > 0) {
            // Устанавливаем id последней карточки как текущий
            setCardId(cards[cards.length - 1].id);
        }
    }, [cards, isFlipped]);

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


    const handleKeyDown = async (event: React.KeyboardEvent<HTMLElement>, index: number, id: number, card: any) => {
        if (isFlipped[index]) {
            const moveCard = (isCorrect: boolean) => {
                setTimeout(() => {
                    setZIndexes(prevState => {
                        const newZIndex = [...prevState];
                        newZIndex[index] = -1;
                        return newZIndex;
                    });
                }, 250);

                setTimeout(() => {
                    setCards(prevCards => {
                        const newCards = prevCards.filter(currentCard => currentCard.id !== id);
                        newCards.unshift({ ...card, flipped: false });
                        return newCards;
                    });

                    if (isCorrect) {
                        setXMoveRight(prevState => {
                            const newState = [...prevState];
                            newState[index] = false;
                            return newState;
                        });
                    } else {
                        setXMoveLeft(prevState => {
                            const newState = [...prevState];
                            newState[index] = false;
                            return newState;
                        });
                    }
                }, 500);
            };

            if (event.key === 'ArrowLeft') {
                try {
                    await cardService.incorrectAnswer(id);
                } catch (error) {
                    console.log("No answer given", error);
                }
                setXMoveLeft(prevState => {
                    const newState = [...prevState];
                    newState[index] = true;
                    return newState;
                });
                setIsFlipped(prevState => {
                    const newFlipped = [...prevState];
                    newFlipped[index] = false;
                    return newFlipped;
                });
                moveCard(false);
            } else if (event.key === 'ArrowRight') {
                try {
                    await cardService.correctAnswer(id);
                } catch (error) {
                    console.log("No answer given", error);
                }
                setXMoveRight(prevState => {
                    const newState = [...prevState];
                    newState[index] = true;
                    return newState;
                });
                setIsFlipped(prevState => {
                    const newFlipped = [...prevState];
                    newFlipped[index] = false;
                    return newFlipped;
                });
                moveCard(true);
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
                    <CardReact
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
                            //boxShadow: '0 0px 8px rgba(0, 0, 0, 0.1)', // пофиксить тень для всех карточке
                            borderRadius: '20px',
                            outline: 'none',
                            zIndex: zIndexes[index] ? 0 : index
                        }}>

                        {/* Front Side */}
                        <CardReact.Body
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
                            <CardReact.Img style={{ height: '45%', width: '50%', objectFit: 'contain', pointerEvents: 'none', overflow: 'hidden' }} src={card.imgUrl} alt={card.ruWord} />
                            <CardReact.Text style={{ fontSize: '30px' }}>{card.ruWord}</CardReact.Text>
                        </CardReact.Body>

                        {/* Back Side */}
                        <CardReact.Body
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
                            <CardReact.Img
                                style={{ height: '45%', width: '50%', objectFit: 'contain', pointerEvents: 'none', overflow: 'hidden' }}
                                src={card.imgUrl}
                                alt={card.engWord}
                            />
                            <Row>
                                <Col style={{ display: 'flex', justifyContent: 'center', flexDirection: 'column', alignItems: 'center' }}>
                                    <div style={{ display: 'flex' }}>
                                        <Button onClick={() => handleTriangleClick(card.engWord)} variant="outline-light" style={{ borderRadius: '50%', display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                                            <svg xmlns="http://www.w3.org/2000/svg" style={{ height: '30px' }} fill="#BBDDFF" viewBox="0 0 384 512">
                                                <path d="M73 39c-14.8-9.1-33.4-9.4-48.5-.9S0 62.6 0 80V432c0 17.4 9.4 33.4 
                                                24.5 41.9s33.7 8.1 48.5-.9L361 297c14.3-8.7 23-24.2 23-41s-8.7-32.2-23-41L73 39z"
                                                />
                                            </svg>
                                        </Button>
                                        <CardReact.Text style={{ fontSize: '30px' }}>{card.engWord}</CardReact.Text>
                                    </div>
                                    <CardReact.Text style={{ fontSize: '30px' }}>{card.exampleOfUsage}</CardReact.Text>
                                </Col>
                            </Row>
                        </CardReact.Body>
                    </CardReact>
                )}
            </div>
        </Container >
    )
}


/*
if (isFlipped[index]) {
            if (event.key === 'ArrowLeft') {
                try {
                    await cardService.incorrectAnswer(id);
                } catch (error) {
                    console.log("No answer given", error)
                }
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
                try {
                    await cardService.correctAnswer(id);
                } catch (error) {
                    console.log("No answer given", error)
                }
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
                        newZIndex[index] = -1;  // Установить zIndex в 0 после задержки
                        return newZIndex;
                    });
                }, 250); // 250 миллисекунд задержки

                setTimeout(() => {
                    setCards(prevCards => {
                        const newCards = prevCards.filter(currentCard => currentCard.id !== id); // Удаляем карточку из массива
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
*/