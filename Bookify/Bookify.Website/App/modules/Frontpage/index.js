import React from 'react';
import FrontpageViewModel from './frontpage-view-model';
import {observer} from "mobx-react";
import {observable} from "mobx";
import { Link } from 'react-router'
import bookifyapi from '../util/bookifyapi';

const BookView = ({ model, book }) => (
  <Link className="col-lg-2 col-md-2 col-sm-2 col-xs-4" to={"/book/" + book.Id}>
      <img className="cover" src={bookifyapi.getBookThumbnailSrc(book.Id)} alt="" />
  </Link>
)

/**
 * The Frontpage.
 */
@observer
class Frontpage extends React.Component {
  constructor() {
    super(...arguments);
    this.model = new FrontpageViewModel();
    this.model.loadBooks();
  }

  render() {
    return (
      <div>
      <div className="row">
        <div className="col-xs-12">

        <form className="searchForm">
              <div className="input-group">
                <input type="text" className="form-control" aria-label="..." placeholder="Søg" />
                <div className="input-group-btn">
                  <button className="btn btn-raised btn-primary"><i className="material-icons">search</i></button>
                </div>
              </div>
          </form>

        </div>
      </div>
        <div className="row">
              <div className="col-xs-12">
                  { this.model.books.map((book, index) => <BookView model={this.model} book={book} key={index} />) }
              </div>
          </div>
      </div>
    )
  }
}

export default observer(Frontpage);