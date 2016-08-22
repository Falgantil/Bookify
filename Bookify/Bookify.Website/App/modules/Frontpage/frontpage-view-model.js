import bookifyapi from '../util/bookifyapi';
import {observable, computed} from "mobx";

class FrontpageViewModel {
  @observable books = [];
  @observable search = '';

  constructor() {

  }

  @computed
  get hasItems() {
    return this.books.length > 0;
  }

  async loadBooks() {
    this.books = await bookifyapi.getBooks();
  }

  async submit(e) {
    this.books = await bookifyapi.getBooks({search:this.search})
  }
}

export default FrontpageViewModel;