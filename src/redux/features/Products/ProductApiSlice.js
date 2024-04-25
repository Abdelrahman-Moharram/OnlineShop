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
        }),
        search : builder.query({
            query:(query)=>{
                return{
                    url:`/api/Products/search`,
                    params:{'query':query}
                }
                
            }
        }),

        productDetails : builder.query({
            query:(id)=>{
                return{
                    url:`/api/Products/${id}`,
                }
            }
        }),

        productSuggestions : builder.query({
            query:({Id, productId})=>{
                console.log();
                return{
                    url:`/api/Products/suggestions-category-brand`,
                    params:{'id':Id, 'productid':productId}
                }
            }
        }),

        
        
    })
})


export const {
    useProductListQuery,
    useHomePageQuery,
    useSearchQuery,
    useProductDetailsQuery,
    useProductSuggestionsQuery
} = productApiSlice