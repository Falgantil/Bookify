import $ from 'jquery';
import http from './http';
import SessionStore from '../Shared/SessionStore';

let baseUrl = 'https://bookifyapi.azurewebsites.net/';
// let baseUrl = 'http://localhost:13654/';
let getAuthToken = () => {
  if (!SessionStore.currentUser) {
    return null;
  }
  return SessionStore.currentUser.Token;
};

class BookifyAPI {
  getBaseUrl() { return baseUrl; }
  //getRoles() { return roles; }

  // Books
  getBooks(args) {
  	if (args != null) {
  		var argumentbuilder = '?';
  		var first = true;
  		$.each(args, function(key, value) {
  			if (typeof value === 'object') {
  				$.each(value.slice(), function(index, item) {
  					if (first) {
  						argumentbuilder += key + "[]=" + item;
  						first = false;
  					} else {
  						argumentbuilder += "&" + key + "[]=" + item;
  					}
  				});
  			} else {
  				if (first) {
  					argumentbuilder += key + "=" + value;
  					first = false;
  				} else {
  					argumentbuilder += "&" + key + "=" + value;
  				}
  			}
  		});
      console.log(baseUrl + 'books' + argumentbuilder);
  		return http.get(baseUrl + 'books' + argumentbuilder);
  	} else {
      console.log(baseUrl + 'books' + argumentbuilder);
  		return http.get(baseUrl + 'books');
  	}
  }
  getBook(id) { return http.get(baseUrl + 'books/' + id); }
  getBookFeedback(id) { return http.get(baseUrl + 'books/getbookfeedback/' + id); }
  getBookThumbnailSrc(id) { return baseUrl + 'files/' + id + '/cover'; }

  postBook(book) {
    return http.post(
      baseUrl + 'books',
      book,
      {
        headers: { 'Authorization': 'JWT ' + getAuthToken() }
      });
  }

  postBookCover(bookId, data) {
    $.ajax({
      url: baseUrl + 'files/' + bookId + '/cover',
      type: 'POST',
      processData: false,
      contentType: false,
      headers: { 'Authorization': 'JWT ' + getAuthToken() },
      data: data
    });
  }

  postBookEPub(bookId, data) {
    $.ajax({
      url: baseUrl + 'files/' + bookId + '/epub',
      type: 'POST',
      processData: false,
      contentType: false,
      headers: { 'Authorization': 'JWT ' + getAuthToken() },
      data: data
    });
  }

  postBookFeedback(bookId, text, rating) {
    return http.post(
      baseUrl + 'feedbacks/' + bookId,
      { text, rating },
      {
        headers: { 'Authorization': 'JWT ' + getAuthToken() }
      });
  }

  getGenres() { return http.get(baseUrl + 'genres'); }

  // Auth
  async login(email, password) {
    var result = await http.post(baseUrl + 'auth/login', { email, password });
    SessionStore.authenticate(result);
    return result
  }

  // Register
  async register(firstName, lastName, username, email, password) {
    var result = await http.post(baseUrl + 'auth/register', {
      firstName,
      lastName,
      username,
      email,
      password
    });
    SessionStore.authenticate(result);
    return result
  }

  logout() {
    SessionStore.deauthenticate();
  }
}

export default new BookifyAPI();