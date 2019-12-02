import bookShelfApi from "../api/bookselfapi";

export const fetchBooks = ()=> async (dispatch, getState)=>{
    dispatch({type:"BOOKS_FETCH_BEGIN"});
     try{
    const response=  await bookShelfApi.get("/api/bookshelf", {
        headers:{
            'Content-Type': 'application/json',
            'Authorization': 'Barer ' + getState().auth.token
        }
     });

     console.log(response);
     if(response.status === 200){
        dispatch({type:"BOOKS_FETCH_SUCCESS", payload: response.data});
     }else{
         dispatch({type:"BOOKS_FETCH_FAIL", payload: "Server error"});
     }
    }catch(ex){
        dispatch({type:"BOOKS_FETCH_FAIL", payload: "api connection error"});
    }
 }


export const loanBook = (data)=> async (dispatch, getState)=>{
    dispatch({type:"BOOKS_LOAN_BEGIN"});
     try{
         console.log(data);
    const response=  await bookShelfApi.post("/api/bookshelf/loan",
    data,
     { 
         headers:{
            'Content-Type': 'application/json',
            'Authorization': 'Barer ' + getState().auth.token
        }
     });

     console.log(response);
     if(response.status === 200 && response.data){
        dispatch({
            type:"BOOKS_LOAN_SUCCESS", 
            payload: {loanedToId: getState().auth.userId, bookId:data.BookId }
        });
        dispatch(fetchBooks());
     }else{
         dispatch({type:"BOOKS_LOAN_FAIL", payload: "Seever error"});
     }
    }catch(ex){
        dispatch({type:"BOOKS_LOAN_FAIL", payload: ex.message});
    }
 }


export const returnBook = (data)=> async (dispatch, getState)=>{
    dispatch({type:"BOOKS_RETURN_BEGIN"});
     try{
         console.log(data);
    const response=  await bookShelfApi.post("/api/bookshelf/return",
    data,
     { 
         headers:{
            'Content-Type': 'application/json',
            'Authorization': 'Barer ' + getState().auth.token
        }
     });

     console.log(response);
     if(response.status === 200 && response.data){
        dispatch({
            type:"BOOKS_RETURN_SUCCESS", 
            payload: {bookId:data.BookId }
        });
        dispatch(fetchBooks());
     }else{
         dispatch({type:"BOOKS_RETURN_FAIL", payload: "Server error"});
     }
    }catch(ex){
        dispatch({type:"BOOKS_RETURN_FAIL", payload: ex.message});
    }
 }



export const login = (data, routrhistory)=> async (dispatch)=>{
    //dispatch({type:"BOOKS_RETURN_BEGIN"});
     try{
     
            const response=  await bookShelfApi.post("/token",
                 data,
                { 
                    headers:{
                        'Content-Type': 'application/json'
                    }
                }
            );
         
             if(response.status === 200 && response.data){
                 const tokenParts = response.data.split(".");
                 const decodedString = atob(tokenParts[1]);
                 const {Payload} = JSON.parse(decodedString);
                 Payload.token = response.data;
                dispatch({type:"LOGIN_SUCCESS", payload: Payload});
                routrhistory.push('/books');
             }else{
                console.log(response);
                 dispatch({type:"LOGIN_FAIL", payload: "Invalid username or password"});
             }
        }catch(ex){
            console.log(ex);
            dispatch({type:"LOGIN_FAIL", payload: "Invalid username or password"});
        }
 }




export const signUpUser = (data)=> async (dispatch, getState)=>{
    //dispatch({type:"BOOKS_RETURN_BEGIN"});
     try{
            console.log(data);
            const response=  await bookShelfApi.post("/api/user",
                data,
                { 
                    headers:{
                        'Content-Type': 'application/json'
                    }
                }
            );

            console.log(response);
            if(response.status === 200){
               dispatch({type:"SIGNUP_SUCCESS", payload: data.UserName});
            }else{
                dispatch({type:"SIGNUP_FAIL", payload: "Server Error"});
            }
        }catch(ex){
            console.log(ex);
            dispatch({type:"SIGNUP_FAIL", payload: ex.message});
        }
 }
