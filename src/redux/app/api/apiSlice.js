import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import Cookies from 'js-cookie';
const baseQuery = fetchBaseQuery({
    baseUrl:import.meta.env.VITE_BASE_URL,
    credentials:'include',
    prepareHeaders:(headers)=>{
        const token = Cookies.get('access_token')
        if(token){
            headers.set('authorization', `Bearer ${token}`)
        }
        return headers
    }
})

const baseQueryWithReAuth = async (args, api, extraOptions)=>{
    let result = await baseQuery(args, api, extraOptions);
    if (result?.error?.status === 403 || result?.error?.status === 401){
        const res = await baseQuery('/api/accounts/refresh-token', api, extraOptions);
        if(res?.data?.isSuccessed){
            Cookies.set('access_token', res?.data?.token)
            result = await baseQuery(args, api, extraOptions)
        }else{
            if(res.error.status === 403)
                res.error.data.message = "Your Session has expired !"
            return res
        }
    }
    return result
}

export const apiSlice = createApi({
    baseQuery: baseQueryWithReAuth,
    endpoints:()=>({}),
})

