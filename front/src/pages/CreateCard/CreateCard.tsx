import { Col, Container, Image, Row, Form, Button, FloatingLabel } from 'react-bootstrap'

interface Props {

}

export const CreateCard = (props: Props) => {


    return (
        <Container style={{ display: 'flex', justifyContent: 'center', marginTop: '10px', width: '55%', minWidth: '620px' }}>
            <Row style={{ width: '100%' }}>
                <Col style={{ position: 'relative', textAlign: 'center' }}>
                    <Button href='/' variant='outline' className='rounded-circle' style={{ position: 'absolute', left: '0', width: '2.55rem', height: '2.55rem', border: 'none' }}>
                        <svg height='20' width='12.5' xmlns="http://www.w3.org/2000/svg" viewBox="0 0 320 512" style={{ filter: 'drop-shadow(2px 2px 4px rgba(0, 0, 0, 0.9))' }}  >
                            <path fill='white' d="M41.4 233.4c-12.5 12.5-12.5 32.8 0 45.3l160 160c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L109.3 256 246.6 118.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0l-160 160z" />
                        </svg>
                    </Button>
                    <Image style={{ height: '200px', objectFit: 'cover', marginTop: '20px' }} src='https://i.natgeofe.com/k/6496b566-0510-4e92-84e8-7a0cf04aa505/red-fox-portrait.jpg?w=1084.125&h=721.875' />

                    {['English', 'Russion', 'Example of usage (English)'].map((item) => {
                        return (
                            <FloatingLabel style={{ fontWeight: '500', color: '#A9A9A9', margin: '15px 0px', borderBottom: '2px solid #00BFFF' }} label={item}>
                                <Form.Control type='text' placeholder={item} style={{ borderWidth: '0px' }} />
                            </FloatingLabel>
                        )
                    })}
                    <Button style={{width: '100%', borderRadius: '30px', marginTop: '10px'}}>Save</Button>
                </Col>
            </Row>
        </Container>
    )
}