import { Button, Col, Container, Image, Row } from "react-bootstrap";
import './MainButtonGroup.css'
import { ButtonGroup } from "./MainButtonGroup";


export function MainContext() {

    return (
        <Container className="d-flex flex-column align-items-center" style={{height: '600px'}}>
            <Row className="mt-4">
                <ButtonGroup />
            </Row>
            <Row>
                <Col>
                    <Image src="/imgs/image.png" style={{ height: "400px", objectFit: "cover" }} />
                </Col>
            </Row>
            <Row>
                <Col>
                    <Button href='/learn' className='but char' style={{ height: "2.8rem", width: "430px", boxShadow: '0 4px 6px rgba(0, 0, 0, 0.4)', borderRadius: "30px", fontWeight: 'bold', fontSize: '18px', alignItems: "center", display: 'flex', justifyContent: 'center' }}>Start</Button>
                </Col>
                <Col>
                    <Button href='/card' className="rounded-circle d-flex justify-content-center but" style={{ height: "3rem", width: "3rem", boxShadow: "0 4px 6px rgba(0, 0, 0, 0.4)" }}>
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512">
                            <path fill="white" d="M256 80c0-17.7-14.3-32-32-32s-32 14.3-32 32V224H48c-17.7 0-32 
                                14.3-32 32s14.3 32 32 32H192V432c0 17.7 14.3 32 32 32s32-14.3 32-32V288H400c17.7 
                                0 32-14.3 32-32s-14.3-32-32-32H256V80z"
                            />
                        </svg>
                    </Button>
                </Col>
            </Row>
        </Container>
    )
}