import { useState } from 'react'
import { Navbar as NavbarBs, Container, Button, Image } from 'react-bootstrap'
import { ThemeRolMenu } from './ThemeRolMenu'
import { StatisticRolMenu } from './StatisticRolMenu'


export const Navbar = () => {

    const [showTheme, setShowTheme] = useState<boolean>(false)
    const [showStat, setShowStat] = useState<boolean>(false)

    const handleTheme = () => setShowTheme(!showTheme)
    const handleStat = () => setShowStat(!showStat)


    return (
        <>
            <NavbarBs className='shadow-sm ' style={{ position: "relative", zIndex: 2, borderBottom: '1px solid #182026'}}>
                <Container className='d-flex justify-content-center' >
                    <Button onClick={handleTheme} variant='outline-light' className="rounded-circle" style={{ width: "3rem", height: "3rem", margin: '0 8rem' }}>
                        <svg xmlns="http://www.w3.org/2000/svg" height="25" width="25" viewBox="0 0 512 512">
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
                    <Image src='/imgs/uk.png' style={{ width: "30px", margin: '0 8rem' }} />
                    <Button onClick={handleStat} variant='outline-light' className='rounded-circle' style={{ width: "3rem", height: "3rem", margin: '0 8rem' }}>
                        <svg xmlns="http://www.w3.org/2000/svg" height="24" width="21" viewBox="0 0 448 512">
                            <path fill="#B197FC" d="M160 80c0-26.5 21.5-48 48-48h32c26.5 0 48 21.5 
                                48 48V432c0 26.5-21.5 48-48 48H208c-26.5 0-48-21.5-48-48V80zM0 272c0-26.5 
                                21.5-48 48-48H80c26.5 0 48 21.5 48 48V432c0 26.5-21.5 48-48 48H48c-26.5 
                                0-48-21.5-48-48V272zM368 96h32c26.5 0 48 21.5 48 48V432c0 26.5-21.5 48-48 
                                48H368c-26.5 0-48-21.5-48-48V144c0-26.5 21.5-48 48-48z"
                            />
                        </svg>
                    </Button>
                </Container>
            </NavbarBs >
            <ThemeRolMenu showTheme={showTheme} />
            <StatisticRolMenu showStat={showStat} />
        </>
    )
}