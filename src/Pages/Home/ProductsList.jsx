import React, { useEffect, useState } from 'react'
import DetailedProductsCollection from '../../Components/Lists/ProductsList/DetailedProductsCollection'
import { useProductListQuery } from '../../redux/features/Products/ProductApiSlice'
import Pagination from '../../Components/Common/Pagination'
import Spinner from '../../Components/Common/Spinner'
import { useNavigate, useParams, useSearchParams } from 'react-router-dom'
const ProductsList = () => {

  const [params] = useSearchParams();
  const nav = useNavigate()
  const size = params.get('size')
  const page = params.get('page')

  useEffect(()=>{
    if(!size || !page){
      nav('?size=24&page=1')
    }

  },[size, page])
    
   
  const {data, isLoading} = useProductListQuery({take:size, skip:page-1})

  return (
    <div>
      {
        isLoading?
          <Spinner />
        :
          <DetailedProductsCollection size={size} title={"All Products"} page={page} data={data?.productList} isLoading={isLoading} />
      }
  
      <div className="flex justify-center my-10">
        <Pagination size={parseInt(size)} page={parseInt(page)} totalPages={data?.pages} />
      </div>
    </div>
  )
}

export default ProductsList
