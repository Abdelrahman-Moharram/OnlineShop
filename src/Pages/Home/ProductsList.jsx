import React, { useEffect, useState } from 'react'
import DetailedProductsCollection from '../../Components/Lists/ProductsList/DetailedProductsCollection'
import { useProductListQuery } from '../../redux/features/Products/ProductApiSlice'
import Pagination from '../../Components/Common/Pagination'
import Spinner from '../../Components/Common/Spinner'
import { useNavigate, useSearchParams } from 'react-router-dom'
import { toast } from 'react-toastify'
const ProductsList = () => {

  const [params] = useSearchParams();
  const nav = useNavigate()
  const [size, setSize] = useState(params.get('size') ?? 24)
  const [page, setPage] = useState(params.get('page') ?? 1)
  const [sort, setSort] = useState(params.get('sort') ?? '')
  const [minprice, setMinprice] = useState(params.get('minprice') ??  '')
  const [maxprice, setMaxprice] = useState(params.get('maxprice') ?? '')

  const handleSize =(val)=>{
    if(val.match(/^[0-9]+$/))  
      setSize(parseInt(val))
    else{
      toast.error("invalid size filter")
    }
  }
  const handlePage =(val)=>{
    console.log(val);
    if(!isNaN(val)){
      setPage(val)
    }
    else if(val.match(/^[0-9]+$/))
      setPage(parseInt(val))
    else{
      toast.error("invalid page filter")
    }
  }
  const handleSort = (val) => {
    setSort(val)
  }

  const handlePriceFilter =({min, max})=>{
    if(!isNaN(min))
      setMinprice(min)
    else if(min.match(/^[0-9]+$/))
      setMinprice(parseFloat(min))
    else if(min == '')
      setMinprice(min)
    else
      toast.error("invalid min value")

    if(!isNaN(max))
      setMaxprice(max)
    else if(max.match(/^[0-9]+$/))
      setMaxprice(parseFloat(max))
    else if(max == '')
      setMaxprice(max)
    else
      toast.error("invalid max value")

      console.log(min, max);
  }

  useEffect(()=>{
      nav(`?size=${size}&page=${page}&sort=${sort}&minprice=${minprice}&maxprice=${maxprice}`)
  },[
    size,
    page,
    sort,
    minprice,
    maxprice])
    
   
  const {data, isLoading} = useProductListQuery({take:size, skip:page-1, sort, minprice, maxprice}, {refetchOnMountOrArgChange:true})

  useEffect(()=>{
    setMinprice(data?.minPrice)
    setMaxprice(data?.maxPrice)
  },[isLoading])

  return (
    <div>
      {
        isLoading?
          <Spinner />
        :
          <DetailedProductsCollection 
            title={"All Products"} 
            handleSize={handleSize}
            handleSort={handleSort}
            handlePriceFilter={handlePriceFilter}
            size={size} 
            data={data?.productList} 
            sort={sort} 
            isLoading={isLoading} 
            minprice={minprice} 
            maxprice={maxprice} 
          />
      }
  
      <div className="flex justify-center my-10">
        <Pagination handlePage={handlePage} page={page} totalPages={data?.pages} />
      </div>
    </div>
  )
}

export default ProductsList
