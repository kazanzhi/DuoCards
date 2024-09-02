import { Navbar } from '../components/Navbar/Navbar'
import { MainContext } from '../components/Main/MainContext'
import Footer from '../components/Footer/Footer'


export function Home() {

  return (
    <div >
      <Navbar />
      <MainContext />
      <Footer />
    </div>
  )
}