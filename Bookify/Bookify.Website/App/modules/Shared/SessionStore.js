import {observable} from "mobx";

/**
 * The SessionStore.
 */
class SessionStore {
  @observable currentUser = null;

  authenticate(result) {
    $.cookie = "AuthToken="+result.Token+";role="+result.Roles;
    this.currentUser = result;
  }
}

export default new SessionStore();