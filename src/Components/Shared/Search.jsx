import React, { useEffect, useState } from 'react'
import { useSearchQuery } from '../../redux/features/Products/ProductApiSlice';
import { Link } from 'react-router-dom';
const Search = () => {
    const [searchQuery, setSearch] = useState('')
    const {isLoading, data} = useSearchQuery(searchQuery, {skip: !searchQuery, refetchOnMountOrArgChange:true})
  return (
    <div className="">

        <div className="relative">
            <label htmlFor="Search" className="sr-only"> Search </label>

            <input
                type="text"
                id="Search"
                placeholder="Search for..."
                value={searchQuery}
                onChange={e=>setSearch(e.target.value)}
                className="w-full rounded-md border-gray-200 py-2.5 px-10 shadow-xl sm:text-sm outline-none"
            />

            <span className="absolute inset-y-0 end-0 grid w-10 place-content-center">
                <button type="button" className="text-gray-600 hover:text-gray-700">
                <span className="sr-only">Search</span>

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
                        d="M21 21l-5.197-5.197m0 0A7.5 7.5 0 105.196 5.196a7.5 7.5 0 0010.607 10.607z"
                        />
                    </svg>
                </button>
            </span>
        </div>
        {
            searchQuery && data?.length?
                <div className='absolute left-20 right-20 top-[70px] px-2 bg-white shadow-lg rounded-md'>
                    {
                        data.map(prod=>(
                            <div className='px-4 py-2' key={prod?.id}>
                                <Link 
                                    to={`/products/${prod?.id}`}  
                                    className="flex justify-between items-center hover:bg-gray-100 rounded-lg"
                                    onClick={()=>{
                                        if(searchQuery)
                                            setSearch('')
                                    }}
                                >
                                    <div className="col-span-1">
                                    {
                                        prod?.image && prod.image.length?
                                            <img src={prod?.image} alt="logo" width={"60px"} />
                                        :
                                            <img src={Logo} alt="logo" width={"60px"} />
                                    }
                                    </div>
                                    <div className='col-span-10 truncate'>{prod.name}</div>
                                    <div className='col-span-1 rounded-full bg-slate-100 shadow-lg p-2'>{prod.priceAfterDiscount}</div>
                                </Link>
                            </div>
                        ))
                    }
                </div>
            :null
        }
    </div>
  )
}

export default Search
