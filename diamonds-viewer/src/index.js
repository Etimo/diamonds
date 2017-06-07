import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux'
import { createStore, combineReducers, applyMiddleware, compose } from 'redux'
import createSagaMiddleware from 'redux-saga'

import { updateSaga, highscoreSaga } from './sagas'
import boardReducer from './reducers/boardReducer'
import App from './containers/App';

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose
const sagaMiddleware = createSagaMiddleware()

const store = createStore(combineReducers({gameboard: boardReducer}),
        composeEnhancers(applyMiddleware(sagaMiddleware)))

sagaMiddleware.run(updateSaga)
sagaMiddleware.run(highscoreSaga)

ReactDOM.render(
    (<Provider store={store}>
      <App />
    </Provider>),
  document.getElementById('root')
);
