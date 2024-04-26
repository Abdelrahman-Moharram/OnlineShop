import React from 'react'
import { useDispatch } from 'react-redux'
import QuntityInput from '../Collections/QuntityInput'
import { MdDelete } from "react-icons/md";
import { deleteFromCart } from '../../redux/features/Products/ProductSlice';


const CartItem = ({item}) => {
    const dispatch = useDispatch()
    const handleDelete = (id)=>{
        dispatch(deleteFromCart(id))
    }
  return (
    <div className='flex justify-evenly gap-5 cursor-pointer py-5 px-3 bg-white rounded-xl mb-2 border-2'>
        <img src={item.image.includes('https')?  item.image :import.meta.env.VITE_BASE_URL+item.image} alt="logo" width={"60px"} srcSet="" />
        <div className='truncate'>
            <div className='truncate'>{item?.name}</div>
            <div className='flex justify-between pr-3 mt-3 font-bold'>
                <p className=''>{item?.price}</p>
                <div className='flex gap-3'>
                    
                    <QuntityInput id={item?.id} quantity={item?.quantity} />
                    
                    <div 
                        onClick={()=>handleDelete(item?.id)}
                        className="cursor-pointer text-red-600 text-2xl p-2 rounded-full hover:bg-gray-100"
                    >
                        <MdDelete />
                    </div>

                </div>
            </div>
        </div>
    </div>
  )
}

export default CartItem