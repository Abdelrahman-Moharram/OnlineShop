import { apiSlice } from "../../app/api/apiSlice";


export const productApiSlice = apiSlice.injectEndpoints({
    endpoints:(builder)=>({
        productList: builder.query({
            query:()=>({
                url:'/api/Products'
            })
        }),
        HomePage : builder.query({
            query:()=>({
                url:'/api/home'
            })
        })
    })
})


export const {
    useProductListQuery,
    useHomePageQuery
} = productApiSlice