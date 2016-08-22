import $ from 'jquery';
import http from './http';

let baseUrl = 'http://bookifyapi.azurewebsites.net/';
//let baseUrl = 'http://localhost:13654/';
//let authToken = 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3NkYXRlIjoxNDcxNjA5MTMyLCJleHBkYXRlIjoxNTAzMTQ1MTMyLCJ1c2VyaWQiOjV9.fyv68ofK4E8lyE7hJTVVG9QgY85dWC4YbwkAF7CN4yY';

class BookifyAPI {
  getBaseUrl() { return baseUrl; }
  //getAuthToken() { return authToken; }
  //getRoles() { return roles; }

  // Books
  getBooks() { return http.get(baseUrl + 'books'); }
  getBook(id) { return http.get(baseUrl + 'books/' + id); }
  getBookFeedback(id) { return http.get(baseUrl + 'books/getbookfeedback/' + id); }
  getBookThumbnailSrc(id) { return baseUrl + 'files/' + id + '/downloadcover'; }
  getRelatedBooks(id) { return http.get(baseUrl + 'books?search' + id); }

  postBook(book) { return http.post(baseUrl + 'books', book, { headers: { 'Authorization': 'jwt ' + authToken } }); }
  postBookCover(bookId, data) {
    $.ajax({
      url: baseUrl + 'files/' + bookId + '/uploadcover',
      type: 'POST',
      processData: false,
      contentType: false,
      headers: { 'Authorization': 'jwt ' + authToken },
      data: data
    });
  }

  postBookEPub(bookId, data) {
    $.ajax({
      url: baseUrl + 'files/' + bookId + '/uploadepub',
      type: 'POST',
      processData: false,
      contentType: false,
      headers: { 'Authorization': 'jwt ' + authToken },
      data: data
    });
  }

  getGenres() { return http.get(baseUrl + 'genres'); }

  // Auth
  async login(email, password) {
    var result = await http.post(baseUrl + 'auth/login', { email, password });
    // if (result.Token.length > 0) {
    //   authToken = result.Token;
    //   return result;
    // }
    // return false;
    return result
  }
  logout() { authToken = ''; }


}

export default new BookifyAPI();