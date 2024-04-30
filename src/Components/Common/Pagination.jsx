import React from 'react'

const Pagination = ({handlePage, totalPages, page}) => {
    page = parseInt(page)
  return (
    <div className="inline-flex items-center justify-center gap-3">
        {
            page > 1?
            <div
                onClick={()=>{
                    handlePage(page-1)
                    window.scroll({top: 0, left: 0, behavior: 'smooth' })
                }}
                className="cursor-pointer inline-flex size-8 items-center justify-center rounded border border-gray-100 bg-white text-gray-900 rtl:rotate-180"
            >
                <span className="sr-only">Next Page</span>
                <svg xmlns="http://www.w3.org/2000/svg" className="h-3 w-3" viewBox="0 0 20 20" fill="currentColor">
                <path
                    fillRule="evenodd"
                    d="M12.707 5.293a1 1 0 010 1.414L9.414 10l3.293 3.293a1 1 0 01-1.414 1.414l-4-4a1 1 0 010-1.414l4-4a1 1 0 011.414 0z"
                    clipRule="evenodd"
                />
                </svg>
            </div>
            :
            null
        }

        <p className="text-xs text-gray-900">
            {page}
            <span className="mx-0.25">/</span>
            {totalPages}
        </p>
        {
            page < totalPages?
        <div
            onClick={()=>{
                handlePage(page+1)
                window.scroll({top: 0, left: 0, behavior: 'smooth' })
            }}
            className="cursor-pointer inline-flex size-8 items-center justify-center rounded border border-gray-100 bg-white text-gray-900 rtl:rotate-180"
        >
            <span className="sr-only">Next Page</span>
            <svg xmlns="http://www.w3.org/2000/svg" className="h-3 w-3" viewBox="0 0 20 20" fill="currentColor">
            <path
                fillRule="evenodd"
                d="M7.293 14.707a1 1 0 010-1.414L10.586 10 7.293 6.707a1 1 0 011.414-1.414l4 4a1 1 0 010 1.414l-4 4a1 1 0 01-1.414 0z"
                clipRule="evenodd"
            />
            </svg>
        </div>
        :
        null
        }
    </div>
  )
}

export default Pagination