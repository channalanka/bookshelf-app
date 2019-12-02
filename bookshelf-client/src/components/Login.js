import React from 'react';
import { connect } from 'react-redux';
import { login } from "../actions/middleware";
import { logOut } from '../actions';
import{Link} from 'react-router-dom';

class Login extends React.Component {
    constructor(props) {
        super(props);
    
        this.props.logOut();

        this.state = {
            username: '',
            password: ''
        };
    }

    handleChange(e) {
        const { name, value } = e.target;
        this.setState({ [name]: value });
    }

    loginSubmit(e) {
        e.preventDefault();

        this.setState({ submitted: true });
        const { username, password } = this.state;
        if (username && password) {
            
            this.props.login(
                {
                    userName: username, 
                    password: password
                }, 
                this.props.history
            );
        }
    }

    render() {
        const { username, password } = this.state;
        const {auth}  = this.props
        return (
        <div className="ui placeholder segment">
            <div className="ui icon header">Sign In</div>
            <br/>
            {auth.msg && <div className="ui negative message">
                <i className="close icon"></i>
                <div className="header">
                        Error
                </div>
                <p>{auth.msg}</p>
            </div>
            }
            <form className="ui form" onSubmit={(e) => this.loginSubmit(e)} >
                <div className="field">
                    <label>User Name</label>
                    <input type="text"  name="username" value={username} onChange={(e) =>this.handleChange(e)} placeholder="User Name" />
                </div>
                <div className="field">
                    <label>Password</label>
                    <input type="password" name="password" value={password} onChange={(e) => this.handleChange(e)}  placeholder="Password" />
                </div>

                <button className="ui primary button" type="submit">Login</button>
                <br/>
                
                <Link to="/signup" className="ui primary basic button">Register</Link>
            </form>
        </div>
        );

    }
}

const mapStateToProps = ({ auth }) => {
    return { auth: auth };
}


export default connect(mapStateToProps,{
        login,
        logOut
    })(Login);