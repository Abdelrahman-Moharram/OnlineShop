import React from 'react'
import InfoSkeleton from '../../Skeletons/InfoSkeleton'
import { Link } from 'react-router-dom'

const ProductInfo = ({product}) => {
  return (
    <div>
    {
        product?.name?
            <div>
                <h2 className='text-[20px]'>{product?.name}</h2>
                <Link  to={`/brands/${product?.brandId}`}  className="flex text-gray-600 hover:text-gray-500 text-[13px] mb-3">
                    {product?.brandName}
                </Link>
                
                {
                    product?.price?
                    <>
                        <span className="text-xl">Price</span>
                        <p className='text-[17px] mb-7'> {product?.price} EGP</p>
                    </>
                    :null
                }
                

                <p>

                    {product?.description}
                </p>
            </div>
        :
        <InfoSkeleton />
    }
  


</div>
  )
}

export default ProductInfo