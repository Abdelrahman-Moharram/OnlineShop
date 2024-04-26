import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    cart:[]
}

const productSlice = createSlice({
    name:"product",
    initialState,
    reducers:{
        addToCart: (state, action)=>{
            console.log(state.cart.filter(i=>i.id === action?.payload?.id).length);
            if(state.cart.filter(i=>i.id === action?.payload?.id).length){
                state.cart.find(item=>item?.id === action.payload?.id).quantity += action.payload.quantity

            }else{
                state.cart.push(action.payload)
            }
        },
        deleteFromCart: (state, action)=>{
            state.cart.splice(state.cart.indexOf(i=>i.id === action.payload), 1)
        },
        clearCart:(state)=>{
            state.cart = {}
        },
        increaseQuantity: (state, action)=>{
            state.cart.find(item=>item?.id === action.payload).quantity += 1
        },
        decreaseQuantity: (state, action)=>{
            state.cart.find(item=>item?.id === action.payload).quantity -= 1
        },
        
    }
})


export const {addToCart, increaseQuantity, decreaseQuantity, deleteFromCart} = productSlice.actions
export default productSlice.reducer