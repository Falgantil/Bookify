import $ from 'jquery';
import http from './http';

let baseUrl = 'http://bookifyapi.azurewebsites.net/';
let authToken = '';

class BookifyAPI {
  getBaseUrl() { return baseUrl; }
  getAuthToken() { return authToken; }

  // Books
  getBooks() { return http.get(baseUrl + 'books');  }
  getBook(id) { return http.get(baseUrl + 'books/' + id); }
  getBookFeedback(id) { return http.get(baseUrl + 'books/getbookfeedback/' + id); }
  getBookThumbnailSrc(id) { return baseUrl + 'books/' + id + '/cover'; }
  getRelatedBooks(id) { return http.get(baseUrl + 'books?search' + id); }

  postBook(book) { http.post('http://localhost:9180/books/create', book); }
  postBookCover(bookId, data) { http.post(baseUrl + 'files/' + bookId + '/UploadCover', data, { processData: false, contentType: false, Authorization: 'jwt' + authToken }); }

  getGenres() { return http.get('http://localhost:9180/books/genres'); }

  // Auth
  async login(email, password) {
    var result = await http.post(baseUrl + 'auth/login', { email, password });
    if (result.Token.length > 0) {
      authToken = result.Token;
      return true;
    }
    return false;
  }
  logout() { authToken = ''; }

}

export default new BookifyAPI();