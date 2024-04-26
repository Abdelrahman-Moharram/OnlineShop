import React from 'react'
import { FaPlus, FaMinus } from "react-icons/fa";
import { useDispatch } from 'react-redux';
import { decreaseQuantity, increaseQuantity } from '../../redux/features/Products/ProductSlice';

const QuntityInput = ({id, quantity}) => {
    const dispatch = useDispatch()
    
  return (
    <div>
        <label htmlFor="Quantity" className="sr-only"> Quantity </label>

        <div className="flex items-center rounded border border-gray-200">
            <button 
                onClick={()=>dispatch(decreaseQuantity(id))}
                type="button" className="px-[10px] size-10 leading-10 text-gray-600 transition hover:opacity-75"
            >
                <FaMinus />
            </button>

            <div
                id="Quantity"
                className="pt-[10px] h-10 w-16 border-transparent text-center [-moz-appearance:_textfield] sm:text-sm [&::-webkit-inner-spin-button]:m-0 [&::-webkit-inner-spin-button]:appearance-none [&::-webkit-outer-spin-button]:m-0 [&::-webkit-outer-spin-button]:appearance-none"
            >
                {quantity}
            </div>

            <button 
                onClick={()=>dispatch(increaseQuantity(id))}
                type="button" className="px-[10px] size-10 leading-10 text-gray-600 transition hover:opacity-75"
            >
                <FaPlus />
            </button>
        </div>
    </div>
  )
}

export default QuntityInput