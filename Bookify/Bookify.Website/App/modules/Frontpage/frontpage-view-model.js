import bookifyapi from '../util/bookifyapi';
import {observable, computed} from "mobx";
import _ from 'lodash';

class FrontpageViewModel {
  @observable availableGenres = [];
  @observable books = [];
  @observable search = '';
  @observable selectedGenres = [];

  constructor() {

  }

  @computed
  get hasGenres() {
    return this.availableGenres.length > 0;
  }

  async loadGenres() {
    this.availableGenres = await bookifyapi.getGenres();
  }

  toggleGenre(id, value) {
    if (value) {
      this.selectedGenres.push(id);
    } else {
      _.remove(this.selectedGenres, (key) => id == key);
    }
  }

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