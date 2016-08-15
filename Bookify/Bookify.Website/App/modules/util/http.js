// A wrapper around jQuery's ajax.
import $ from 'jquery';
import _ from 'lodash';

let defaultOptions = {
  contentType: 'application/json;charset=utf8',
  dataType: 'json'
};

class Http {
  /**
   * Sends a HTTP GET request.
   */
  get(url, data, options) {
    options = this._mergeOptions(options);
    options.type = 'GET';
    options.data = data;
    options.url = url;
    return $.ajax(options);
  }

  /**
   * Sends a HTTP POST request.
   */
  post(url, data, options) {
    options = this._mergeOptions(options);
    options.type = 'POST';
    options.data = JSON.stringify(data);
    options.url = url;
    return $.ajax(options);
  }

  /**
   * Sends a HTTP PUT request.
   */
  put(url, data, options) {
    options = this._mergeOptions(options);
    options.type = 'PUT';
    options.data = JSON.stringify(data);
    options.url = url;
    return $.ajax(options);
  }

  /**
   * Sends a HTTP DELETE request.
   */
  delete(url, data, options) {
    options = this._mergeOptions(options);
    options.type = 'DELETE';
    options.data = data;
    options.url = url;
    return $.ajax(options);
  }

  /**
   * Merges the additional options with the defaults, and returns the merged options.
   */
  _mergeOptions(options) {
    return _.extend({}, defaultOptions, options || {});
  }
}

export default new Http();