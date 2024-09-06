import { useState } from 'react'
import { Button, Container, Card, Row, Col } from 'react-bootstrap'

type Props = {}

export const LearnCards = (props: Props) => {
    const initialCards = [
        { id: 1, engWord: 'box', ruWord: 'коробка', imgUrl: 'https://www.celladorales.com/wp-content/uploads/2016/12/ShippingBox_sq.jpg' },
        { id: 2, engWord: 'laptop', ruWord: 'ноутбук', imgUrl: 'https://cdn.thewirecutter.com/wp-content/media/2023/06/bestlaptops-2048px-9765.jpg?auto=webp&quality=75&width=1024' },
        { id: 3, engWord: 'phone', ruWord: 'телефон', imgUrl: 'https://m.media-amazon.com/images/I/61gDg-vFhzL.jpg' },
    ]

    const [isFlipped, setIsFlipped] = useState(false); // Состояние для переворота

    const handleFlip = () => {
        setIsFlipped(true);
    };

    return (
        <Container fluid style={{ flex: 1, display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
            <div
                onClick={handleFlip}
                style={{
                    perspective: '1000px',
                    width: '650px',
                    height: '650px'
                }}
            >
                <Card
                    style={{
                        width: '100%',
                        height: '100%',
                        position: 'relative',
                        transformStyle: 'preserve-3d',
                        transition: 'transform 0.8s',
                        transform: isFlipped ? 'rotateY(180deg)' : 'rotateY(0deg)',
                        boxShadow: '0 0px 8px rgba(0, 0, 0, 0.1)',
                        borderRadius: '20px'
                    }}>

                    {/* Front Side */}
                    <Card.Body style={{
                        position: 'absolute',
                        width: '100%',
                        height: '100%',
                        backfaceVisibility: 'hidden',
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'center',
                        flexDirection: 'column'
                    }}
                    >
                        <Card.Img style={{ height: '250px', width: '300px', objectFit: 'cover', pointerEvents: 'none' }} src='https://www.celladorales.com/wp-content/uploads/2016/12/ShippingBox_sq.jpg' />
                        <Card.Text style={{ fontSize: '25px' }}>коробка</Card.Text>
                    </Card.Body>


                    {/* Back Side */}
                    <Card.Body
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
                            style={{ height: '250px', width: '300px', objectFit: 'cover' }}
                            src='https://www.celladorales.com/wp-content/uploads/2016/12/ShippingBox_sq.jpg'
                        />
                        <Row>
                            <Col>
                                <Button variant="outline-light" className="rounded-circle">
                                    <svg xmlns="http://www.w3.org/2000/svg" height='19' width='15' fill="#BBDDFF" viewBox="0 0 384 512">
                                        <path d="M73 39c-14.8-9.1-33.4-9.4-48.5-.9S0 62.6 0 80V432c0 17.4 9.4 33.4 
                                                24.5 41.9s33.7 8.1 48.5-.9L361 297c14.3-8.7 23-24.2 23-41s-8.7-32.2-23-41L73 39z"
                                        />
                                    </svg>
                                </Button>
                            </Col>
                            <Col>
                                <Card.Text style={{ fontSize: '25px' }}>box</Card.Text>
                            </Col>
                        </Row>
                    </Card.Body>
                </Card>
            </div>
        </Container >
    )
}
/*


*/