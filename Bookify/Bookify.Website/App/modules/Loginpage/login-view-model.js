import bookifyapi from '../util/bookifyapi';
import {observable, computed} from "mobx";

class LoginViewModel {
@observable email = '';
@observable password = '';

  constructor() {
  }

  async submit() {
    var result = await bookifyapi.login(this.email, this.password);
    return result;
  }
}

export default LoginViewModel;