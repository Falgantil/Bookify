import React from 'react'
import {observer} from "mobx-react";
import {observable} from "mobx";
import { Link, browserHistory } from 'react-router';
import bookifyapi from '../util/bookifyapi';
import RegistrationViewModel from './registration-view-model';

@observer
class Registrationpage extends React.Component {
  constructor() {
    super(...arguments);
    this.model = new RegistrationViewModel();
  }

  async submit(e) {
    e.preventDefault();
    var success = await this.model.submit();
    // var token = bookifyapi.getAuthToken();
    // console.log(token);
    if (!success) {
      this.setState({ showError: true });
    }
    else {
      browserHistory.push('/');
    }
  }

  render() {
    return (
      <div className="row">
        <div className="col-xs-12 col-sm-6 col-sm-offset-3">
          <form onSubmit={(e) => this.submit(e)}>
            <legend className="form-groups">Personlig oplysninger</legend>
            <div className="form-group">
              <input
                type="text"
                className="form-control"
                placeholder="Fornavn"
                value={this.model.firstName}
                onChange={(e) => this.model.firstName = e.target.value } />
            </div>
            <div className="form-group">
              <input
                type="text"
                className="form-control"
                placeholder="Efternavn"
                value={this.model.lastName}
                onChange={(e) => this.model.lastName = e.target.value } />
            </div>
            <div className="form-group">
              <input
                type="text"
                className="form-control"
                placeholder="Brugernavn"
                value={this.model.username}
                onChange={(e) => this.model.username = e.target.value } />
            </div>
            <br/>
            <legend className="form-groups">Konto oplysninger</legend>
            <div className="form-group">
              <input
                type="text"
                className="form-control"
                placeholder="Email"
                value={this.model.email}
                onChange={(e) => this.model.email = e.target.value } />
            </div>
            <div className="form-group">
              <input
                type="password"
                className="form-control"
                placeholder="Password"
                value={this.model.password}
                onChange={(e) => this.model.password = e.target.value } />
            </div>
            <br/>
            <button type="submit" className="btn btn-block btn-raised btn-primary"><i className="material-icons">check</i></button>
          </form>
        </div>
      </div>
    )
  }
}

export default Registrationpage