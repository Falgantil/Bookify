import React from 'react'
import { render } from 'react-dom'
import { Router, Route, browserHistory } from 'react-router'
import App from './modules/App'
import Bookpage from './modules/Bookpage'
import Loginpage from './modules/Loginpage'
import NewBookpage from './modules/NewBookpage'

render((
  <Router history={browserHistory}>
    <Route path="/" component={App}>
      <Route path="/booknew" component={NewBookpage}/>
      <Route path="/book/:bookId" component={Bookpage}/>
      <Route path="/loginpage" component={Loginpage}/>
    </Route>
  </Router>
), document.getElementById('app'))

$.material.init();
