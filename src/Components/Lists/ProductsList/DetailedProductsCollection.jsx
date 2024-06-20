import React from 'react'
import SortCollections from '../../Collections/SortCollections'
import SummaryCollection from '../../Collections/Summary.Collection'
import PriceFilter from '../../Collections/PriceFilter'
import AvailbilityFilter from '../../Collections/AvailbilityFilter'
import ProductCard from '../../Cards/ProductCard'
import PageSizeFilter from '../../Collections/PageSizeFilter'

const DetailedProductsCollection = ({
  title, 
  data, 
  sort,
  size,
  minprice,
  maxprice,
  handleSize,
  handleSort,
  handlePriceFilter
}) => {
  return (
    
<section>
  <div className="mx-auto max-w-screen-xl px-4 py-5 sm:px-6 lg:px-8">
    <header>
      <h2 className="text-xl font-bold text-gray-900 sm:text-3xl">{title}</h2>
    </header>

    <div className="mt-8 sm:flex sm:items-center sm:justify-between">
      
      <div className="hidden sm:flex sm:gap-4">
        <div className="relative">
          <details className="group [&_summary::-webkit-details-marker]:hidden">
            <SummaryCollection />
            <AvailbilityFilter />
          </details>
        </div>

        <PriceFilter minprice={minprice} maxprice={maxprice} handlePriceFilter={handlePriceFilter} />
      </div>

      <div className='flex gap-3'>
        <SortCollections sort={sort} handleSort={handleSort} />
        <PageSizeFilter handleSize={handleSize} size={size} />
      </div>
    </div>



    <div className="mt-4 grid gap-4 sm:grid-cols-2 lg:grid-cols-4">
      {
        data && data.length?
          data.map(product=>(
            <ProductCard product={product} key={product.id} />
          ))
        :
          // to do add product list skeleton
        null
      }
    </div>
  </div>
</section>
  )
}

export default DetailedProductsCollection
