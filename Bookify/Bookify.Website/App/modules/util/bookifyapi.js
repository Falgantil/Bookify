import $ from 'jquery';
import http from './http';

//let baseUrl = 'https://bookifyapi.azurewebsites.net/';
let baseUrl = 'http://localhost:13654/';
let authToken = 'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3NkYXRlIjoxNDcyNDU2NDkxLCJleHBkYXRlIjoxNTAzOTkyNDkxLCJ1c2VyaWQiOjR9.sCvCJMw-9WuZGAaEOcJv0d16d_MMY-XUg34Jw2jl2lI';

class BookifyAPI {
  getBaseUrl() { return baseUrl; }
  //getAuthToken() { return authToken; }
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

  postBook(book) { return http.post(baseUrl + 'books', book, { headers: { 'Authorization': 'jwt ' + authToken } }); }
  postBookCover(bookId, data) {
    $.ajax({
      url: baseUrl + 'files/' + bookId + '/cover',
      type: 'POST',
      processData: false,
      contentType: false,
      headers: { 'Authorization': 'jwt ' + authToken },
      data: data
    });
  }

  postBookEPub(bookId, data) {
    $.ajax({
      url: baseUrl + 'files/' + bookId + '/epub',
      type: 'POST',
      processData: false,
      contentType: false,
      headers: { 'Authorization': 'jwt ' + authToken },
      data: data
    });
  }

  postBookFeedback(bookId, text, rating) {
    return http.post(baseUrl + 'feedbacks/' + bookId, { text, rating }, { headers: { 'Authorization': 'jwt ' + authToken } });
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