import { Button, Col, Container, Row } from "react-bootstrap";
import { LearnCards } from "./LearnCards";


export function Learn() {
    return (
        <main style={{ display: 'flex', flexDirection: 'column', minHeight: '100vh', backgroundColor: '#F3F9FF' }}>
            <header style={{ marginTop: '10px' }}>
                <Container fluid >
                    <Row style={{ justifyContent: 'center' }}>
                        <Col sm={6} md={5} lg={4} xl={3} style={{ justifyContent: 'start' }}>
                            <Button href='/' variant='outline' className='rounded-circle' style={{ border: 'none' }}>
                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 320 512" style={{ filter: 'drop-shadow(2px 2px 4px rgba(0, 0, 0, 0.5))', height: '35px' }}  >
                                    <path fill='grey' d="M41.4 233.4c-12.5 12.5-12.5 32.8 0 45.3l160 160c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L109.3 256 246.6 118.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0l-160 160z" />
                                </svg>
                            </Button>
                        </Col>
                        <Col sm={6} md={5} lg={4} xl={3} style={{ display: 'flex', justifyContent: 'end' }}>
                            <Button href='/card' variant='outline' className='rounded-circle' style={{ border: 'none' }}>
                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512" style={{ filter: 'drop-shadow(2px 2px 4px rgba(0, 0, 0, 0.5))', height: '27px' }} >
                                    <path fill='grey' d="M410.3 231l11.3-11.3-33.9-33.9-62.1-62.1L291.7 89.8l-11.3 11.3-22.6 22.6L58.6 322.9c-10.4 10.4-18 23.3-22.2 37.4L1 480.7c-2.5 8.4-.2 17.5 6.1 23.7s15.3 8.5 23.7 6.1l120.3-35.4c14.1-4.2 27-11.8 37.4-22.2L387.7 253.7 410.3 231zM160 399.4l-9.1 22.7c-4 3.1-8.5 5.4-13.3 6.9L59.4 452l23-78.1c1.4-4.9 3.8-9.4 6.9-13.3l22.7-9.1v32c0 8.8 7.2 16 16 16h32zM362.7 18.7L348.3 33.2 325.7 55.8 314.3 67.1l33.9 33.9 62.1 62.1 33.9 33.9 11.3-11.3 22.6-22.6 14.5-14.5c25-25 25-65.5 0-90.5L453.3 18.7c-25-25-65.5-25-90.5 0zm-47.4 168l-144 144c-6.2 6.2-16.4 6.2-22.6 0s-6.2-16.4 0-22.6l144-144c6.2-6.2 16.4-6.2 22.6 0s6.2 16.4 0 22.6z" />
                                </svg>
                            </Button>
                            <Button href='/card' variant='outline' className='rounded-circle' style={{ border: 'none' }}>
                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" style={{ filter: 'drop-shadow(2px 2px 4px rgba(0, 0, 0, 0.5))', height: '33px' }} >
                                    <path fill='grey' d="M256 80c0-17.7-14.3-32-32-32s-32 14.3-32 32V224H48c-17.7 0-32 14.3-32 32s14.3 32 32 32H192V432c0 17.7 14.3 32 32 32s32-14.3 32-32V288H400c17.7 0 32-14.3 32-32s-14.3-32-32-32H256V80z" />
                                </svg>
                            </Button>
                        </Col>
                    </Row>
                </Container>
            </header >
            <LearnCards />
        </main >
    );
}