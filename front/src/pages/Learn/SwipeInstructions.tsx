import React from 'react'
import { Row, Col } from 'react-bootstrap'

type Props = {}

export default function SwipeInstructions({ }: Props) {
    return (
        <Row style={{ position: "absolute", zIndex: 100, width: '100%', paddingTop: '30px', fontSize: '25px' }}>
            <Col style={{ color: '#E98585', display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
                <svg xmlns="http://www.w3.org/2000/svg" style={{height: '40px'}} fill="#E98585" viewBox="0 0 448 512">
                    <path d="M9.4 233.4c-12.5 12.5-12.5 32.8 0 45.3l160 160c12.5 12.5 32.8 12.5 
                                                    45.3 0s12.5-32.8 0-45.3L109.2 288 416 288c17.7 0 32-14.3 32-32s-14.3-32-32-32l-306.7 
                                                    0L214.6 118.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0l-160 160z"
                    />
                </svg>
                <span>If you didn't know</span><span style={{ fontWeight: 'bold' }}>swipe left</span>
            </Col>
            <Col style={{ color: '#4CB74C', display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
                <svg xmlns="http://www.w3.org/2000/svg" style={{height: '40px'}} fill="#4CB74C" viewBox="0 0 448 512">
                    <path d="M438.6 278.6c12.5-12.5 12.5-32.8 0-45.3l-160-160c-12.5-12.5-32.8-12.5-45.3
                                                    0s-12.5 32.8 0 45.3L338.8 224 32 224c-17.7 0-32 14.3-32 32s14.3 32 32 32l306.7 
                                                    0L233.4 393.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0l160-160z"
                    />
                </svg>
                <span>If you were right</span><span style={{ fontWeight: 'bold' }}>swipe right</span>
            </Col>
        </Row>
    )
}