import React from 'react'
import { useSelector } from 'react-redux'
import { ImFileEmpty } from "react-icons/im";
import CartItem from './CartItem';
import { Link } from 'react-router-dom';
import { FaCartShopping } from 'react-icons/fa6';

const Cart = () => {
    const {cart} = useSelector(state=>state.product)

    return (
        <div className='absolute right-10 top-[60px] max-w-[40rem] bg-white rounded-md shadow-lg px-5 py-4 z-10 my-3'>
            {
                cart && cart.length?
                    <div>
                        {
                            cart.map((item, idx)=>(
                                <CartItem item={item} key={idx} />
                            ))
                        }
                        <div className='flex justify-end mt-4'>
                            <Link className='flex items-center px-8 py-2 rounded-md shadow-md gap-2 bg-black text-white' to={'/cart'}>
                                <FaCartShopping />
                                Cart 
                            </Link>
                        </div>
                    </div>
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