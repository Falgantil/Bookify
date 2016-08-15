import $ from 'jquery';
import http from '../util/http';
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
    this.books = await http.get('http://localhost:9180/books/getBooks');
    console.log(  this.books)

  }
}

export default FrontpageViewModel;