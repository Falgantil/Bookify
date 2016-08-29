import bookifyapi from '../util/bookifyapi';
import {observable, computed} from "mobx";
import SessionStore from '../Shared/SessionStore'

class RegistrationViewModel {
  @observable firstName = '';
  @observable lastName = '';
  @observable username = '';
  @observable email = '';
  @observable password = '';

  constructor() {
  }

  async submit() {
    var result = await bookifyapi.register(this.firstName, this.lastName, this.username, this.email, this.password);

    SessionStore.authenticate(result);

    return result;
  }
}

export default RegistrationViewModel;