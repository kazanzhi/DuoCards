import { Button, Col, Container, Image, Row } from "react-bootstrap"
import './HomeButtonGroup'
import { HomeButtonGroup } from "./HomeButtonGroup"
import elephantImage from '../../../assets/images/elephant.png'


export const HomeBody = () => {

    return (
        <main>
            <Container fluid style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', padding: '30px 0px' }} >
                <Row style={{ width: '100%' }}>
                    <HomeButtonGroup />
                </Row>
                <Row>
                    <Col>
                        <Image src={elephantImage} style={{ height: "100%", objectFit: "cover" }} />
                    </Col>
                </Row>
                <Row style={{ width: '100%', height: '80px', display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                    <Col sm={9} md={8} xl={4}>
                        <Button href='/learn' style={{ boxShadow: '0 4px 6px rgba(0, 0, 0, 0.4)', borderRadius: "30px", fontWeight: 'bold', fontSize: '26px', alignItems: "center", display: 'flex', justifyContent: 'center' }}>Start</Button>
                    </Col>
                    <Col sm={3} md={1} xl={1}>
                        <Button href='/create' style={{ height: "53px", width: "53px", boxShadow: "0 4px 6px rgba(0, 0, 0, 0.4)", borderRadius: '50%', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
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
        </main >
    )
}