import React from 'react'
import {observer} from "mobx-react";
import {observable} from "mobx";
import { Link, browserHistory } from 'react-router';
import bookifyapi from '../util/bookifyapi';
import LoginViewModel from './login-view-model';


@observer
class Loginpage extends React.Component {
  constructor() {
    super(...arguments);
    this.model = new LoginViewModel();
    this.state = {
      showError: false,
    };
  }

  async submit(e) {
    e.preventDefault();
    var success = await this.model.submit();
    console.log(bookifyapi.getAuthToken());
    if (!success) {
      this.setState({ showError: true });
    }
    else {
      browserHistory.push('/');
    }
  }

  render(){
    return (
      <div className="row">
        <div className="col-xs-12 col-sm-6 col-sm-offset-3">
        <h4>Log ind</h4>
        <form onSubmit={(e) => this.submit(e)}>
          <div className="form-group">
            <input type="text" className="form-control" id="email" placeholder="Email" value={this.model.email} onChange={(e) => this.model.email = e.target.value } />
          </div>
          <div className="form-group">
            <input type="password" className="form-control" id="password" placeholder="Password" value={this.model.password} onChange={(e) => this.model.password = e.target.value } />
          </div>
          <button type="submit" className="btn btn-block btn-raised btn-primary"><i className="material-icons">check</i></button>
        </form>
        </div>
        {this.state.showError && (
          <div className="col-xs-offset-3 col-xs-6">
            <div className="alert alert-danger" role="alert">FAIL</div>
          </div>
          )}
      </div>
      )
  }
}

export default Loginpage;