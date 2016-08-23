import bookifyapi from '../util/bookifyapi';
import {observable, computed} from "mobx";

class BookListViewModel {
  @observable books = [];

  constructor() {
  }

 async loadBooks(type, book) {
    switch(type) {
      case "sameAuthor":
          this.books = await  bookifyapi.getRelatedBooksByAuthor(book.Author.Id);
          break;
      default:
    }
  }
}

export default BookListViewModel;