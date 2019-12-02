import { combineReducers } from 'redux';
import { defaultBooks, defaultAuth } from './default.state';


const bookReducer = (state = defaultBooks, action) => {
    console.log(action);
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
                books: updateBooks(state, action.payload)
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
                books: updateBooks(state, action.payload)
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


const authReducer = (state = defaultAuth, action) => {
    switch (action.type) {

        case "LOGIN_SUCCESS":
            return {
                ...state,
                token: action.payload.token,
                userName: action.payload.UserName,
                userId: action.payload.UserId,
                name: action.payload.Name,
                autherror: '',
                isSigendIn: true,
            };
        case "LOGIN_FAIL":
            return {
                ...state,
                autherror: action.payload,
                isSigendIn: false,
            };

        case "LOGOUT":
            return defaultAuth;

        case "SIGNUP_SUCCESS":
            return {
                ...state,
                signedUpSucess: true,
                autherror: '',
                userName: action.payload
            };


        case "SIGNUP_FAIL":
            return {
                ...state,
                autherror: action.payload,
                isSigendIn: false,
            };
        default:
            return state;
    }
}

const updateBooks = (state, payload) => {
    const newArr = [...state.books];
    const index = newArr.findIndex(x => x.bookId === payload.bookId);
    newArr[index].loanedToId = payload.loanedToId;
    return newArr;
}

export default combineReducers({
    bookshelf: bookReducer,
    auth: authReducer
});