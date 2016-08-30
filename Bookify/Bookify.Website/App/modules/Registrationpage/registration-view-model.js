import bookifyapi from '../util/bookifyapi';
import {observable, computed} from "mobx";

class RegistrationViewModel {
  @observable firstName = '';
  @observable lastName = '';
  @observable username = '';
  @observable email = '';
  @observable password = '';

  constructor() {
  }

  async submit() {
    return await bookifyapi.register(this.firstName, this.lastName, this.username, this.email, this.password);
  }
}

export default RegistrationViewModel;