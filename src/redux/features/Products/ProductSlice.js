import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    cart:[]
}

const productSlice = createSlice({
    name:"product",
    initialState,
    reducers:{
        fetchCart:(state, action)=>{
            state.cart = action.payload
        },
        addToCart: (state, action)=>{
            console.log(state.cart.filter(i=>i.productId === action?.payload?.productId).length);
            if(state.cart.filter(i=>i.productId === action?.payload?.productId).length){
                state.cart.find(item=>item?.productId === action.payload?.productId).quantity += action.payload.quantity

            }else{
                state.cart.push(action.payload)
            }
        },
        deleteFromCart: (state, action)=>{
            state.cart.splice(state.cart.indexOf(i=>i.productId === action.payload), 1)
        },
        clearCart:(state)=>{
            state.cart = {}
        },
        increaseQuantity: (state, action)=>{
            state.cart.find(item=>item?.productId === action.payload).quantity += 1
        },
        decreaseQuantity: (state, action)=>{
            state.cart.find(item=>item?.productId === action.payload).quantity -= 1
        },
        
    }
})


export const {
    addToCart, 
    increaseQuantity, 
    decreaseQuantity, 
    deleteFromCart, 
    fetchCart
} = productSlice.actions
export default productSlice.reducer