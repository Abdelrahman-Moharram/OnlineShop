import { apiSlice } from "../../app/api/apiSlice";



export const roleApiSlice = apiSlice.injectEndpoints({
    endpoints: (builder)=>({
        rolesList: builder.query({
            query:()=>({
                url:'/api/Roles'
            }),
        })
    })
})

export const{
    useRolesListQuery
} = roleApiSlice