import React from 'react';
import FrontpageViewModel from './frontpage-view-model';
import {observer} from "mobx-react";
import {observable} from "mobx";
import { Link } from 'react-router'

const BookView = ({ model, book }) => (
  <Link className="col-sm-4" to={"/" + book.id}>
    <div className="col-sm-6">
      <img src="https://i.mofibo.net/covers/3/8/2/9788758821283_2by3_120.jpg" />
    </div>
    <div className="col-sm-6">
      <h3>Bogens Titel</h3>
      <p>En kort beskrivelse omkring bogen</p>
    </div>
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
    this.model.loadItems();
  }

  render() {
    return (
      <div>
        {this.model.hasItems && <strong>You have items yo!</strong>}
        {!this.model.hasItems && <strong>You don't have any items. Poor you!</strong>}
        {this.model.hasItems && (
          <div className="row">
            { this.model.items.map((item, index) => <BookView model={this.model} book={item} key={index} />) }
          </div>
        )}
      </div>
    )
  }
}

export default observer(Frontpage);