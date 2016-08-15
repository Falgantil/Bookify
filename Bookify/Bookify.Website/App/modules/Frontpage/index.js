import React from 'react';
import FrontpageViewModel from './frontpage-view-model';
import {observer} from "mobx-react";
import {observable} from "mobx";
import { Link } from 'react-router'

const BookView = ({ model, book }) => (
  <Link className="col-lg-2 col-md-2 col-sm-2 col-xs-4" to={"/" + book.Id}>
      <img className="img-responsive" src={"http://localhost:9180/books/thumbnail/" + book.Id} alt=""/>
  </Link>
)

/**
 * The Frontpage.
 */
@observer
class Frontpage extends React.Component {
  constructor() {
    super(...arguments);
    this.model = new FrontpageViewModel();
    this.model.loadBooks();
  }

  render() {
    return (
      <div>
        {this.model.hasItems && <strong>You have items yo!</strong>}
        {!this.model.hasItems && <strong>You don't have any items. Poor you!</strong>}
        {this.model.hasItems && (
          <div className="row">
              <div className="col-xs-12">
                  { this.model.books.map((book, index) => <BookView model={this.model} book={book} key={index} />) }
              </div>
          </div>
        )}
      </div>
    )
  }
}

export default observer(Frontpage);