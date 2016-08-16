import bookifyapi from '../util/bookifyapi';
import {observable, computed} from "mobx";

class BookListViewModel {
  @observable books = [];

  constructor() {
  }

 async loadBooks(type, bookId) {
    switch(type) {
      case "related":
          this.books = await  bookifyapi.getRelatedBooks(bookId);
          break;
      default:
    }
  }
}

export default BookListViewModel;