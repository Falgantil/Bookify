import React from 'react'
import BookListViewModel from './booklist-view-model';
import {observer} from "mobx-react";
import {observable} from "mobx";
import { Link } from 'react-router'

const Book = ({ book }) => (
  <Link to={"/" + book.Id} className="book-thumbnail">
    <img src={"http://localhost:9180/books/thumbnail/" + book.Id} alt="" />
    <p>{book.Title}</p>
  </Link>
  )

@observer
class BookListView extends React.Component {
  constructor(args) {
    super(...arguments);
    this.model = new BookListViewModel();
    this.model.loadBooks(args.type, args.bookId);
  }

  render() {
    return (
      <div>
        {this.model.books.filter((b)  => b.Id != this.props.bookId).map((b, index) => <Book book={b} key={index} />) }
      </div>
    )
  }
}

export default BookListView;