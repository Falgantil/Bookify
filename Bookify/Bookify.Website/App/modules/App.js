import React from 'react'
import { Link } from 'react-router'

const TitleHeader = ({ title }) => (
   <h1>{title}</h1>
)

class App extends React.Component {
  render() {
    return (
      <div>
        <TitleHeader title="React Router Woot!" />
        <TitleHeader title="React Router Woot 1!" />
        <TitleHeader title="React Router Woot 2!" />
        <ul role="nav">
          <li><Link to="/about">About</Link></li>
          <li><Link to="/repos">Repos</Link></li>
        </ul>

        {/* add this */}
        {this.props.children}

      </div>
    )
  }
}

export default App;
