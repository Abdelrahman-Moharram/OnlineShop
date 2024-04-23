import { useEffect } from 'react'
import { useSelector } from 'react-redux'
import { useNavigate } from 'react-router-dom'
import { toast } from 'react-toastify'

const IsAuthenticated = ({children}) => {
    const nav = useNavigate()
    const {isAuthenticated} = useSelector(state=>state.auth)
    useEffect(()=>{
        if(!isAuthenticated){
            toast.error("Un-Authenticated, please login first")
            nav('/auth/login')
        }
    },[isAuthenticated])
    return (
        children
    )
}

export default IsAuthenticated