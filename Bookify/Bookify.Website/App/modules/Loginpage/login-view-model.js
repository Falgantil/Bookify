import bookifyapi from '../util/bookifyapi';
import {observable, computed} from "mobx";

class LoginViewModel {
@observable email = 'admin@test.com';
@observable password = '';

  constructor() {
  }

  async submit() {
    var result = await bookifyapi.login(this.email, this.password);
    $.cookie = "AuthToken="+result.Token+";role="+result.Roles;
    console.log("Token: " + result.Token);
    console.log("Roles: " + result.Roles);
    console.log("Alias: " + result.Alias);
    
    return result;
  }
}

export default LoginViewModel;