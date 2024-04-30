import React, { useState } from 'react'
import { Link } from 'react-router-dom'

const SortCollections = ({sort, handleSort}) => {
  const [showMenu, setShowMenu] = useState(false)

  return (
    <div className="relative" onClick={()=>setShowMenu(!showMenu)}>
      <div className="cursor-pointer inline-flex items-center overflow-hidden  border-none outline-none rounded-md bg-white">
        <div
          
          className="px-4 py-2 text-sm/none text-gray-600 hover:bg-gray-50 hover:text-gray-700"
        >
          Sort By ({sort})
        </div>

       
      </div>

      {
        showMenu?
          <div
            className="absolute end-0 z-10 mt-2 w-56 rounded-md border border-gray-100 bg-white shadow-lg"
            role="menu"
          >
            <div className="p-2">
              <div
                className="cursor-pointer block rounded-lg px-4 py-2 text-sm text-gray-500 hover:bg-gray-50 hover:text-gray-700"
                role="menuitem"
                onClick={()=>{handleSort('asc')}}
              >
                Asending
              </div>

              <Link
                className="cursor-pointer block rounded-lg px-4 py-2 text-sm text-gray-500 hover:bg-gray-50 hover:text-gray-700"
                role="menuitem"
                onClick={()=>{handleSort('des')}}
              >
                Decsinding
              </Link>
        </div>
          </div>
        :null
      }
    </div>
  )
}

export default SortCollections