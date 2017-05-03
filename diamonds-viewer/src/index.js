import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux'
import { createStore, combineReducers, applyMiddleware, compose } from 'redux'
import createSagaMiddleware from 'redux-saga'

import { updateSaga } from './sagas'
import boardReducer from './reducers/boardReducer'
import App from './containers/App';

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose
const sagaMiddleware = createSagaMiddleware()

const store = createStore(combineReducers({rows: boardReducer}), 
        composeEnhancers(applyMiddleware(sagaMiddleware)))

ReactDOM.render(
    (<Provider store={store}>
      <App />
    </Provider>),
  document.getElementById('root')
);
