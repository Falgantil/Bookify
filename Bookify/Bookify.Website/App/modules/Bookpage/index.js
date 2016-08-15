import React from 'react'
import BookpageViewModel from './bookpage-view-model';
import {observer} from "mobx-react";
import {observable} from "mobx";
import { Link } from 'react-router';
import BookListView from '../BookList';
import RatingView from '../Shared/rating-view';

const GenreList = ({ genre, isLast }) => (
  <span>
    <a href="#">{genre.Name}</a>
    {!isLast && (<span>, </span>)}
  </span>
  )



/**
 * The Bookpage.
 */
@observer
class Bookpage extends React.Component {
  constructor() {
    super(...arguments);
    this.model = new BookpageViewModel();
  }

  render() {
    this.model.loadItem(this.props.params.bookId);

    if (!this.model.book) return (<div></div>);
    return (
      <div>
  <div className="row">
      <div className="col-xs-12 text-center">
            <div className="book-cover">
                <div className="image-container">
                    <img className="cover" src={"http://localhost:9180/books/thumbnail/" + this.model.book.Id} />
                </div>
            </div>
            <h1>{this.model.book.Title}</h1>
            <small>af <a href="#">{this.model.book.Author.Name}</a></small>
        </div>
      <div className="col-xs-12">
          <dl className="dl-horizontal">
              <dt>Beskrivelse</dt>
              <dd>{this.model.book.Summary}</dd>
              <dt>Genre</dt>
              <dd>
              {this.model.book.Genres.map((b, count) =>
                <GenreList key={b.Id} genre={b} isLast={this.model.book.Genres.length == count + 1} />
                )}
              </dd>
              <dt>ISBN</dt>
              <dd>{this.model.book.ISBN}</dd>
              <dt>Sprog</dt>
              <dd>{this.model.book.Language}</dd>
              <dt>Udgivelses år</dt>
              <dd>{this.model.book.PublishYear}</dd>
              <dt>Pris</dt>
              <dd>{this.model.book.Price},-</dd>
          </dl>
      </div>
  </div>
  <div className="row">
    <h4 className="col-xs-12">Bøger af samme forfatter</h4>
    <div className="col-xs-12">
      <BookListView apiUrl="http://localhost:9180/books/getRelatedBooks" currentBookId={this.model.book.Id} />
    </div>
  </div>
   <div className="row">
    <h4 className="col-xs-12">Bedømmelser</h4>
    <div className="col-xs-12">
        <div className="row">
                  <div className="col-xs-4">
                      <b>Mette Jacobsen</b>
                     <RatingView value="4" />
                  </div>
                  <div className="col-xs-8"></div>
        </div>
    </div>
  </div>
  </div>
    )
  }
}

export default Bookpage;