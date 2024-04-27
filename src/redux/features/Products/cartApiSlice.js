import { apiSlice } from "../../app/api/apiSlice";



export const cartApiSlice = apiSlice.injectEndpoints({
    endpoints:(builder)=>({
        getUserCart: builder.query({
            query:()=>({
                url:'/api/Carts'
            })
        }),
        deleteCartItem: builder.mutation({
            query:(id)=>({
                url:`/api/Carts/delete/${id}`,
                method:'DELETE'
            })
        }),
        addItemToCart: builder.mutation({
            query:(data)=>{
                console.log(data);
                return{
                    url:'/api/Carts/update',
                    method:'POST',
                    body:{...data}
                }
            }
        })

    })
})


export const {
    useGetUserCartQuery,
    useAddItemToCartMutation,
    useDeleteCartItemMutation
} = cartApiSlice