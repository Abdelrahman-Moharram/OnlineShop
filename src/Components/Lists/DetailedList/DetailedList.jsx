import React from 'react'
import DetailedListItem from './DetailedListItem'

const DetailedList = (data) => {
  return (
    <div className="flow-root">
        <dl className="-my-3 divide-y divide-gray-100 text-sm">
            {
              data && data.length > 0?
                data.map(item=>(
                  <DetailedListItem key={item} value={data[item]} />
                ))
              :null 
            }
        </dl>
    </div>
  )
}

export default DetailedList