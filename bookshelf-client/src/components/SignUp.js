import React from 'react';
import { connect } from 'react-redux';
import { logOut } from '../actions';
import { signUpUser } from "../actions/middleware";
import{Link} from 'react-router-dom';


class SignUp extends React.Component {
    constructor(props) {
        super(props);

        this.props.logOut();
        this.state = {
            username: '',
            password: '',
            name:'',
            confirmPassword: ''
        };


    }

    handleChange(e) {
        const { name, value } = e.target;
        this.setState({ [name]: value });
    }

    signUpSubmit(e) {
        e.preventDefault();

        this.setState({ submitted: true });
        const { username, password, confirmPassword, name } = this.state;
        this.props.signUpUser(
            {
                Name: name,
                UserName: username,
                Password: password,
                ConfirmPassword: confirmPassword
            }
        );

        this.setState({ username: '', password:'', confirmPassword:'', name:'' });
    }

    render() {
        const { username, password, confirmPassword, name} = this.state;
        const {auth} = this.props
        return (
            <div className="ui placeholder segment">
                 <div className="ui icon header">Register</div>
            <br/>
            {auth.msg && <> {
            auth.signedUpSucess ? (
                <div className="ui positive message">
                    <i className="close icon"></i>
                    <div className="header">
                        Sucess
                    </div>
                    <p>
                        {auth.msg}
                    </p>
                </div> 
                ) : 
                ( 
                <div className="ui negative message">
                    <i className="close icon"></i>
                    <div className="header">
                        Error
                    </div>
                    <p>
                        {auth.msg}
                    </p>
                </div>  
                )}
                </> }

                <form className="ui form" onSubmit={(e) => this.signUpSubmit(e)} >
                <div className="field">
                        <label>Name</label>
                        <input type="text" name="name" value={name} onChange={(e) => this.handleChange(e)} placeholder=" Name" />
                    </div>
                    <div className="field">
                        <label>User Name</label>
                        <input type="text" name="username" value={username} onChange={(e) => this.handleChange(e)} placeholder="Use Name" />
                    </div>
                    <div className="field">
                        <label>Password</label>
                        <input type="password" name="password" value={password} onChange={(e) => this.handleChange(e)}  />
                    </div>
                    <div className="field">
                        <label>Confirm Password</label>
                        <input type="password" name="confirmPassword" value={confirmPassword} onChange={(e) => this.handleChange(e)}  />
                    </div>
                    <button className="ui button" >Register</button>
                </form>
                <br></br>
                {auth.signedUpSucess && 
                <Link className="ui primary basic button" to="/login"  >
                        Go Back To Login
                </Link>
                }
                <div>

                </div>
            </div>
        );

    }
}
const mapStateToProps = ({ auth }) => {
    return { auth: auth };
}

export default connect(mapStateToProps, {
    signUpUser,
    logOut
})(SignUp);