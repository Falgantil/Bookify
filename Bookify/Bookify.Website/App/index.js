import React from 'react'
import { render } from 'react-dom'
import { Router, Route, useRouterHistory } from 'react-router'
import App from './modules/App'
import About from './modules/About'
import Repos from './modules/Repos'
import { createHashHistory } from 'history'

const appHistory = useRouterHistory(createHashHistory)({
  queryKey: false
})

render((
  <Router history={appHistory}>
    <Route path="/" component={App}>
      {/* make them children of `App` */}
      <Route path="/repos" component={Repos}/>
      <Route path="/about" component={About}/>
    </Route>
  </Router>
), document.getElementById('app'))