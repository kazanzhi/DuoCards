import { useContext } from 'react'
import { Button, Col, Container, Row } from 'react-bootstrap'
import { ThemeContext } from '../../App'

interface Props {
    showTheme: boolean
}
type ThemeContextType = any

export const ThemeRolMenu = ({ showTheme }: Props) => {

    const { setTheme } = useContext<ThemeContextType | null>(ThemeContext)

    const toggleTheme = (event: string) => {
        switch (event) {
            case 'pink':
                setTheme('pink')
                break;
            case 'brown':
                setTheme('brown')
                break;
            case 'dark':
                setTheme('dark')
                break;
            case 'light':
                setTheme('light')
                break;
            default:
                break;
        }
    }
    return (
        <>
            {showTheme !== null &&
                <Container className='d-flex flex-column align-items-center shadow-sm common' fluid style={{
                    padding: "0", position: 'absolute', width: '100%', height: '120px',
                    transform: showTheme ? 'translateY(0)' : 'translateY(-120px)',
                    transition: '0.3s ease',
                }} >
                    <Row>
                        <Col style={{ margin: '15px 2px' }}>
                            <Button onClick={() => toggleTheme('pink')} className='rounded-circle' variant="outline-light" style={{ height: "3rem", width: "3rem" }}>
                                <svg xmlns="http://www.w3.org/2000/svg" height="32" width="24" viewBox="0 0 384 512" style={{ filter: 'drop-shadow(1px 2px 2px rgba(0, 0, 0, 0.3))' }}>
                                    <path fill="#FF00FF" d="M192 512C86 512 0 426 0 320C0 228.8 130.2 57.7 166.6 11.7C172.6
                            4.2 181.5 0 191.1 0h1.8c9.6 0 18.5 4.2 24.5 11.7C253.8 57.7 384 228.8 384
                            320c0 106-86 192-192 192zM96 336c0-8.8-7.2-16-16-16s-16 7.2-16 16c0 61.9
                            50.1 112 112 112c8.8 0 16-7.2 16-16s-7.2-16-16-16c-44.2 0-80-35.8-80-80z"
                                    />
                                </svg>
                            </Button>
                        </Col>
                        <Col style={{ margin: '15px 2px' }}>
                            <Button onClick={() => toggleTheme('brown')} className='rounded-circle' variant="outline-light" style={{ height: "3rem", width: "3rem" }}>
                                <svg xmlns="http://www.w3.org/2000/svg" height="32" width="24" viewBox="0 0 384 512" style={{ filter: 'drop-shadow(1px 2px 2px rgba(0, 0, 0, 0.3))' }}>
                                    <path fill="#B8860B" d="M192 512C86 512 0 426 0 320C0 228.8 130.2 57.7 166.6 11.7C172.6
                            4.2 181.5 0 191.1 0h1.8c9.6 0 18.5 4.2 24.5 11.7C253.8 57.7 384 228.8 384
                            320c0 106-86 192-192 192zM96 336c0-8.8-7.2-16-16-16s-16 7.2-16 16c0 61.9
                            50.1 112 112 112c8.8 0 16-7.2 16-16s-7.2-16-16-16c-44.2 0-80-35.8-80-80z"
                                    />
                                </svg>
                            </Button>
                        </Col>
                        <Col style={{ margin: '15px 2px' }}>
                            <Button onClick={() => toggleTheme('dark')} className='rounded-circle' variant="outline-light" style={{ height: "3rem", width: "3rem" }}>
                                <svg xmlns="http://www.w3.org/2000/svg" height="32" width="24" viewBox="0 0 384 512" style={{ filter: 'drop-shadow(1px 2px 2px rgba(0, 0, 0, 0.3))' }}>
                                    <path fill="black" d="M192 512C86 512 0 426 0 320C0 228.8 130.2 57.7 166.6 11.7C172.6
                            4.2 181.5 0 191.1 0h1.8c9.6 0 18.5 4.2 24.5 11.7C253.8 57.7 384 228.8 384
                            320c0 106-86 192-192 192zM96 336c0-8.8-7.2-16-16-16s-16 7.2-16 16c0 61.9
                            50.1 112 112 112c8.8 0 16-7.2 16-16s-7.2-16-16-16c-44.2 0-80-35.8-80-80z"
                                    />
                                </svg>
                            </Button>
                        </Col>
                        <Col style={{ margin: '15px 2px' }}>
                            <Button onClick={() => toggleTheme('light')} className='rounded-circle' variant="outline-light" style={{ height: "3rem", width: "3rem" }}>
                                <svg xmlns="http://www.w3.org/2000/svg" height="32" width="24" viewBox="0 0 384 512" style={{ filter: 'drop-shadow(1px 2px 2px rgba(0, 0, 0, 0.3))' }}>
                                    <path fill="#6495ED" d="M192 512C86 512 0 426 0 320C0 228.8 130.2 57.7 166.6 11.7C172.6
                            4.2 181.5 0 191.1 0h1.8c9.6 0 18.5 4.2 24.5 11.7C253.8 57.7 384 228.8 384
                            320c0 106-86 192-192 192zM96 336c0-8.8-7.2-16-16-16s-16 7.2-16 16c0 61.9
                            50.1 112 112 112c8.8 0 16-7.2 16-16s-7.2-16-16-16c-44.2 0-80-35.8-80-80z"
                                    />
                                </svg>
                            </Button>
                        </Col>
                    </Row>
                    <Row >
                        <Col>
                            <p>use the theme you like</p>
                        </Col>
                    </Row>
                </Container>
            }
        </>
    )
}
/*
background: rgba(0, 0, 0, 0.133);
backdrop-filter: blur(2px);
*/
