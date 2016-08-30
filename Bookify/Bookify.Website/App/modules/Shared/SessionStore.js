import {observable} from "mobx";
import cookie from 'react-cookie';

const CookieIdentifier = "auth_token";

/**
 * The SessionStore.
 */
class SessionStore {

  constructor() {
    var result = cookie.load(CookieIdentifier);
    if (result) {
      this.currentUser = result;
    }
  }

  @observable currentUser = null;

  @observable
  loggedIn = () => this.currentUser != null;

  authenticate(result) {
    this.currentUser = result;
    cookie.save(CookieIdentifier, result);
  }

  deauthenticate() {
    this.currentUser = null;
    cookie.remove(CookieIdentifier);
  }
}

export default new SessionStore();