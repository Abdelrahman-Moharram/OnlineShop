import React, { useState } from 'react'
import UserNavDropDown from './UserNavDropDown'
import { Link } from 'react-router-dom'
import { FaCartShopping } from "react-icons/fa6";
import Cart from '../Cart/Cart';
import { useSelector } from 'react-redux';

const NavAuthOptions = ({isAuthenticated, user}) => {
    const [cartToggler, setCartToggler] = useState(false)
    const {cart} = useSelector(state=>state.product)

    return (
    <div>
        {
            isAuthenticated?
                <div className='flex gap-3'>
                    <div
                        onClick={()=>setCartToggler(!cartToggler)} 
                        className="relative rounded-full p-4 transition-all cursor-pointer hover:bg-gray-100"
                    >
                        {
                        cart.length?
                            <div 
                                className="absolute top-0 right-0 text-[10px] rounded-full px-1.5 p-[2px] bg-red-600 text-white"
                            >
                                {cart.length}
                            </div>
                        :
                            null
                        }

                        <FaCartShopping className='text-lg' />
                    </div>
                    {
                        cartToggler?

                            <Cart />
                        
                        :null
                    }
                    <UserNavDropDown user={user} />
                </div>
            :
                <div className="sm:flex sm:gap-4">
                    <Link
                        className="rounded-md bg-teal-600 px-5 py-2.5 text-sm font-medium text-white shadow"
                        to='/auth/login'
                    >
                        Login
                    </Link>

                    <div className="hidden sm:flex">
                        <Link
                            className="rounded-md bg-gray-100 px-5 py-2.5 text-sm font-medium text-teal-600"
                            to='/auth/register'
                        >
                            Register
                        </Link>
                    </div>
                </div>
        }
    </div>
    
  )
}

export default NavAuthOptions