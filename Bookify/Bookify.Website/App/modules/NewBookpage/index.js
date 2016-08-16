import React from 'react';
import {observer} from "mobx-react";
import {observable} from "mobx";
import { Link } from 'react-router'
import bookifyapi from '../util/bookifyapi';

@observer
class NewBookpage extends React.Component {
  constructor() {
    super(...arguments);
  }

  render() {
    return (
      <div className="row">

      <form>
<div className="form-horizontal">
        <h4>Ny bog</h4>


        <div className="form-group">
            <div className="col-md-6">
                <input type="text" className="form-control" placeholder="Title" />
            </div>
        </div>

        <div className="form-group">
            <div className="col-md-6">
                <input type="text" className="form-control" placeholder="ISBN" />
            </div>
        </div>

        <div className="form-group">
            <div className="col-md-6">
                <input type="text" className="form-control" placeholder="Author" />
            </div>
        </div>

        <div className="form-group">
            <div className="col-md-6">
                <input type="text" className="form-control" placeholder="Summary" />
            </div>
        </div>

        <div className="form-group">
            <div className="col-md-6">
                <select name="languageSelect" id="" className="form-control">
                  <option value="dansk">Dansk</option>
                  <option value="engelsk">Engelsk</option>
                </select>
            </div>
        </div>

        <div className="form-group">
            <div className="col-md-6">
                <input type="text" className="form-control" placeholder="Pris" />
            </div>
        </div>

        <div className="form-group">
            <div className="col-md-6">
                <input type="text" className="form-control" placeholder="Udgivelses år" />
            </div>
        </div>

        <div className="form-group">
            <div className="col-md-6">
                <input type="text" className="form-control" placeholder="Side antal" />
            </div>
        </div>

        <div className="form-group">
            <div className="col-md-6">
                <input type="text" className="form-control" placeholder="Kopier til udlån" />
            </div>
        </div>

        <div className="form-group">
            <div className="col-md-6">
                <button type="submit" className="btn btn-block btn-raised btn-primary"><i className="material-icons">check</i></button>
            </div>
        </div>
    </div>
      </form>



      </div>
    )
  }
}

export default observer(NewBookpage);