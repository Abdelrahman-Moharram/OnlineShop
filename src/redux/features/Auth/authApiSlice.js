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
    useLogoutQuery
} = authApiSlice