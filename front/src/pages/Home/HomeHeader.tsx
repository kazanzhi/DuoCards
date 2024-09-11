import { useState } from 'react'
import { Container, Button, Image, Row, Col, Dropdown } from 'react-bootstrap'
import { ThemeRolingMenu } from './HomeThemeRolingMenu'
import ukImage from '../../assets/images/uk.png'
import './HomeHeader.css'


export const HomeHeader = () => {

    const [showTheme, setShowTheme] = useState<boolean>(false)

    const handleTheme = () => setShowTheme(!showTheme)

    const handleLogin = () => {

    }

    const handleRegister = () => {

    }

    return (
        <header className='shadow-sm' style={{ borderBottom: '1px solid #E2EDF5', height: '62px', display: 'flex' }}>
            <Container fluid style={{ display: 'flex', justifyContent: 'center' }}>
                <Row className='custom-row'>
                    <Col sm={4} md={4} style={{ display: 'flex', justifyContent: 'start', alignItems: 'center' }}>
                        <Button onClick={handleTheme} variant='outline-light' className="rounded-circle" style={{ width: "3rem", height: "3rem", display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                            <svg xmlns="http://www.w3.org/2000/svg" height="30" width="30" viewBox="0 0 512 512">
                                <path fill="#B197FC" d="M512 256c0 .9 0 1.8 0 2.7c-.4 36.5-33.6 61.3-70.1 
                                61.3H344c-26.5 0-48 21.5-48 48c0 3.4 .4 6.7 1 9.9c2.1 10.2 6.5 20 
                                10.8 29.9c6.1 13.8 12.1 27.5 12.1 42c0 31.8-21.6 60.7-53.4 62c-3.5
                                .1-7 .2-10.6 .2C114.6 512 0 397.4 0 256S114.6 0 256 0S512 114.6 512 
                                256zM128 288a32 32 0 1 0 -64 0 32 32 0 1 0 64 0zm0-96a32 32 0 1 0 
                                0-64 32 32 0 1 0 0 64zM288 96a32 32 0 1 0 -64 0 32 32 0 1 0 64 0zm96 
                                96a32 32 0 1 0 0-64 32 32 0 1 0 0 64z"
                                />
                            </svg>
                        </Button>
                    </Col>
                    <Col sm={4} md={4} style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                        <Image src={ukImage} style={{ width: '45px', height: '25px' }} />
                    </Col>
                    <Col sm={4} md={4} style={{ display: 'flex', justifyContent: 'end', alignItems: 'center' }}>
                        {/* Statistic 
                        <Button variant='outline-light' className='rounded-circle' style={{ width: "3rem", height: "3rem" }}>
                            <svg xmlns="http://www.w3.org/2000/svg" height="24" width="21" viewBox="0 0 448 512">
                                <path fill="#B197FC" d="M160 80c0-26.5 21.5-48 48-48h32c26.5 0 48 21.5 
                                48 48V432c0 26.5-21.5 48-48 48H208c-26.5 0-48-21.5-48-48V80zM0 272c0-26.5 
                                21.5-48 48-48H80c26.5 0 48 21.5 48 48V432c0 26.5-21.5 48-48 48H48c-26.5 
                                0-48-21.5-48-48V272zM368 96h32c26.5 0 48 21.5 48 48V432c0 26.5-21.5 48-48 
                                48H368c-26.5 0-48-21.5-48-48V144c0-26.5 21.5-48 48-48z"
                                />
                            </svg>
                        </Button> */}
                        <Button href='login' variant="outline-light" className='rounded-circle' style={{ height: '3rem', width: '3rem', display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" height='30' width='30'>
                                <path fill='#B197FC' d="M352 96l64 0c17.7 0 32 14.3 32 32l0 256c0 17.7-14.3 32-32 32l-64 0c-17.7 0-32 14.3-32 32s14.3 32 32 32l64 0c53 0 96-43 96-96l0-256c0-53-43-96-96-96l-64 0c-17.7 0-32 14.3-32 32s14.3 32 32 32zm-9.4 182.6c12.5-12.5 12.5-32.8 0-45.3l-128-128c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L242.7 224 32 224c-17.7 0-32 14.3-32 32s14.3 32 32 32l210.7 0-73.4 73.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0l128-128z" />
                            </svg>
                        </Button>
                    </Col>
                </Row>
            </Container>

            {/* Затемнение и размытие страницы */}
            {showTheme && <div className="backdrop" style={{
                position: "fixed",
                top: "0",
                left: "0",
                width: "100%",
                height: "100%",
                backgroundColor: "rgba(0, 0, 0, 0.5)",
                zIndex: 2,
                backdropFilter: 'blur(5px)', // Эффект размытия
                pointerEvents: 'none',
            }} />}

            <ThemeRolingMenu showTheme={showTheme} setShowTheme={setShowTheme} />
        </header>
    )
}