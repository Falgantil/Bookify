import React from 'react'
import FeedbackFormViewModel from './feedbackform-view-model';
import bookifyapi from '../../util/bookifyapi';
import {observer} from "mobx-react";
import {observable} from "mobx";
import { Link } from 'react-router'

@observer
class FeedbackForm extends React.Component {
  constructor(args) {
    super(...arguments);
    console.log(args);
    this.model = new FeedbackFormViewModel(args.bookId);
    this.state = { showError: false };
  }

  async submit(e) {
    e.preventDefault();
    this.model.submit(e);
    var success = await this.model.submit();
    if (success) { window.location.reload(); }
  }

  render() {
    return (
      <div className="row">
          <form onSubmit={(e) => this.submit(e)}>
            <div className="col-xs-2">
              <input type="number" className="form-control" id="rating" placeholder="Rating" value={this.model.rating} min='0' max='5' onChange={(e) => this.model.rating = e.target.value } />
            </div>
            <div className="col-xs-8">
              <input type="text" className="form-control" id="text" placeholder="Text" value={this.model.text} onChange={(e) => this.model.text = e.target.value } />
            </div>
            <div className="col-xs-2">
                <button type="submit" className="btn btn-block btn-raised btn-primary"><i className="material-icons">check</i></button>
            </div>
          </form>
      </div>
    )
  }
}

export default FeedbackForm;