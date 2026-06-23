Practice_Project_Neo_players_App_AD
 
reactapp/src/App.js:
 
import React from 'react'
import Welcome from './components/Welcome'
 
function App() {
  return (
    <div>
      <h1>Neo Players</h1>
      <h2>Top Students List</h2>
      <Welcome name="Sara"/>
    </div>
  )
}
 
export default App
 
 
reactapp/src/components/Welcome.jsx :
 
 
import React from 'react'
 
const Welcome = (props) => {
  return (
    <div>
        <h2>{props.name}</h2>
    </div>
  )
}
 
export default Welcome
 
Practice_Project_Neo_Quote_App_Structure_AD
 
reactapp/src/App.js:
 
import React, { Component } from 'react'
import Display from './components/display'
import Footer from './components/footer'
import Header from './components/header'
 
export class App extends Component {
  render() {
    return (
      <div>
      <Header/>
      <Display/>
      <Footer/>
      </div>
    )
  }
}
 
export default App
 
reactapp/src/components/display.jsx :
 
 
import React, { Component } from 'react'
 
export class display extends Component {
  render() {
    return (
      <div>
      <p>Quote Loading ...</p>
      <img src="https://icons8.com/preloaders/preloaders/1494/Spinner-2.gif" alt="Placeholder"></img>
      </div>
     
    )
  }
}
 
export default display
 
reactapp/src/components/footer.jsx :
 
 
import React from 'react'
 
function footer() {
    const year = new Date().getFullYear();
  return (
    <div>
    <p>Copyright {year} Quote App. All rights reserved.</p>
    </div>
  )
}
 
export default footer
 
reactapp/src/components/header.jsx :
 
 
import React from 'react'
 
function header() {
  return (
    <div>
    <h1>Neo Daily Quote App</h1>
    <nav>
    <ul>All Quotes</ul>
    <ul>Add Quote</ul>
    <ul>Favorite Quotes</ul>
    </nav>
    </div>
  )
}
 
export default header
 
