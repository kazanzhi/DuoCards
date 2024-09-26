import { Button, Col, Row, Modal, Container } from "react-bootstrap";
import buttonsData from "../../../data/buttonsData.json"
import { useContext, useState } from "react";
import { CardsContext } from "../../../App";

type Props = {

}

export function HomeButtonGroup(props: Props) {
    const cardsContext = useContext(CardsContext)

    const { learnCards, knownCards, learnedCards } = cardsContext || { learnCards: [], knownCards: [], learnedCards: [] }

    const [showModal, setShowModal] = useState<boolean[]>(buttonsData.map(() => false))

    const handleShow = (index: number) => {
        const newShowModal = [...showModal];
        newShowModal[index] = true;
        setShowModal(newShowModal);
    }

    const handleClose = (index: number) => {
        const newShowModal = [...showModal];
        newShowModal[index] = false;
        setShowModal(newShowModal);
    }

    const getCardsCount = (id: number) => {
        switch(id){
            case 0: 
                return learnCards.length;
            case 1:
                return knownCards.length;
            case 2: 
                return learnedCards.length
        }
    }

    return (
        <Container style={{ marginTop: '20px' }}>
            <Row style={{ display: 'flex', justifyContent: 'center' }}>
                {buttonsData.map((button) => {
                    return (
                        <Col sm={4} md={4} lg={3} xl={2} key={button.id}>
                            <Button
                                className="no-focus-outline"
                                variant="light"
                                style={{
                                    height: '100%',
                                    width: '100%',
                                    color: button.color,
                                    border: '1px solid #eaeaea',
                                    display: 'flex',
                                    justifyContent: 'center',
                                    alignItems: 'center',
                                    flexDirection: 'column',
                                    boxShadow: '0 0px 8px rgba(0, 0, 0, 0.1)'
                                }}
                                onClick={() => handleShow(button.id)}  >
                                <h3>{getCardsCount(button.id)}</h3>
                                <div>
                                    <span style={{ fontSize: '26px' }}>{button.meaning}</span>
                                    <svg style={{ border: "1px solid", height: '25px', width: '25px', borderRadius: '50%', margin: '0px 0px 5px 5px' }} xmlns="http://www.w3.org/2000/svg" height="0.8rem" width="0.8rem" viewBox="0 0 320 512">
                                        <path fill={button.color} d="M80 160c0-35.3 28.7-64 64-64h32c35.3 0 64 28.7 64 64v3.6c0 21.8-11.1 42.1-29.4 
                                    53.8l-42.2 27.1c-25.2 16.2-40.4 44.1-40.4 74V320c0 17.7 14.3 32 32 32s32-14.3 32-32v-1.4c0-8.2
                                    4.2-15.8 11-20.2l42.2-27.1c36.6-23.6 58.8-64.1 58.8-107.7V160c0-70.7-57.3-128-128-128H144C73.3
                                    32 16 89.3 16 160c0 17.7 14.3 32 32 32s32-14.3 32-32zm80 320a40 40 0 1 0 0-80 40 40 0 1 0 0 80z"
                                        />
                                    </svg>
                                </div>
                            </Button>

                            <Modal show={showModal[button.id]} onHide={() => handleClose(button.id)} centered aria-labelledby="contained-modal-title-vcenter">
                                <Modal.Body style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', marginTop: '20px', marginRight: '10px', marginLeft: '10px' }}>
                                    <h2 className="mb-3" style={{ fontSize: '34px' }}>{button.title}</h2>
                                    <p className="mb-5" style={{ fontSize: '24px' }}>{button.content}</p>
                                    <Button variant="outline-light" className="rounded-circle position-absolute" onClick={() => handleClose(button.id)} style={{ right: "-10px", top: "-20px" }}>
                                        <svg height={31} width={20} xmlns="http://www.w3.org/2000/svg" viewBox="0 0 384 512">
                                            <path fill="#B197FC" d="M342.6 150.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 
                                0L192 210.7 86.6 105.4c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 
                                45.3L146.7 256 41.4 361.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 
                                45.3 0L192 301.3 297.4 406.6c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 
                                0-45.3L237.3 256 342.6 150.6z"
                                            />
                                        </svg>
                                    </Button>
                                </Modal.Body>
                            </Modal>

                        </Col>
                    )
                })}
            </Row>
        </Container >
    )
}