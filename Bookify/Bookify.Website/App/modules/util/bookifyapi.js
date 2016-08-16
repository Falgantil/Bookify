import $ from 'jquery';
import http from './http';

//let baseUrl = 'http://localhost:9180/';
let baseUrl = 'http://bookifyapi.azurewebsites.net/';

class BookifyAPI {
  getBaseUrl() { return baseUrl; }

  getBooks() { return http.get(baseUrl + 'books/getbooks');  }
  getBook(id) { return http.get(baseUrl + 'books/getbook/' + id); }


  getRelatedBooks(id) { return http.get(baseUrl + 'books/getrelatedbooks/' + id); }

}

export default new BookifyAPI();