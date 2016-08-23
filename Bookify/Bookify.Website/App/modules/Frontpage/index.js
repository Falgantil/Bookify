import React from 'react';
import FrontpageViewModel from './frontpage-view-model';
import {observer} from "mobx-react";
import {observable} from "mobx";
import { Link } from 'react-router'
import bookifyapi from '../util/bookifyapi';
import RatingView from '../Shared/rating-view';

const BookView = ({ model, book }) => (
  <div className="book col-xs-12 col-sm-6 col-md-4 col-lg-4">
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

const CheckBoxView = observer(({model, genre}) => (
      <label>
        <input type="checkbox" className="filled-in" checked={model.selectedGenres.indexOf(genre.Id) > -1} onChange={(e) => { model.toggleGenre(genre.Id, e.target.checked);} }/>
        {genre.Name}
      </label>
))

/**
 * The Frontpage.
 */
@observer
class Frontpage extends React.Component {
  constructor() {
    super(...arguments);
    this.model = new FrontpageViewModel();
    this.model.loadBooks();
    this.model.loadGenres();

    this.state = {
      genretoggle: false
    };
  }

  async submit(e) {
    e.preventDefault();
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
              
              <div className="toggler">
                <h3 onClick={() => this.setState({ genreToggle: !this.state.genreToggle })}>Genres</h3>
              </div>
              <div className="input-group-btn">
                <button type="submit" className="btn btn-raised btn-primary"><i className="material-icons">search</i></button>
              </div>
              
            </div>
        </form>
        {this.state.genreToggle && (
                <fieldset className="input-group toggle-content">
                  { this.model.availableGenres.map((genre) => <CheckBoxView key={genre.Id} model={this.model} genre={genre} />) }
                </fieldset>
              )}
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