import React from 'react';
import {observer} from "mobx-react";
import {observable} from "mobx";
import { Link, browserHistory } from 'react-router'
import bookifyapi from '../util/bookifyapi';
import NewBookViewModel from './newbook-view-model';
import _ from 'lodash';
import $ from 'jquery';

const CheckBoxView = observer(({model, genre}) => (
    <div className="col-xs-3">
        <label>
            <input type="checkbox"
                checked={model.genres.indexOf(genre.Id) > -1}
                onChange={(e) => {
                    model.toggleGenre(genre.Id, e.target.checked);
                } }
                /> {genre.Name}
        </label>
    </div>
))

@observer
class NewBookpage extends React.Component {
  constructor() {
    super(...arguments);
    this.model = new NewBookViewModel();
    this.model.loadGenres();
    this.state = {
      showError: false
    };
  }

  async submit(e) {
    e.preventDefault();
    var formData = new FormData();
    formData.append('cover', this.refs.coverInput.files[0]);

    var result = await this.model.submit(e, formData);
    //await bookifyapi.postBookCover(23, formData, (data) => { console.log(data); } );
  }

  render() {
    return (
      <div className="row">

      <form ref="uploadForm" onSubmit={(e) => this.submit(e)}>
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
            <label>Cover</label>
            <input type="file" name="coverInput" ref="coverInput" />
          </div>

        <div className="form-group">
            <div className="col-md-6">
                <select id="" className="form-control" value={this.model.language} onChange={(e) => this.model.language = e.target.value } required>
                  <option value="dansk">Dansk</option>
                  <option value="engelsk">Engelsk</option>
                </select>
            </div>
        </div>

        <div>
        <label>Genre</label>
            <br/>
            <div className="row">
            { this.model.availableGenres.map((genre) => <CheckBoxView key={genre.Id} model={this.model} genre={genre} />) }
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

      </div>
    )
  }
}

export default observer(NewBookpage);