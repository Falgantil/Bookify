import bookifyapi from '../util/bookifyapi';
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
    this.book = await bookifyapi.getBook(bookId);
    console.log(this.book);
  }
}

export default BookpageViewModel;