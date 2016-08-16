import $ from 'jquery';
import http from '../util/http';
import {observable, computed} from "mobx";

class BookpageViewModel {
  @observable book;

  constructor() {
  }


  async loadItem(bookId) {
    if (this.bookId == bookId) {
      return;
    }
    this.bookId = bookId;
    this.book = await http.get('http://localhost:9180/books/getBook/' + this.bookId);
  }
}

export default BookpageViewModel;