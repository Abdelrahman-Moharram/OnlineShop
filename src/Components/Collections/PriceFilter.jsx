import React, { useState } from 'react'

const PriceFilter = ({minprice, maxprice, handlePriceFilter}) => {
  const [prices, setPrices] = useState({
    minprice:minprice,
    maxprice: maxprice
  })
  return (
    <div className="relative z-[1]">
          <details className="group [&_summary::-webkit-details-marker]:hidden">
            <summary
              className="flex cursor-pointer items-center gap-2 border-b border-gray-400 pb-1 text-gray-900 transition hover:border-gray-600"
            >
              <span className="text-sm font-medium"> Price </span>

              <span className="transition group-open:-rotate-180">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  fill="none"
                  viewBox="0 0 24 24"
                  strokeWidth="1.5"
                  stroke="currentColor"
                  className="h-4 w-4"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    d="M19.5 8.25l-7.5 7.5-7.5-7.5"
                  />
                </svg>
              </span>
            </summary>

            <div
              className="z-50 group-open:absolute group-open:top-auto group-open:mt-2 ltr:group-open:start-0"
            >
              <div className="w-96 rounded border border-gray-200 bg-white">
                <header className="flex items-center justify-between p-4">
                  <span className="text-sm text-gray-700"> The highest price is $600 </span>

                  <button 
                    type="button" 
                    className="text-sm text-gray-900 underline underline-offset-4"
                    onClick={()=>{
                      setPrices((prev)=>({...prev, minprice:'', maxprice:''}))
                    }}
                  >
                    Reset
                  </button>
                </header>

                <div className="border-t border-gray-200 p-4">
                  <div className="flex justify-between gap-4">
                    <label htmlFor="FilterPriceFrom" className="flex items-center gap-2">
                      <span className="text-sm text-gray-600">L.E</span>

                      <input
                        type="number"
                        id="FilterPriceFrom"
                        placeholder="From"
                        className="w-full rounded-md border-gray-200 shadow-sm sm:text-sm"
                        value={prices.minprice ?? minprice}
                        onChange={(e)=>setPrices((prev)=>({...prev, minprice:e.target.value}))}
                      />
                    </label>

                    <label htmlFor="FilterPriceTo" className="flex items-center gap-2">
                      <span className="text-sm text-gray-600">L.E</span>

                      <input
                        type="number"
                        id="FilterPriceTo"
                        placeholder="To"
                        className="w-full rounded-md border-gray-200 shadow-sm sm:text-sm"
                        value={prices.maxprice ?? maxprice}
                        onChange={(e)=>setPrices((prev)=>({...prev, maxprice:e.target.value}))}
                      />
                    </label>
                  </div>

                  <div className='flex justify-end mt-4'>
                    <button
                      className="inline-block rounded border transition-colors  border-black bg-black px-4 py-1.5 text-sm font-medium text-white hover:bg-transparent hover:text-indigo-600 focus:outline-none focus:ring active:text-indigo-500"
                      onClick={()=>handlePriceFilter({min:prices.minprice??minprice, max:prices.maxprice??maxprice})}
                    >
                      apply
                    </button>

                  </div>
                </div>
              </div>
            </div>
          </details>
        </div>
  )
}

export default PriceFilter