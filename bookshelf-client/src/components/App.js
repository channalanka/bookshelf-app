import React from 'react';
import { connect } from 'react-redux';
import { BrowserRouter, Route, Link,Redirect } from "react-router-dom"
import Container from './Container';
import {logOut} from "../actions";
import {removeAuthStateFromLocalStorage} from "../getAuthState";
import Login from "./Login";
import SignUp from "./SignUp";

const App = (props) => {
    return (
        <div>

            <BrowserRouter>
                <div className="ui secondary pointing menu">
                    {props.auth.isSigendIn ? (
                        <div className="right menu">
                            <div className="item">{props.auth.userName}</div>
                            <button className="item" onClick={() =>{ props.logOut(); removeAuthStateFromLocalStorage();}}>
                                SignOut
                                </button>
                        </div>
                    ) :
                        (
                            <div className="right menu">
                                <Link to='/login' >SignIn</Link>
                              
                            </div>
                        )
                    }
                </div>
                <h2 className="ui center aligned icon header">
                    <i className="circular book icon"></i>
                    Bookshelf App
                </h2>

                <div>
                    <Route path="/" exact component={props.auth.isSigendIn ? Container : Login} />
                    <Route path="/signup" exact component={SignUp} />
                    <Route path="/login" exact component={Login} />
                    <PrivateRoute path='/books' isAuthenticated={props.auth.isSigendIn} component={Container} />
                </div>
            </BrowserRouter>
        </div>
    );

    /* return (<>
    { props.auth.isSigendIn ?  <Container></Container> : <Login></Login>}
    </>); */
}

const PrivateRoute = ({ component: Component, isAuthenticated, ...rest }) => (
    <Route {...rest} render={(props) => (
      isAuthenticated === true
        ? <Component {...props} />
        : <Redirect to='/login' />
    )} />
  )

const mapStateToProps = ({ auth }) => {
    return { auth: auth };
}


export default connect(mapStateToProps, {
    logOut
})(App);