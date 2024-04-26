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
                console.log("revoking...");
                return {
                    url:'/api/accounts/refresh-token'
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
    useRefreshTokenQuery
} = authApiSlice