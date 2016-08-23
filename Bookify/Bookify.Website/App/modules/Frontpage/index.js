import React from 'react';
import FrontpageViewModel from './frontpage-view-model';
import {observer} from "mobx-react";
import {observable} from "mobx";
import { Link } from 'react-router'
import bookifyapi from '../util/bookifyapi';
import RatingView from '../Shared/rating-view';

const BookView = ({ model, book }) => (
  <div className="book col-xs-12 col-sm-6 col-md-4 col-lg-3">
    <Link  to={"/book/" + book.Id}>
    <div className="book-content">
          <img className="cover" src={bookifyapi.getBookThumbnailSrc(book.Id)} alt="" width="100" height="145" />
        <div className="book-text-wrap">
            <div>
              <strong>{book.Title}</strong>
            </div>
            <div>{book.Author.Name} </div>
            <div title={book.AverageRating}>
              <RatingView value={book.AverageRating} />
            </div>
        </div>
      </div>
    </Link>
  </div>
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

  async submit(e) {
    e.preventDefault();
    console.log(e);
    var result = await this.model.submit(e);
  }

  render() {
    return (
      <div>
      <div className="row">
        <div className="col-xs-12">

        <form ref="searchForm" onSubmit={(e) => this.submit(e)} className="searchForm">
            <div className="input-group">
              <input type="text" className="form-control" aria-label="..." placeholder="Søg" value={this.model.search} onChange={(e) => this.model.search = e.target.value}/>
              <div className="input-group-btn">
                <button type="submit" className="btn btn-raised btn-primary"><i className="material-icons">search</i></button>
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