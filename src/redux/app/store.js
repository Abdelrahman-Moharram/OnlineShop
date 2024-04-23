import { configureStore } from "@reduxjs/toolkit";
import { setupListeners } from "@reduxjs/toolkit/query";
import { apiSlice } from "./api/apiSlice";
import authSlice from "../features/Auth/authSlice";



export const store = configureStore({
    reducer:{
        auth: authSlice,
        [apiSlice.reducerPath]: apiSlice.reducer,
    },
    middleware:(getDefaultMiddleware)=>
        getDefaultMiddleware().concat(apiSlice.middleware),
    
    devTools: import.meta.env.VITE_NODE_ENV === 'development',
    })

setupListeners(store.dispatch);