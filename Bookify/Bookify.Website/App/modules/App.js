import React from 'react';
import { Link } from 'react-router';
import FrontPage from './FrontPage';
import NavigationBar from './Shared/navigationbar.js';
import 'bootstrap-material-design';
import 'bootstrap-material-design/dist/js/material.js';
import 'bootstrap-material-design/dist/js/ripples.js';
import 'bootstrap-material-design/dist/css/bootstrap-material-design.css';
import 'bootstrap-material-design/dist/css/ripples.css';
import './style.styl';

class App extends React.Component {
  render() {
    return (
      <div>
        <NavigationBar />
        <div className="container">
          {this.props.children}
          {!this.props.children && <FrontPage />}
        </div>
      </div>
    )
  }
}

export default App;
