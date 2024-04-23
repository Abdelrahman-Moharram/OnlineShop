import React from 'react'
import UserNavDropDown from './UserNavDropDown'
import { Link } from 'react-router-dom'

const NavAuthOptions = ({isAuthenticated, user}) => {
  return (
    <div>
        {
            isAuthenticated?
                <UserNavDropDown user={user} />
            :
                <div className="sm:flex sm:gap-4">
                    <Link
                        className="rounded-md bg-teal-600 px-5 py-2.5 text-sm font-medium text-white shadow"
                        to='/auth/login'
                    >
                        Login
                    </Link>

                    <div className="hidden sm:flex">
                        <Link
                            className="rounded-md bg-gray-100 px-5 py-2.5 text-sm font-medium text-teal-600"
                            to='/auth/register'
                        >
                            Register
                        </Link>
                    </div>
                </div>
        }
    </div>
    
  )
}

export default NavAuthOptions