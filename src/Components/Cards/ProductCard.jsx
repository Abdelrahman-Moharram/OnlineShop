import React from 'react'
import { Link } from 'react-router-dom'

const ProductCard = ({product}) => {
  return (
    product?
    <Link to={`/products/${product?.id}`} className="group block overflow-hidden">
        <img
          // src={import.meta.env.VITE_BASE_URL+product?.image[0]}
          src={product?.image[0]?.includes('http')?  product?.image[1] :import.meta.env.VITE_BASE_URL+product?.image[1]}
          alt=""
          className="h-[350px] w-full object-cover transition duration-500 group-hover:scale-105 sm:h-[450px]"
        />

        <div className="relative bg-white pt-3">
          <h3 className="text-xs text-gray-700 group-hover:underline group-hover:underline-offset-4">
              {product?.name}
          </h3>

          <p className="mt-2">
              <span className="sr-only"> Regular Price </span>
              <span className="tracking-wider text-gray-900"> {product?.priceAfterDiscount} L.E </span>
              <del className='text-xs'>{product?.price} L.E</del>
          </p>
        </div>
    </Link>
    :null
  )
}

export default ProductCard