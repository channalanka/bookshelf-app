import { combineReducers } from 'redux';
import { defaultBooks, defaultAuth } from './default.state';
import {getAuthStateFromLocalStorage} from "../getAuthState"


const bookReducer = (state = defaultBooks, action) => {
    
    switch (action.type) {
        case "BOOKS_FETCH_SUCCESS":
            return {
                ...state,
                books: action.payload,
                loading: false
            };
        case "BOOKS_FETCH_FAIL":
            return {
                ...state,
                books: [],
                loading: false,
                error: action.payload,
            };
        case "BOOKS_FETCH_BEGIN":
            return {
                ...state,
                loading: true
            };
        case "SELECT_BOOK":
            return {
                ...state,
                error: '',
                selectedBook: action.payload,
            };
        case "BOOKS_LOAN_SUCCESS":
            return {
                ...state,
                selectedBook: {
                    ...state.selectedBook,
                    loanedToId: action.payload.loanedToId
                },
                loading: false,
                books: updateBooks([...state.books], action.payload)
            };
        case "BOOKS_LOAN_BEGIN":
            return {
                ...state,
                loading: true
            };
        case "BOOKS_LOAN_FAIL":
            return {
                ...state,
                loading: false,
                error: action.payload,
            };
        case "BOOKS_RETURN_SUCCESS":
            return {
                ...state,
                selectedBook: {
                    ...state.selectedBook,
                    loanedToId: null
                },
                loading: false,
                books: updateBooks([...state.books], action.payload)
            };
        case "BOOKS_RETURN_BEGIN":
            return {
                ...state,
                loading: true
            };
        case "BOOKS_RETURN_FAIL":
            return {
                ...state,
                loading: false,
                error: action.payload,
            };
        default:
            return state;
    }
}


const intialState = getAuthStateFromLocalStorage();
const authReducer = (state = intialState, action) => {
    switch (action.type) {

        case "LOGIN_SUCCESS":
            return {
                ...state,
                token: action.payload.token,
                userName: action.payload.UserName,
                userId: action.payload.UserId,
                name: action.payload.Name,
                msg: '',
                isSigendIn: true,
            };
        case "LOGIN_FAIL":
            return {
                ...state,
                msg: action.payload,
                isSigendIn: false,
            };

        case "LOGOUT":
            return defaultAuth;

        case "SIGNUP_SUCCESS":
            return {
                ...state,
                signedUpSucess: true,
                msg: action.payload.msg,
                userName: action.payload.userName
            };


        case "SIGNUP_FAIL":
            return {
                ...state,
                msg: action.payload,
                isSigendIn: false,
            };
        default:
            return state;
    }
}

const updateBooks = (books, payload) => {

    return books.map((item) => {
        if (item.bookId !== payload.bookId)
          return item
        
        return {
          ...item,
          loanedToId: payload.loanedToId
        }
      })
   
}

export default combineReducers({
    bookshelf: bookReducer,
    auth: authReducer
});