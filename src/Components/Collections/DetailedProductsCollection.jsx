import React from 'react'
import SortCollections from './SortCollections'
import SummaryCollection from './Summary.Collection'
import PriceFilter from './PriceFilter'
import AvailbilityFilter from './AvailbilityFilter'
import ProductCard from '../Cards/ProductCard'

const DetailedProductsCollection = ({title}) => {
  return (
    
<section>
  <div className="mx-auto max-w-screen-xl px-4 py-5 sm:px-6 lg:px-8">
    <header>
      <h2 className="text-xl font-bold text-gray-900 sm:text-3xl">{title}</h2>

      <p className="mt-4 max-w-md text-gray-500">
        Lorem ipsum, dolor sit amet consectetur adipisicing elit. Itaque praesentium cumque iure
        dicta incidunt est ipsam, officia dolor fugit natus?
      </p>
    </header>

    <div className="mt-8 sm:flex sm:items-center sm:justify-between">
      
      <div className="hidden sm:flex sm:gap-4">
        <div className="relative">
          <details className="group [&_summary::-webkit-details-marker]:hidden">
            <SummaryCollection />
            <AvailbilityFilter />
          </details>
        </div>

        <PriceFilter />
      </div>

      <SortCollections />
    </div>

    <div className="mt-4 grid gap-4 sm:grid-cols-2 lg:grid-cols-4">
      <ProductCard />
      <ProductCard />
      <ProductCard />
      <ProductCard />
      <ProductCard />
      <ProductCard />
    </div>
  </div>
</section>
  )
}

export default DetailedProductsCollection
