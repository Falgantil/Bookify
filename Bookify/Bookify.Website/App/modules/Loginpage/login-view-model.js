import bookifyapi from '../util/bookifyapi';
import {observable, computed} from "mobx";
import SessionStore from '../Shared/SessionStore'

class LoginViewModel {
  @observable email = '';
  @observable password = '';

  constructor() {
  }

  async submit() {
    var result = await bookifyapi.login(this.email, this.password);

    SessionStore.authenticate(result);

    return result;
  }
}

export default LoginViewModel;