import React from 'react';
import {observer} from "mobx-react";
import { ButtonGroup, Button, DropdownButton, MenuItem } from 'react-bootstrap';
import { Link } from 'react-router';
import SessionStore from '../Shared/SessionStore'
import bookifyapi from '../util/bookifyapi';

/**
 * The NavigationBar.
 */
@observer
class NavigationBar extends React.Component {
  render() {
    return (
      <div className="navbar navbar-fixed-top">
        <div className="container">
          <div className="navbar-header">
            <button type="button" className="navbar-toggle" data-toggle="collapse" data-target=".navbar-responsive-collapse">
              <span className="icon-bar"></span>
              <span className="icon-bar"></span>
              <span className="icon-bar"></span>
            </button>
            <Link className="navbar-brand" to="/">Bookify</Link>
          </div>
          <div className="navbar-collapse collapse navbar-responsive-collapse">
            <ul className="nav navbar-nav">
              <li><Link to="/book/new">Ny bog</Link></li>
            </ul>
            {(SessionStore.loggedIn() && (
              <ul className="nav navbar-nav navbar-right">
                <li><a href="#">Velkommen {SessionStore.currentUser.Person.Alias}</a></li>
                <li><a href="#" onClick={() => bookifyapi.logout()}>Log ud</a></li>
              </ul>
            )) || (
              <ul className="nav navbar-nav navbar-right">
                <li><Link to="/registration">Registrér</Link></li>
                <li><Link to="/loginpage">Log ind</Link></li>
              </ul>
            )}
          </div>
        </div>
      </div>
    )
  }
}

export default NavigationBar;