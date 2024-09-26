import { useState, useEffect } from 'react'
import { Col, Container, Image, Row, Form, Button, FloatingLabel } from 'react-bootstrap'
import { cardService } from '../../services/cardService'
import { CardDto } from '../../Dtos/CardDto'
import { useNavigate, useParams } from 'react-router-dom'

interface Props {

}

export const CreateCard = (props: Props) => {

    const redirect = useNavigate()
    const [ruWord, setRuWord] = useState<string>('')
    const [engWord, setEngWord] = useState<string>('')
    const [exampleOfUsage, setExampleOfUsage] = useState<string>('')
    const [typingTimeout, setTypingTimeout] = useState<NodeJS.Timeout | null>(null);
    const [isEditMode, setIsEditMode] = useState<boolean>(false)

    const { id = '' } = useParams<{ id: string }>()
    const cardId = parseInt(id, 10)

    useEffect(() => {
        if (id) {
            setIsEditMode(true)
            const fetchCard = async () => {
                try {
                    const card = await cardService.getById(cardId)
                    if (card) {
                        setRuWord(card.ruWord)
                        setEngWord(card.engWord)
                        setExampleOfUsage(card.exampleOfUsage)
                    }
                } catch (error) {
                    console.log('Error fetching card:', error)
                }
            }

            fetchCard()
        }

    }, [id])

    const handleSave = async () => {
        const newCard: CardDto = {
            ruWord: ruWord,
            engWord: engWord,
            exampleOfUsage: exampleOfUsage
        }
        if (isEditMode) {
            try{
                await cardService.updateCard(cardId, newCard)
            }catch(error){
                console.log("Card not been updated:", error)
            }
        } else {
            try {
                await cardService.createCard(newCard)
            } catch (error) {
                console.log("Card colud not created.")
            }
        }
        redirect("/")
    }


    const fetchTranslation = async (word: string) => {
        try {
            const translatedWord = await cardService.translate(word)
            setRuWord(translatedWord)
        } catch (error) {
            console.log("Translation failed:", error)
        }
    }

    const handleInput = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>, type: string) => {
        const value = event.target.value
        if (type === 'English') {
            setEngWord(value);
            if (typingTimeout) {
                clearTimeout(typingTimeout);
            }

            // Устанавливаем новый таймаут
            const timeout = setTimeout(() => {
                fetchTranslation(value); // Запрос перевода после 1  секунд бездействия
            }, 1000);

            setTypingTimeout(timeout); // Сохраняем таймаут в состоянии
        } else if (type === 'Russion') {
            setRuWord(value);
        } else if (type === 'Example of usage (English)') {
            setExampleOfUsage(value);
        }
    }

    return (
        <Container fluid style={{ height: '100vh' }}>
            <Row style={{ display: 'flex', justifyContent: 'center' }}>
                <Col sm={12} md={8} lg={6} xl={6} style={{ position: 'relative', textAlign: 'center' }}>
                    <Button href='/' variant='outline' className='rounded-circle' style={{ position: 'absolute', left: '0', border: 'none' }}>
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 320 512" style={{ filter: 'drop-shadow(2px 2px 4px rgba(0, 0, 0, 0.9))', height: '35px' }}  >
                            <path fill='white' d="M41.4 233.4c-12.5 12.5-12.5 32.8 0 45.3l160 160c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L109.3 256 246.6 118.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0l-160 160z" />
                        </svg>
                    </Button>
                    <Image style={{ height: '300px', objectFit: 'cover', marginTop: '20px' }} src='https://i.natgeofe.com/k/6496b566-0510-4e92-84e8-7a0cf04aa505/red-fox-portrait.jpg?w=1084.125&h=721.875' />
                </Col>
            </Row>
            <Row style={{ display: 'flex', justifyContent: 'center', height: '100px' }}>
                <Col sm={12} md={8} lg={6} xl={6} >
                    <FloatingLabel style={{ color: '#A9A9A9', margin: '20px 0px', borderBottom: '2px solid #00BFFF', fontSize: '20px' }} label={'English'}>
                        <Form.Control value={engWord} onChange={(event) => handleInput(event, 'English')} type='text' placeholder={'English'} style={{ borderWidth: '0px', fontSize: '30px', height: '80%' }} />
                    </FloatingLabel>
                </Col>
            </Row>
            <Row style={{ display: 'flex', justifyContent: 'center', height: '100px' }}>
                <Col sm={12} md={8} lg={6} xl={6} >
                    <FloatingLabel style={{ color: '#A9A9A9', margin: '20px 0px', borderBottom: '2px solid #00BFFF', fontSize: '20px' }} label={'Russion'}>
                        <Form.Control value={ruWord} onChange={(event) => handleInput(event, 'Russion')} type='text' placeholder={'Russion'} style={{ borderWidth: '0px', fontSize: '30px', height: '80%' }} />
                    </FloatingLabel>
                </Col>
            </Row>
            <Row style={{ display: 'flex', justifyContent: 'center', height: '100px' }}>
                <Col sm={12} md={8} lg={6} xl={6} >
                    <FloatingLabel style={{ color: '#A9A9A9', margin: '20px 0px', borderBottom: '2px solid #00BFFF', fontSize: '20px' }} label={'Example of usage (English)'}>
                        <Form.Control value={exampleOfUsage} onChange={(event) => handleInput(event, 'Example of usage (English)')} type='text' placeholder={'Example of usage (English)'} style={{ borderWidth: '0px', fontSize: '30px', height: '80%' }} />
                    </FloatingLabel>
                </Col>
            </Row>


            <Row style={{ display: 'flex', justifyContent: 'center' }}>
                <Col sm={12} md={8} lg={6} xl={6}>
                    <Button onClick={handleSave} style={{ width: '100%', borderRadius: '30px', marginTop: '40px', height: '55px', fontSize: '30px', display: 'flex', justifyContent: 'center', alignItems: 'center' }}>Save</Button>
                </Col>
            </Row>
        </Container >
    )
}