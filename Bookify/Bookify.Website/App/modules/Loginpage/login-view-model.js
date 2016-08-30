import bookifyapi from '../util/bookifyapi';
import {observable, computed} from "mobx";

class LoginViewModel {
  @observable email = '';
  @observable password = '';

  constructor() {
  }

  async submit() {
    return await bookifyapi.login(this.email, this.password);
  }
}

export default LoginViewModel;