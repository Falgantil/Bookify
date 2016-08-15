import React from 'react'
import { render } from 'react-dom'
import { Router, Route, browserHistory } from 'react-router'
import App from './modules/App'
import Bookpage from './modules/Bookpage'

render((
  <Router history={browserHistory}>
    <Route path="/" component={App}>
      <Route path="/:bookName" component={Bookpage}/>
    </Route>
  </Router>
), document.getElementById('app'))

$.material.init();
