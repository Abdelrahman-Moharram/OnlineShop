import { useEffect } from 'react'
import { useSelector } from 'react-redux'
import { useNavigate } from 'react-router-dom'
import { toast } from 'react-toastify'

const IsAdmnin = ({children}) => {

    const nav = useNavigate()
    const {isAuthenticated, user} = useSelector(state=>state.auth)
    const roles = user?.roles?.split(', ')
    useEffect(()=>{
        if(!isAuthenticated){
            toast.error("Un-Authenticated, please login first")
            nav('/auth/login')
        }

        if(!roles.includes('admin')){
            console.log("HERE");
            toast.error('you trying to access Admin route with Normal account ')
            nav('/')
        }
    },[isAuthenticated])
    return (
        children
    )
}

export default IsAdmnin