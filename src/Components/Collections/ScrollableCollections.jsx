import React from 'react'
import { Link } from 'react-router-dom'

const ScrollableCollections = ({title, children}) => {
  return (
    <section>
      <div className="mx-auto max-w-screen-xl px-4 py-5 sm:px-6 lg:px-8">
        <header className='my-3'>
          <Link className='hover:underline' to={"/products"}>
            <h2 className="text-xl font-bold text-gray-900 sm:text-3xl">{title} </h2>
          </Link>
        </header>


        <div >
            {children}
        </div>
      
      </div>
    </section>
  )
}

export default ScrollableCollections
