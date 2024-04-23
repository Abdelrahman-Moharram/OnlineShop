import React, { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { useLoginMutation } from '../../redux/features/Auth/authApiSlice'
import { ImSpinner3 } from 'react-icons/im'
import Cookies from 'js-cookie'
import { setAuth } from '../../redux/features/Auth/authSlice'
import { useDispatch } from 'react-redux'
const Login = () => {
    const nav = useNavigate()

    const initialData = {
        "username": '',
        'password': ''
    }
    const [form, setForm] = useState(initialData)
    const [login, {isLoading}] = useLoginMutation()
    const dispatch = useDispatch()
    const handleForm = async (e) =>{
        e.preventDefault()
        try{
            const {data} =  await login(form)
            if(!isLoading){
                const token = data?.token
                if(token){
                    Cookies.set('access_token', String(token))
                    dispatch(setAuth())
                    setForm(initialData)
                    nav('/')
                }
            }
        }
        catch(err){
            console.log(err);
        }
    }

  return (
    <div className="h-screen">


        <div className="mx-auto max-w-screen-xl px-4 py-16 sm:px-6 lg:px-8">
        <div className="mx-auto max-w-lg">
            <h1 className="text-center text-2xl font-bold text-indigo-600 sm:text-3xl">Get started today</h1>

            <p className="mx-auto mt-4 max-w-md text-center text-gray-500">
            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Obcaecati sunt dolores deleniti
            inventore quaerat mollitia?
            </p>

            <form onSubmit={handleForm} className="mb-0 mt-6 space-y-4 rounded-lg p-4 shadow-lg sm:p-6 lg:p-8">

            <div>
                <label htmlFor="email" className="sr-only">Email</label>

                <label
                htmlFor="Username"
                className="relative block rounded-md border border-gray-200 shadow-sm focus-within:border-blue-600 focus-within:ring-1 focus-within:ring-blue-600"
                >
                <input
                    onChange={e=>setForm(prev=>{
                        return {...prev, username: e.target.value}    
                    })}
                    type="text"
                    value={form.username}
                    id="Username"
                    className="peer border-none bg-transparent placeholder-transparent text-md px-5 p-2 focus:border-transparent focus:outline-none focus:ring-0"
                    placeholder="Username"
                />

                <span
                    className="pointer-events-none absolute start-2.5 top-0 -translate-y-1/2 bg-white p-0.5 text-xs text-gray-700 transition-all peer-placeholder-shown:top-1/2 peer-placeholder-shown:text-sm peer-focus:top-0 peer-focus:text-xs"
                >
                    Username or Email
                </span>
                </label>
            </div>

            <div>
            <label
                htmlFor="password"
                className="relative block rounded-md border border-gray-200 shadow-sm focus-within:border-blue-600 focus-within:ring-1 focus-within:ring-blue-600"
                >
                <input
                    type="password"
                    value={form.password}
                    onChange={e=>setForm(prev=>{
                        return {...prev, password: e.target.value}    
                    })}
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

            <button
                type="submit"
                className="block w-full rounded-lg bg-indigo-600 px-5 py-3 text-sm font-medium text-white"
            >
                {
                    isLoading?
                    <span className=''><ImSpinner3 className='animate-spin text-center mx-auto' /></span>
                    :
                    <span>Sign in</span>
                }
            </button>

            <p className="text-center text-sm text-gray-500">
                No account?
                <Link className="underline" to="/auth/register">Sign up</Link>
            </p>
            </form>
        </div>
        </div>
    </div>
  )
}

export default Login