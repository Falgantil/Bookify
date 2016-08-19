import React from 'react';
import {observer} from "mobx-react";
import {observable} from "mobx";
import { Link, browserHistory } from 'react-router'
import bookifyapi from '../util/bookifyapi';
import NewBookViewModel from './newbook-view-model';

const CheckBoxView = ({id, value}) => (
    /*
    <label className="mdl-checkbox mdl-js-checkbox mdl-js-ripple-effect" for="checkbox-1">
      <input type="checkbox" id="checkbox-1" className="mdl-checkbox__input" checked />
      <span className="mdl-checkbox__label">Checkbox</span>
    </label>
    */

   <div className="checkbox">
    <label>
      <input type="checkbox"/> Check me out
    </label>
  </div>
)

@observer
class NewBookpage extends React.Component {
  constructor() {
    super(...arguments);
    this.model = new NewBookViewModel();
    this.model.loadGenres();
    this.state = {
      showError: false,
    };
  }

  async submit(e) {
    e.preventDefault();
    var success = await this.model.submit();
    browserHistory.push('/book/1');
  }

  render() {
    return (
      <div className="row">

      <form onSubmit={(e) => this.submit(e)}>
<div className="form-horizontal">
        <h4>Ny bog</h4>


        <div className="form-group">
            <div className="col-md-6">
                <input type="text" className="form-control" placeholder="Title" value={this.model.title} onChange={(e) => this.model.title = e.target.value } required />
            </div>
        </div>

        <div className="form-group">
            <div className="col-md-6">
                <input type="text" className="form-control" placeholder="ISBN" value={this.model.isbn} onChange={(e) => this.model.isbn = e.target.value } />
            </div>
        </div>

        <div className="form-group">
            <div className="col-md-6">
                <input type="text" className="form-control" placeholder="Forfatter" />
            </div>
        </div>

        <div className="form-group">
            <div className="col-md-6">
                <input type="text" className="form-control" placeholder="Resume" value={this.model.summary} onChange={(e) => this.model.summary = e.target.value } />
            </div>
        </div>
<br/>
        <div>
            <label htmlFor="exampleInputFile">Cover</label>
            <input type="file" id="exampleInputFile" />
            <p >Example block-level help text here.</p>
          </div>

        <div className="form-group">
            <div className="col-md-6">
                <select name="languageSelect" id="" className="form-control" value={this.model.language} onChange={(e) => this.model.language = e.target.value } required>
                  <option value="dansk">Dansk</option>
                  <option value="engelsk">Engelsk</option>
                </select>
            </div>
        </div>

        <div className="form-group">
            <div className="col-md-6">
                <input type="text" className="form-control" placeholder="Pris" value={this.model.price} onChange={(e) => this.model.price = e.target.value } />
            </div>
        </div>

        <div className="form-group">
            <div className="col-md-6">
                <input type="text" className="form-control" placeholder="Udgivelses år" value={this.model.publishYear} onChange={(e) => this.model.publishYear = e.target.value } />
            </div>
        </div>

        <div className="form-group">
            <div className="col-md-6">
                <input type="text" className="form-control" placeholder="Side antal" value={this.model.pageCount} onChange={(e) => this.model.pageCount = e.target.value } />
            </div>
        </div>

        <div className="form-group">
            <div className="col-md-6">
                <input type="text" className="form-control" placeholder="Kopier til udlån" value={this.model.availableCopies} onChange={(e) => this.model.availableCopies = e.target.value } />
            </div>
        </div>

        <div className="form-group">
            <div className="col-md-6">
                <button type="submit" className="btn btn-block btn-raised btn-primary"><i className="material-icons">check</i></button>
            </div>
        </div>
    </div>
      </form>


{ this.model.genres.map((genre, index) => <CheckBoxView key={index} value={genre.name} />) }


      </div>
    )
  }
}

export default observer(NewBookpage);