import React from 'react'
import DetailedProductsCollection from '../../Components/Lists/ProductsList/DetailedProductsCollection'
import { useProductListQuery } from '../../redux/features/Products/ProductApiSlice'
const ProductsList = () => {
  const {data, isLoading} = useProductListQuery({take:24, skip:0})
  
  return (
    <div>
      <DetailedProductsCollection title={"test title"} data={data} isLoading={isLoading} />
    </div>
  )
}

export default ProductsList
