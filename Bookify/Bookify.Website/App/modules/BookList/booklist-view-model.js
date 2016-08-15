import $ from 'jquery';
import http from '../util/http';
import {observable, computed} from "mobx";

class BookListViewModel {
  @observable books = [];

  constructor() {
  }

 async loadBooks(apiUrl) {
    this.books =  await http.get(apiUrl);
  }
}

export default BookListViewModel;