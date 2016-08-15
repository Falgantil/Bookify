import React from 'react'
import _ from 'lodash';

class RatingView extends React.Component {
  render(){
    return (
      <div className="icon-ratings hearts">
      {_.range(0, this.props.value).map(() => (<div className="icon-rating-full"></div>))}
      {_.range(this.props.value, 5).map(() => (<div className="icon-rating-empty"></div>))}
      </div>
    );
  }
}

export default RatingView;