import { useState, useEffect } from "react";
import { Button, Image } from "react-bootstrap"
import './DraggedObject.css'

type Direction = "left" | "right" | null;

export function DraggedObject() {

    const initialCards = [
        { id: 1, engWord: 'box', ruWord: 'коробка', imgUrl: 'https://www.celladorales.com/wp-content/uploads/2016/12/ShippingBox_sq.jpg' },
        { id: 2, engWord: 'laptop', ruWord: 'ноутбук', imgUrl: 'https://cdn.thewirecutter.com/wp-content/media/2023/06/bestlaptops-2048px-9765.jpg?auto=webp&quality=75&width=1024' },
        { id: 3, engWord: 'phone', ruWord: 'телефон', imgUrl: 'https://m.media-amazon.com/images/I/61gDg-vFhzL.jpg' },
    ];

    const [cards, setCards] = useState(initialCards.map(card => ({ ...card, counter: 0 })))
    const [topCardIndex, setTopCardIndex] = useState(cards.length - 1);
    const [isDragging, setIsDragging] = useState(false);
    const [position, setPosition] = useState({ x: 0, y: 0 });
    const [startPosition, setStartPosition] = useState({ x: 0, y: 0 });
    const [offset, setOffset] = useState({ x: 0, y: 0 });
    const [turnOver, setTurnOver] = useState<boolean[]>(initialCards.map(() => false));
    const [direction, setDirection] = useState<Direction>(null);
    const [left, setLeft] = useState(false)
    const [right, setRight] = useState(false)
    const [isAnimating, setIsAnimating] = useState(false);
    const [click, setClick] = useState(false)


    useEffect(() => {
        const calculateStartPosition = () => {
            const windowWidth = window.innerWidth;
            const windowHeight = window.innerHeight;
            const initialX = (windowWidth - 500) / 2;
            const initialY = (windowHeight - 550) / 2;
            setStartPosition({ x: initialX, y: initialY });
            setPosition({ x: initialX, y: initialY });
        };

        calculateStartPosition();
        window.addEventListener('resize', calculateStartPosition);
        return () => {
            window.removeEventListener('resize', calculateStartPosition);
        };
    }, []);

    const handleKeyDown = (event: any, index: number) => {
        if (turnOver) {
            if (event.key === 'ArrowLeft' && index === topCardIndex) {
                setLeft(true)
                setTimeout(() => {
                    setLeft(false)
                    setPosition(startPosition)
                }, 500);
                setCards(prevCards => {
                    const newCards = [...prevCards]
                    const lastCard = newCards.pop()
                    newCards.unshift(lastCard!)
                    setTopCardIndex(prevCards.length - 1)
                    return newCards
                })

            } else if (event.key === 'ArrowRight' && index === topCardIndex) {
                setRight(true)
                setTimeout(() => {
                    setRight(false)
                    setPosition(startPosition)
                }, 500);

                setCards(prevCards => {
                    const newCards = [...prevCards]
                    const lastCard = newCards.pop()
                    if (lastCard) {
                        lastCard.counter += 0.5;
                    }
                    if (lastCard && lastCard?.counter !== 3) {
                        newCards.unshift(lastCard!)
                        setTopCardIndex(prevCards.length - 1)
                    }
                    else {
                        setTopCardIndex(prevCards.length - 2)
                    }
                    return newCards
                })
            }
            setTurnOver((prevTurnOver) => prevTurnOver.map((value, idx) => (idx === index ? false : value)));
        }
    };

    const handleMouseDown = (event: any, index: number) => {
        if (index === topCardIndex && turnOver[index]) {
            setIsDragging(true);
            setIsAnimating(true)
            const rect = event.target.getBoundingClientRect();
            setOffset({
                x: event.clientX - rect.left,
                y: event.clientY - rect.top
            });
        }
    };

    const handleMouseMove = (event: any, index: number) => {
        if (isDragging && index === topCardIndex) {
            const newDirection = event.clientX - offset.x > position.x ? "right" : "left"
            setDirection(newDirection);
            setPosition({
                x: event.clientX - offset.x,
                y: event.clientY - offset.y
            });
        }
    };

    const handleMouseUp = (index: number) => {
        setIsDragging(false);
        setPosition(startPosition);
        setDirection(null);
        setOffset({ x: 0, y: 0 });

        setTimeout(() => {
            setIsAnimating(false);
        }, 500);
    };

    const handleTurnOver = (index: number) => {
        if (index === topCardIndex && turnOver) {
            setTurnOver((prevTurnOver) => prevTurnOver.map((value, idx) => (idx === index ? true : value)));
            setClick(true)
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

    return (
        <>
            {cards.map((item, index) => {
                const isTopCard = index === topCardIndex

                return (
                    <div key={item.id}>
                        <div
                            className={`dragged-object t3d ${left && isTopCard ? 'move-left' : ''} ${right && isTopCard ? 'move-right' : ''} ${isAnimating && isTopCard ? 'return-animation' : ''}`}
                            style={{
                                left: position.x,
                                top: position.y
                            }}
                            onMouseDown={(event) => handleMouseDown(event, index)}
                            onMouseMove={(event) => handleMouseMove(event, index)}
                            onMouseUp={() => handleMouseUp(index)}
                            onKeyDown={(event) => handleKeyDown(event, index)}
                            onClick={() => handleTurnOver(index)}
                            tabIndex={0}
                        >
                            <div className="rotate" style={{ transform: turnOver[index] ? 'rotateY(180deg)' : 'none' }}>
                                <div className="front" >
                                    {click &&
                                        <div style={{ position: "absolute", zIndex: 100, display: 'flex', top: 20 }}>
                                            <div style={{ color: '#E98585', margin: '0 100px 0 0', display: 'flex', flexDirection: 'column', justifyContent: 'center', alignItems: 'center' }}>
                                                <svg xmlns="http://www.w3.org/2000/svg" height="20" width="17.5" fill="#E98585" viewBox="0 0 448 512">
                                                    <path d="M9.4 233.4c-12.5 12.5-12.5 32.8 0 45.3l160 160c12.5 12.5 32.8 12.5 
                                                        45.3 0s12.5-32.8 0-45.3L109.2 288 416 288c17.7 0 32-14.3 32-32s-14.3-32-32-32l-306.7 
                                                        0L214.6 118.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0l-160 160z"
                                                    />
                                                </svg>
                                                <span>If you didn't know</span><span style={{ fontWeight: 'bold' }}>swipe left</span>
                                            </div>
                                            <div style={{ color: '#4CB74C', margin: '0 0 0 100px', display: 'flex', flexDirection: 'column', justifyContent: 'center', alignItems: 'center' }}>
                                                <svg xmlns="http://www.w3.org/2000/svg" height="20" width="17.5" fill="#4CB74C" viewBox="0 0 448 512">
                                                    <path d="M438.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-160-160c-12.5-12.5-32.8-12.5-45.3
                                                        0s-12.5 32.8 0 45.3L338.8 224 32 224c-17.7 0-32 14.3-32 32s14.3 32 32 32l306.7 
                                                        0L233.4 393.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0l160-160z"
                                                    />
                                                </svg>
                                                <span>If you were right</span><span style={{ fontWeight: 'bold' }}>swipe right</span>
                                            </div>

                                        </div>
                                    }
                                    <Image style={{ height: '250px', width: '200px', objectFit: 'cover', pointerEvents: 'none' }} src={item.imgUrl} />

                                    <span >{item.ruWord}</span>
                                    {direction === 'right' && <h5 style={{ position: "absolute", top: '25px', right: '20px', border: '4px solid green', borderRadius: '15px', color: 'green', padding: '5px 7px', rotate: '30deg' }}>Got it</h5>}
                                    {direction === 'left' && <h5 style={{ position: "absolute", top: '40px', left: '15px', border: '4px solid #B22222', borderRadius: '15px', color: '#B22222', padding: '5px 7px', rotate: '-30deg' }}>Study again</h5>}
                                </div>
                                <div className="back">
                                    <Image style={{ height: '250px', width: '200px', objectFit: 'cover' }} src={item.imgUrl} />
                                    <div style={{ display: "flex", alignItems: 'center', justifyContent: "center" }}>
                                        <Button variant="outline-light" className="rounded-circle" onClick={(e) => { e.stopPropagation(); handleTriangleClick(item.engWord); }}>
                                            <svg xmlns="http://www.w3.org/2000/svg" height='19' width='15' fill="#BBDDFF" viewBox="0 0 384 512">
                                                <path d="M73 39c-14.8-9.1-33.4-9.4-48.5-.9S0 62.6 0 80V432c0 17.4 9.4 33.4 
                                                24.5 41.9s33.7 8.1 48.5-.9L361 297c14.3-8.7 23-24.2 23-41s-8.7-32.2-23-41L73 39z"
                                                />
                                            </svg>
                                        </Button>
                                        <span>{item.engWord}</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div >
                )
            })}
        </>
    )
}