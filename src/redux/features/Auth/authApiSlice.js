import { apiSlice } from "../../app/api/apiSlice";


export const authApiSlice = apiSlice.injectEndpoints({
    endpoints:(builder)=>({
        register: builder.mutation({
            query:(data)=>({
                url: '/api/accounts/register',
                method:'POST',
                body:{...data},

            })
        }),

        login: builder.mutation({
            query:(data)=>({
                url: '/api/accounts/login',
                method:'POST',
                body:{...data}
            })
        }),
        refreshToken: builder.query({
            query:()=>{
                return {
                    url:'/api/accounts/refresh-token'
                }
            }
        }),
        googleLogin: builder.mutation({
            query:({token})=>{
                return {
                    url:`/api/accounts/google?token=${token}`,
                    headers:{
                        "Cross-Origin-Opener-Policy": "same-origin-allow-popups",
                    },
                }
            }
        }),
        logout: builder.query({
            query:()=>({
                url:'/api/accounts/revoke-token'
            }),
        })
    })
})

export const {
    useRegisterMutation,
    useLoginMutation,
    useLogoutQuery,
    useRefreshTokenQuery,
    useGoogleLoginMutation
} = authApiSlice