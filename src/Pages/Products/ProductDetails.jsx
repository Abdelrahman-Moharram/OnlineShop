import React, { useEffect, useState } from 'react'
import { BiShoppingBag } from "react-icons/bi";
import { Link, useParams } from 'react-router-dom';
import ProductBanner from '../../Components/Lists/ProductsList/ProductBanner';
import ProductInfo from '../../Components/Lists/ProductsList/ProductInfo';
import { useProductDetailsQuery, useProductSuggestionsQuery } from '../../redux/features/Products/ProductApiSlice';
import ScrollableProductsList from '../../Components/Lists/ProductsList/ScrollableProductsList';
import Divider from '../../Components/Common/Divider';

const ProductDetails = () => {
    const {id} = useParams()

    const {data : product} = useProductDetailsQuery(id)
    const {data:categoryProducts } = useProductSuggestionsQuery({Id:product?.categoryId, productId: product?.id}, {refetchOnMountOrArgChange:true})
    const {data:brandProducts} = useProductSuggestionsQuery({Id:product?.brandId, productId: product?.id}, {refetchOnMountOrArgChange:true, })
    useEffect(() => {
        window.scroll({top: 0, left: 0, behavior: 'smooth' })
    }, [id]);

  return (
    <div className='container-fluid px-4'>
 
        {/* <div className='px-10 md:px-28 py-8'>
          <BreadCrumb  sections={path.split("/").slice(2)} />
        </div> */}
      
      <div className="grid grid-cols-1 sm:grid-cols-2 mt-10 gap-5 place-items-center mb-5">
        <ProductBanner images={product?.image} />
        <ProductInfo product={product} />
      </div>
      
      {
        product?.image?
        <>
          <div className='mx-auto my-12 flex justify-evenly w-[50rem] px-7 py-5 rounded-lg shadow-lg shadow-neutral-500'>
            <span className='mt-[8px] truncate'>{product?.name}</span>
            <Link
              className="flex rounded-md bg-[#000] px-5 min-w-fit py-2.5 text-sm font-medium text-white transition hover:bg-[#333]"
              href="#"
            >
              <BiShoppingBag className='text-[20px] mx-2' />
              <span className='mt-[3px]'>Add To Cart</span>
            </Link>
          </div>


            <Divider title={`More in ${product?.categoryName}`} />
          <ScrollableProductsList data={categoryProducts} />

          <Divider title={`More by ${product?.brandName}`} />

          <ScrollableProductsList data={brandProducts} />
        </>
        : null
      }

    </div>
  )
}

export default ProductDetails