import React, { useEffect } from 'react'
import { useLogoutQuery } from '../../redux/features/Auth/authApiSlice'
import { setLogout } from '../../redux/features/Auth/authSlice'
import { useNavigate } from 'react-router-dom'
import Cookies from 'js-cookie'
import { useDispatch } from 'react-redux'
const Logout = () => {
    const dispatch = useDispatch()
    const nav = useNavigate()
    const {isSuccess} = useLogoutQuery()

    useEffect(()=>{
        dispatch(setLogout())
        Cookies.remove('access_token')
        Cookies.remove('refresh_token')
        nav('/auth/login')
    },[])
    
    try{
    }catch(err){
        console.log(err);
        nav('/')

    }
    
    return (
        <div>Logout</div>
    )
}

export default Logout