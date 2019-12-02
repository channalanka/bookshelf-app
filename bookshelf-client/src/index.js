import React from 'react';
import ReactDom from 'react-dom';
import {Provider} from "react-redux";
import {createStore, applyMiddleware} from "redux";
import Thunk from "redux-thunk";

import App from './components/App';
import rootReducer from "./reducers";
import{ setAuthStateFromLocalStorage} from "./getAuthState";

const store = createStore(
    rootReducer,
    applyMiddleware(Thunk)
    );
    
store.subscribe(()=>{
    setAuthStateFromLocalStorage({
        auth : store.getState().auth
    })
})
   
ReactDom.render(
<Provider store={store}>
    <App/>
</Provider>, 
document.querySelector("#root"));
