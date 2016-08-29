import React from 'react'
import BookpageViewModel from './bookpage-view-model';
import {observer} from "mobx-react";
import {observable} from "mobx";
import { Link } from 'react-router';
import BookListView from '../Shared/BookList';
import RatingView from '../Shared/rating-view';
import FeedbackForm from '../Shared/FeedbackForm';
import bookifyapi from '../util/bookifyapi';

const GenreList = ({ genre, isLast }) => (
  <span>
    <a href="#">{genre.Name}</a>
    {!isLast && (<span>, </span>)}
  </span>
  )

const Feedback  = ({name, rating, text}) => (
    <div className="row">
      <div className="bookFeedback">
         <div className="col-xs-3">
          <b>{name}</b>
          <RatingView value={rating} />
        </div>
        <div className="col-xs-9">
          <p>{text}</p>
        </div>
      </div>
      <br />
      <br />
    </div>
  )

/**
 * The Bookpage.
 */
@observer
class Bookpage extends React.Component {
  constructor() {
    super(...arguments);
    this.model = new BookpageViewModel();

    this.forceUpdate =  this.forceUpdate.bind(this);
  }

  forceUpdate(dto) {
    this.model.book.Feedback.push(dto);
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
                    <img className="cover" src={bookifyapi.getBookThumbnailSrc(this.model.book.Id)} alt=""/>
                </div>
            </div>
            <h1>{this.model.book.Title}</h1>
            <small>af <a href="#">{this.model.book.Author.Name}</a></small>
            <h4><RatingView value="3" /></h4>
            <p><Link className="btn btn-primary btn-lg btn-raised" to="/">KØB</Link></p>
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
  <hr/>
  <div className="row">
    <h4 className="col-xs-12">Bøger af samme forfatter</h4>
    <div className="col-xs-12">
      <BookListView type="sameAuthor" book={this.model.book} />
    </div>
  </div>
  <hr/>
   <div className="row">

   <div className="col-xs-12">
        <FeedbackForm bookId={this.model.book.Id} callback={this.forceUpdate} />
   </div>

    <h4 className="col-xs-12">Bedømmelser</h4>
    <div className="col-xs-12">
        {this.model.book.Feedback.map((feedback, index) =>
          <Feedback key={index} name={feedback.Person.FirstName + ' ' + feedback.Person.LastName} rating={feedback.Rating} text={feedback.Text} />
          )}
    </div>
  </div>
  <hr />
  </div>
    )
  }
}

export default Bookpage;