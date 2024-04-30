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
  const [minprice, setMinprice] = useState(params.get('minprice') ?? '')
  const [maxprice, setMaxprice] = useState(params.get('maxprice') ?? '')

  const handleSize =(val)=>{
    if(val.match(/^[0-9]+$/))  
      setSize(parseInt(val))
    else{
      toast.error("invalid size filter")
    }
  }
  const handlePage =(val)=>{
    if(val.match(/^[0-9]+$/))
      setPage(val)
    else{
      toast.error("invalid page filter")
    }
  }
  const handleSort = (val) => {
    setSort(val)
  }
  const handleMinprice =(val)=>{
    console.log(val);
    if(val.match(/^[0-9]+$/))
      setMinprice(parseFloat(val))
    else if(val == '')
      setMinprice(val)
    else
      toast.error("invalid page filter")

  }
  const handleMaxprice =(val)=>{
    if(val.match(/^[0-9]+$/))
      setMaxprice(parseFloat(val))
    else if(val == '')
      setMaxprice(val)
    else
      toast.error("invalid page filter")
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
            handleMinprice={handleMinprice}
            handleMaxprice={handleMaxprice}
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
