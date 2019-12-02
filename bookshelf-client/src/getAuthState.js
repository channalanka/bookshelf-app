import {defaultAuth} from './reducers/default.state'

export const getAuthStateFromLocalStorage = ()=>{
    try{
        
        const state = localStorage.getItem("authState");
       
        if(!state) return defaultAuth;
        return JSON.parse(state);

    }catch(ex){
        
        return defaultAuth;
    }
}

export const setAuthStateFromLocalStorage = (state)=>{
    try{
        
         const statestr = JSON.stringify(state.auth);
         console.log(statestr);
         localStorage.setItem("authState", statestr);
    

    }catch(ex){
        console.log(ex);
    }
}

export const removeAuthStateFromLocalStorage = ()=>{
    try{
        
         localStorage.removeItem("authState");

    }catch(ex){
        console.log(ex);
    }
}