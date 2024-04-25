import React from 'react'
import { Outlet } from 'react-router-dom'
import Nav from '../../Components/Shared/Nav'
import SideNav from '../../Components/Shared/SideNav'

const Layout = () => {
  return (
    <div className='bg-gray-100'>
        <Nav />
        <div 
          className="grid lg:grid-cols-5 grid-cols-1 pt-[64px]"
        >

          <SideNav className={'lg:col-span-1 lg:flex hidden mt-[50px] justify-center'}  />
          <div className=" col-span-4 py-2 lg:pr-8">
            <div className="bg-white rounded-md min-h-full shadow-lg">
              <Outlet />
            </div>
          </div>

        </div>
    </div>
  )
}

export default Layout
