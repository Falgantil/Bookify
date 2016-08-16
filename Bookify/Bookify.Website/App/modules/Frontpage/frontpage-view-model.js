import bookifyapi from '../util/bookifyapi';
import {observable, computed} from "mobx";

class FrontpageViewModel {
  @observable books = [];

  constructor() {

  }

  @computed
  get hasItems() {
    return this.books.length > 0;
  }

  async loadBooks() {
    this.books = await bookifyapi.getBooks();
    console.log(this.books);

  }
}

export default FrontpageViewModel;