import React, { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { useRegisterMutation } from '../../redux/features/Auth/authApiSlice'
import { ImSpinner3 } from "react-icons/im";
import Cookies from 'js-cookie';
const Register = () => {
    const nav = useNavigate()
    const initialData = {
        'firstName':'',
        'lastName': '',
        "username": '',
        'email':'',
        'password': '',
        'confirmPassword':''
    }
    const [form, setForm] = useState(initialData)

    const handleForm = async (e) =>{
        e.preventDefault()
        try{
            const {data} =  await register(form)
            const token = data.token
            if(token){
                Cookies.set('access_token', String(token))
                dispatch(setAuth())
                setForm(initialData)
                nav('/')               
            }
        }
        catch(err){
            console.log(err);
        }
    }

    const [register, {isError, isLoading}] = useRegisterMutation()
  return (
    <div className="h-screen">


        <div className="mx-auto max-w-screen-xl px-4 py-16 sm:px-6 lg:px-8">
        <div className="mx-auto max-w-lg">
            <h1 className="text-center text-2xl font-bold text-indigo-600 sm:text-3xl">Get started today</h1>

            

            <form onSubmit={handleForm} className="mb-0 mt-6 space-y-4 rounded-lg p-4 shadow-lg sm:p-6 lg:p-8">

                <div className="grid grid-cols-2 gap-2">

                    <div>

                        <label
                        htmlFor="firstName"
                        className="relative block rounded-md border border-gray-200 shadow-sm focus-within:border-blue-600 focus-within:ring-1 focus-within:ring-blue-600"
                        >
                        <input
                            value={form.firstName}
                            onChange={e=>setForm(prev=>{
                                return {...prev, firstName:e.target.value}
                            })}
                            type="text"
                            id="firstName"
                            className="peer border-none bg-transparent placeholder-transparent text-md px-5 p-2 focus:border-transparent focus:outline-none focus:ring-0"
                            placeholder="First Name"
                        />

                        <span
                            className="pointer-events-none absolute start-2.5 top-0 -translate-y-1/2 bg-white p-0.5 text-xs text-gray-700 transition-all peer-placeholder-shown:top-1/2 peer-placeholder-shown:text-sm peer-focus:top-0 peer-focus:text-xs"
                        >
                            First Name
                        </span>
                        </label>
                    </div>

                    <div>

                        <label
                        htmlFor="lastName"
                        className="relative block rounded-md border border-gray-200 shadow-sm focus-within:border-blue-600 focus-within:ring-1 focus-within:ring-blue-600"
                        >
                        <input
                            value={form.lastName}
                            onChange={e=>setForm(prev=>{
                                return {...prev, lastName:e.target.value}
                            })}
                            type="text"
                            id="lastName"
                            className="peer border-none bg-transparent placeholder-transparent text-md px-5 p-2 focus:border-transparent focus:outline-none focus:ring-0"
                            placeholder="Last Name"
                        />

                        <span
                            className="pointer-events-none absolute start-2.5 top-0 -translate-y-1/2 bg-white p-0.5 text-xs text-gray-700 transition-all peer-placeholder-shown:top-1/2 peer-placeholder-shown:text-sm peer-focus:top-0 peer-focus:text-xs"
                        >
                            Last Name
                        </span>
                        </label>
                    </div>
                </div>


                <div>
                    <label
                        htmlFor="UserName"
                        className="relative block rounded-md border border-gray-200 shadow-sm focus-within:border-blue-600 focus-within:ring-1 focus-within:ring-blue-600"
                        >
                        <input
                            value={form.UserName}
                            onChange={e=>setForm(prev=>{
                                return {...prev, UserName:e.target.value}
                            })}
                            type="text"
                            id="UserName"
                            className="peer border-none bg-transparent placeholder-transparent text-md px-5 p-2 focus:border-transparent focus:outline-none focus:ring-0"
                            placeholder="Username"
                        />

                        <span
                            className="pointer-events-none absolute start-2.5 top-0 -translate-y-1/2 bg-white p-0.5 text-xs text-gray-700 transition-all peer-placeholder-shown:top-1/2 peer-placeholder-shown:text-sm peer-focus:top-0 peer-focus:text-xs"
                        >
                            Username
                        </span>
                    </label>
                </div>

                <div>
                    <label
                        htmlFor="Email"
                        className="relative block rounded-md border border-gray-200 shadow-sm focus-within:border-blue-600 focus-within:ring-1 focus-within:ring-blue-600"
                        >
                        <input
                            value={form.Email}
                            onChange={e=>setForm(prev=>{
                                return {...prev, Email:e.target.value}
                            })}
                            type="text"
                            id="Email"
                            className="peer border-none bg-transparent placeholder-transparent text-md px-5 p-2 focus:border-transparent focus:outline-none focus:ring-0"
                            placeholder="Email"
                        />

                        <span
                            className="pointer-events-none absolute start-2.5 top-0 -translate-y-1/2 bg-white p-0.5 text-xs text-gray-700 transition-all peer-placeholder-shown:top-1/2 peer-placeholder-shown:text-sm peer-focus:top-0 peer-focus:text-xs"
                        >
                            Email
                        </span>
                    </label>
                </div>

                <div>
                <label
                    htmlFor="password"
                    className="relative block rounded-md border border-gray-200 shadow-sm focus-within:border-blue-600 focus-within:ring-1 focus-within:ring-blue-600"
                    >
                    <input
                        value={form.password}
                        onChange={e=>setForm(prev=>{
                            return {...prev, password:e.target.value}
                        })}
                        type="password"
                        id="password"
                        className="peer border-none bg-transparent placeholder-transparent text-md px-5 p-2 focus:border-transparent focus:outline-none focus:ring-0"
                        placeholder="password"
                    />

                    <span
                        className="pointer-events-none absolute start-2.5 top-0 -translate-y-1/2 bg-white p-0.5 text-xs text-gray-700 transition-all peer-placeholder-shown:top-1/2 peer-placeholder-shown:text-sm peer-focus:top-0 peer-focus:text-xs"
                    >
                        Password
                    </span>
                </label>
                </div>

                <div>
                <label
                    htmlFor="confirmPassword"
                    className="relative block rounded-md border border-gray-200 shadow-sm focus-within:border-blue-600 focus-within:ring-1 focus-within:ring-blue-600"
                    >
                    <input
                        value={form.confirmPassword}
                        onChange={e=>setForm(prev=>{
                            return {...prev, confirmPassword:e.target.value}
                        })}
                        type="password"
                        id="confirmPassword"
                        className="peer border-none bg-transparent placeholder-transparent text-md px-5 p-2 focus:border-transparent focus:outline-none focus:ring-0"
                        placeholder="Retype Password"
                    />

                    <span
                        className="pointer-events-none absolute start-2.5 top-0 -translate-y-1/2 bg-white p-0.5 text-xs text-gray-700 transition-all peer-placeholder-shown:top-1/2 peer-placeholder-shown:text-sm peer-focus:top-0 peer-focus:text-xs"
                    >
                        Retype Password
                    </span>
                </label>
                </div>
                
            <button
                type="submit"
                className="block w-full rounded-lg bg-indigo-600 px-5 py-3 text-sm font-medium text-white"
            >
                {
                    isLoading?
                    <span className=''><ImSpinner3 className='animate-spin text-center mx-auto' /></span>
                    :
                    <span>Sign up</span>
                }
            </button>

            <p className="text-center text-sm text-gray-500">
                No account? 
                <Link className="underline" to="/auth/login">Sign in</Link>
            </p>
            </form>
        </div>
        </div>
    </div>
  )
}

export default Register