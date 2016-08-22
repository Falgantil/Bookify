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
  @observable language = 'dansk';
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

  async submit(e, cover, epub) {
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
      genres: this.genres
    };
    var result = await bookifyapi.postBook(book);
    await bookifyapi.postBookCover(result.Id, cover);
    await bookifyapi.postBookEPub(result.Id, epub);
    return result;
  }
}

export default NewBookViewModel;