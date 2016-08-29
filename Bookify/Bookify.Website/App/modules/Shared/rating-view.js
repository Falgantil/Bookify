import React from 'react'
import _ from 'lodash';

class RatingView extends React.Component {
getRatingClass(index){
  var rating = parseFloat(this.props.value);
  var result = Math.round((rating - index) * 10);
  result = Math.min(10, Math.max(0, result));
  return 'icon-rating-' + result;
}

  render(){
    return (
      <div className="icon-ratings hearts" title={this.props.value}>
            {_.range(0, 5).map((index) => (<div className={this.getRatingClass(index)} key={index}></div>))}
      </div>
    );
  }
}

export default RatingView;