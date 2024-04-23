import React from 'react'

const SummaryCollection = () => {
  return (
    <summary
              className="flex cursor-pointer items-center gap-2 border-b border-gray-400 pb-1 text-gray-900 transition hover:border-gray-600"
            >
        <span className="text-sm font-medium"> Availability </span>

        <span className="transition group-open:-rotate-180">
        <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            strokeWidth="1.5"
            stroke="currentColor"
            className="h-4 w-4"
        >
            <path
            strokeLinecap="round"
            strokeLinejoin="round"
            d="M19.5 8.25l-7.5 7.5-7.5-7.5"
            />
        </svg>
        </span>
    </summary>
  )
}

export default SummaryCollection