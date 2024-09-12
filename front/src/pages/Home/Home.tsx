import { HomeHeader } from './HomeHeader'
import { HomeBody } from './HomeBody/HomeBody'
import { HomeFooter } from './HomeFooter'
import { Container } from 'react-bootstrap'


export function Home() {

  return (
    <div style={{ display: 'flex', flexDirection: 'column', minHeight: '100vh' }}>
      <HomeHeader />
      <Container fluid style={{ backgroundColor: '#F3F9FF', flex: 1 }}>
        <HomeBody />
      </Container>
      {/* <HomeFooter /> */}
    </div>
  )
}