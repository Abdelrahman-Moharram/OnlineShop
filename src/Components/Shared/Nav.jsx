import React from 'react'
import { Link, NavLink } from 'react-router-dom'
import { FaHome } from "react-icons/fa";
import { useSelector } from 'react-redux';
import NavAuthOptions from '../Common/NavAuthOptions';

const Nav = () => {
    const {isAuthenticated, user} = useSelector(state=>state.auth)
  return (
    <header className="bg-white fixed w-full z-10 shadow-md">
        <div className="mx-auto px-4 sm:px-6 lg:px-8">
            <div className="flex h-16 items-center justify-between">
            <div className="md:flex md:items-center md:gap-12">
                <Link className="block text-teal-600" to={'/'}>
                    <span className="sr-only">Home</span>
                    <FaHome className='text-4xl' />
                </Link>
            </div>

            <div className="hidden md:block">
                <nav aria-label="Global">
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
                </nav>
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