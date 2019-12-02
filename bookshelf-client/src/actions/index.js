export const selectBook= song =>{
    return {
        type: "SELECT_BOOK",
        payload: song
    };
};
export  const bookFetchSuccess = data =>{
    return {
        type:"BOOKS_FETCH_SUCCESS",
        payload: data
    };
};

export  const bookFetchFail = msg =>{
    return {
        type:"BOOKS_FETCH_FAIL",
        payload: msg
    };
};

export  const bookFetchBegin = () =>{
    return {
        type:"BOOKS_FETCH_BEGIN"
    };
};

export  const bookLoanFail = msg =>{
    return {
        type:"BOOKS_LOAN_FAIL",
        payload: msg
    };
};

export  const bookLoanBegin = () =>{
    return {
        type:"BOOKS_LOAN_BEGIN"
    };
};

export  const bookLoanSuccess =  data =>{
    return {
        type:"BOOKS_LOAN_SUCCESS",
        payload: data
    };
};

export  const bookReturnFail = msg =>{
    return {
        type:"BOOKS_RETURN_FAIL",
        payload: msg
    };
};

export  const bookReturnBegin = () =>{
    return {
        type:"BOOKS_RETURN_BEGIN"
    };
};

export  const bookReturnSuccess =  data =>{
    return {
        type:"BOOKS_RETURN_SUCCESS",
        payload: data
    };
};




export  const loginSucess =  data =>{
    return {
        type:"LOGIN_SUCCESS",
        payload: data
    };
};

export  const loginFail = msg =>{
    return {
        type:"LOGIN_FAIL",
        payload: msg
    };
};

export  const logOut = msg =>{
    return {
        type:"LOGOUT"
    };
};

export  const SignUpFail = msg =>{
    return {
        type:"SIGNUP_FAIL",
        payload: msg
    };
};

export  const SignUpSucess = data =>{
    return {
        type:"SIGNUP_SUCESS",
        payload: data
    };
};