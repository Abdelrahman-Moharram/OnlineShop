import React, { useEffect, useState } from 'react'
import { BiShoppingBag } from "react-icons/bi";
import { Link, useParams } from 'react-router-dom';
import ProductBanner from '../../Components/Lists/ProductsList/ProductBanner';
import ProductInfo from '../../Components/Lists/ProductsList/ProductInfo';
import { useProductDetailsQuery, useProductSuggestionsQuery } from '../../redux/features/Products/ProductApiSlice';
import ScrollableProductsList from '../../Components/Lists/ProductsList/ScrollableProductsList';
import { useDispatch } from 'react-redux';
import { addToCart } from '../../redux/features/Products/ProductSlice';

const ProductDetails = () => {
    const {id} = useParams()
    const dispatch = useDispatch()
    const {data : product, isLoading} = useProductDetailsQuery(id)
    const { data:categoryProducts } = useProductSuggestionsQuery({Id:product?.categoryId, productId: product?.id}, {skip:!product?.id, refetchOnMountOrArgChange:true})
    const { data:brandProducts} = useProductSuggestionsQuery({Id:product?.brandId, productId: product?.id}, {skip:!product?.id, refetchOnMountOrArgChange:true, })
    const handleAddToCart = () => {
      dispatch(
        addToCart({
          id: product?.id,
          name: product?.name,
          image: product?.image[0],
          price: product?.price,
          quantity: 1
        }
      )
      )
    }
    useEffect(() => {
        window.scroll({top: 0, left: 0, behavior: 'smooth' })
    }, [id]);

  return (
    <div className='container-fluid px-4'>
 
        {/* <div className='px-10 md:px-28 py-8'>
          <BreadCrumb  sections={path.split("/").slice(2)} />
        </div> */}
      
      <div className="grid grid-cols-1 sm:grid-cols-2 mt-10 gap-5 place-items-center mb-5">
        <ProductBanner images={product?.image} isLoading={isLoading} />
        <ProductInfo product={product} />
      </div>
      
      {
        product?.image?
        <>
          <div className='mx-auto my-12 flex justify-evenly w-[50rem] px-7 py-5 rounded-lg shadow-lg shadow-neutral-500'>
            <span className='mt-[8px] truncate'>{product?.name}</span>
            <div
              onClick={handleAddToCart}
              className="cursor-pointer flex rounded-md bg-[#000] px-5 min-w-fit py-2.5 text-sm font-medium text-white transition hover:bg-[#333]"
            >
              <BiShoppingBag className='text-[20px] mx-2' />
              <span className='mt-[3px]'>Add To Cart</span>
            </div>
          </div>


          
          <ScrollableProductsList data={categoryProducts} title={`More in ${product?.categoryName}`}  />

          <ScrollableProductsList data={brandProducts} title={`More by ${product?.brandName}`} />
        </>
        : null
      }

    </div>
  )
}

export default ProductDetails