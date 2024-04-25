import React, { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'
import { useSelector } from 'react-redux';
import NavAuthOptions from '../Common/NavAuthOptions';
import Logo from '../../../public/alpha-high-resolution-logo-black-transparent.png'
import { useSearchQuery } from '../../redux/features/Products/ProductApiSlice';

const Nav = () => {
    const {isAuthenticated, user} = useSelector(state=>state.auth)
    const [searchQuery, setSearch] = useState('')
    const {isLoading, data} = useSearchQuery(searchQuery, {skip: !searchQuery, refetchOnMountOrArgChange:true})
  return (
    <header className="bg-white fixed w-full z-10 shadow-md">
        <div className="mx-auto px-4 sm:px-6 lg:px-8">
            <div className="flex h-16 items-center justify-between">
            <div className="md:flex md:items-center md:gap-12">
                <Link className="block text-teal-600" to={'/'}>
                    <span className="sr-only">Home</span>
                    <img src={Logo} alt="logo" width={"60px"} srcSet="" />
                </Link>
            </div>

            <div className="hidden md:block">
                {/* <nav aria-label="Global">
                    <ul className="flex items-center gap-6 text-sm">
                        <li>
                        <NavLink className="text-gray-500 transition hover:text-gray-500/75" to="About"> About </NavLink>
                        </li>

                        <li>
                        <NavLink className="text-gray-500 transition hover:text-gray-500/75" to="Careers"> Careers </NavLink>
                        </li>

                        <li>
                        <NavLink className="text-gray-500 transition hover:text-gray-500/75" to="History"> History </NavLink>
                        </li>

                        <li>
                        <NavLink className="text-gray-500 transition hover:text-gray-500/75" to="Services"> Services </NavLink>
                        </li>

                        <li>
                        <NavLink className="text-gray-500 transition hover:text-gray-500/75" to="Projects"> Projects </NavLink>
                        </li>

                        <li>
                        <NavLink className="text-gray-500 transition hover:text-gray-500/75" to="Blog"> Blog </NavLink>
                        </li>
                    </ul>
                </nav> */}
                
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
                    searchQuery && data && data.length?
                        <div className='absolute left-20 right-20 top-[70px] px-2 bg-white shadow-lg rounded-md'>
                            {
                                data.map(prod=>(
                                <div className='px-4 py-2' key={prod?.id}>
                                    <Link to={"/"}  className="flex justify-evenly items-center hover:bg-gray-100 rounded-lg">
                                        <div className="col-span-1">
                                        {
                                            prod?.image && prod.image.length?
                                                <img src={prod?.image[0]} alt="logo" width={"60px"} />
                                            :
                                                <img src={Logo} alt="logo" width={"60px"} srcSet="" />

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

            <div className="flex items-center gap-4">
                <NavAuthOptions isAuthenticated={isAuthenticated} user={user} />

                <div className="block md:hidden">
                <button className="rounded bg-gray-100 p-2 text-gray-600 transition hover:text-gray-600/75">
                    <svg
                    xmlns="http://www.w3.org/2000/svg"
                    className="h-5 w-5"
                    fill="none"
                    viewBox="0 0 24 24"
                    stroke="currentColor"
                    strokeWidth="2"
                    >
                    <path strokeLinecap="round" strokeLinejoin="round" d="M4 6h16M4 12h16M4 18h16" />
                    </svg>
                </button>
                </div>
            </div>
            </div>
        </div>
    </header>
  )
}

export default Nav