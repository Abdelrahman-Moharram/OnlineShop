import React from 'react'
import { useSelector } from 'react-redux'
import { ImFileEmpty } from "react-icons/im";
import CartItem from './CartItem';

const Cart = () => {
    const {cart} = useSelector(state=>state.product)

    return (
        <div className='absolute right-10 top-[60px] max-w-[40rem] bg-white rounded-md shadow-lg px-5 py-4 z-10 my-3'>
            {
                cart && cart.length?
                    cart.map((item, idx)=>(
                        <CartItem item={item} key={idx} />
                    ))
                :
                <div className='py-3 px-20'>
                    <p className='text-lg font-bold flex gap-4 items-center'>
                        <ImFileEmpty /> 
                        No Item Added to Cart
                    </p>
                </div>
            }
        </div>
    )
}

export default Cart