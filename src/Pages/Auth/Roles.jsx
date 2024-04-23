import React from 'react'
import { useRolesListQuery } from '../../redux/features/Auth/roleApiSlice'

const Roles = () => {
    
    const {data, isLoading, isError, error} = useRolesListQuery()
    console.log("data, ",data,"\nerror", error);
    return (
    <div className='mt-5 px-5'>
      {
        data && data.length?
        data.map(item=>(
                <span 
                    key={item.id}
                    className="bg-blue-100 text-blue-800 text-sm font-medium me-2 px-3.5 py-1.5 rounded-full dark:bg-blue-900 dark:text-blue-300"
                >
                    {item.name}
                </span>
        ))
        :null
      }
    </div>
  )
}

export default Roles
