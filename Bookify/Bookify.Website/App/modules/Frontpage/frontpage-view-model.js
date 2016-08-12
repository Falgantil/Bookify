import $ from 'jquery';
import http from '../util/http';
import {observable, computed} from "mobx";

class FrontpageViewModel {
  @observable items = [];

  constructor() {

  }

  @computed
  get hasItems() {
    return this.items.length > 0;
  }

  async loadItems() {
    let items = await http.get('https://api.github.com/users/jeffijoe/repos');
    this.items.replace(items);
    return this.items;
  }
}

export default FrontpageViewModel;