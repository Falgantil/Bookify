import React from 'react'
import {observer} from "mobx-react";
import {observable} from "mobx";
import { Link } from 'react-router';
import bookifyapi from '../util/bookifyapi';


@observer
class Loginpage extends React.Component {
  constructor() {
    super(...arguments);
  }

  render(){
    return (
      <div className="row">
        <div className="col-xs-12 col-sm-6 col-sm-offset-3">
        <h4>Log ind</h4>
        <form>
          <div className="form-group">
            <input type="email" className="form-control" id="exampleInputEmail1" placeholder="Email" />
          </div>
          <div className="form-group">
            <input type="password" className="form-control" id="exampleInputPassword1" placeholder="Password" />
          </div>
          <button type="submit" className="btn btn-block btn-raised btn-primary"><i className="material-icons">check</i></button>
        </form>
        </div>
      </div>
      )
  }
}

export default Loginpage;