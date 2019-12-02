import React from 'react';
import ReactDom from 'react-dom';
import {Provider} from "react-redux";
import {createStore, applyMiddleware} from "redux";
import Thunk from "redux-thunk";

import App from './components/App';
import rootReducer from "./reducers";


ReactDom.render(
<Provider store={createStore(
    rootReducer,
    applyMiddleware(Thunk)
    )}>
    <App/>
</Provider>, 
document.querySelector("#root"));
