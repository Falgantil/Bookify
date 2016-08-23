import React from 'react'
import _ from 'lodash';

class RatingView extends React.Component {
  render(){
    return (
      <div className="icon-ratings hearts">
      {_.range(0, Math.round(this.props.value)).map((index) => (<div className="icon-rating-full" key={index}></div>))}
      {_.range(Math.round(this.props.value), 5).map((index) => (<div className="icon-rating-empty" key={index}></div>))}
      </div>
    );
  }
}

export default RatingView;