import { HomeHeader } from './HomeHeader'
import { HomeBody } from './HomeBody/HomeBody'
import { HomeFooter } from './HomeFooter'


export function Home() {

  return (
    <div style={{ display: 'flex', flexDirection: 'column', minHeight: '100vh' }}>
      <HomeHeader />
      <HomeBody />
      <HomeFooter />
    </div>
  )
}