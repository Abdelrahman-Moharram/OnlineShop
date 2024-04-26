import { createSlice } from "@reduxjs/toolkit"
import Cookies from "js-cookie"
import { jwtDecode } from "jwt-decode"
const token = Cookies.get('access_token')

const initialState = {
    isAuthenticated: token? true :false,
    user: token? jwtDecode(token): {}
}

const authSlice = createSlice({
    name:'auth',
    initialState,
    reducers:{
        setAuth: (state, action)=>{
            const token = Cookies.get('access_token')
            if(token){
                state.isAuthenticated = token? true : false;
                state.user = token? jwtDecode(token): {}
            }
        },
        setLogout: (state)=>{

            state.isAuthenticated = false
            state.user = {}
        },
    }
    
})

export const {setAuth, setLogout, finishInitialLoad} = authSlice.actions;
export default authSlice.reducer