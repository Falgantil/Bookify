import bookifyapi from '../util/bookifyapi';
import {observable, computed} from "mobx";
import _ from 'lodash';

class NewBookViewModel {
  @observable availableGenres = [];

  @observable title = '';
  @observable isbn = '';
  @observable summary = '';
  @observable price = '';
  @observable publishYear = '';
  @observable pageCount = '';
  @observable availableCopies = '';
  @observable language = '';
  @observable cover = '';
  @observable genres = [];

  constructor() {
  }

  @computed
  get hasGenres() {
    return this.availableGenres.length > 0;
  }

  async loadGenres() {
    return this.availableGenres = await bookifyapi.getGenres();
  }

  toggleGenre(id, value) {
    if (value) {
      this.genres.push(id);
    } else {
      _.remove(this.genres, (key) => id == key);
    }
  }

  async submit(e) {
    var book = {
      title: this.title,
      isbn: this.isbn,
      authorId: 1,
      publisherId: 1,
      summary: this.summary,
      language: this.language,
      price: this.price,
      publishYear: this.publishYear,
      pageCount: this.pageCount,
      copiesAvailable: this.availableCopies,
      genres: this.genres.map((e) => { return { id: e } })
    };
    return await bookifyapi.postBook(book);
  }
}

export default NewBookViewModel;