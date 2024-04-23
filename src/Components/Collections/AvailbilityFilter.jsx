import React from 'react'
import SummaryCollection from './Summary.Collection'

const AvailbilityFilter = () => {
  return (
    <div
    className="z-50 group-open:absolute group-open:top-auto group-open:mt-2 ltr:group-open:start-0"
    >
        <div className="w-96 rounded border border-gray-200 bg-white">
        <header className="flex items-center justify-between p-4">
            <span className="text-sm text-gray-700"> 0 Selected </span>

            <button type="button" className="text-sm text-gray-900 underline underline-offset-4">
            Reset
            </button>
        </header>

        <ul className="space-y-1 border-t border-gray-200 p-4">
            <li>
            <label htmlFor="FilterInStock" className="inline-flex items-center gap-2">
                <input
                type="checkbox"
                id="FilterInStock"
                className="size-5 rounded border-gray-300"
                />

                <span className="text-sm font-medium text-gray-700"> In Stock (5+) </span>
            </label>
            </li>

            <li>
            <label htmlFor="FilterPreOrder" className="inline-flex items-center gap-2">
                <input
                type="checkbox"
                id="FilterPreOrder"
                className="size-5 rounded border-gray-300"
                />

                <span className="text-sm font-medium text-gray-700"> Pre Order (3+) </span>
            </label>
            </li>

            <li>
            <label htmlFor="FilterOutOfStock" className="inline-flex items-center gap-2">
                <input
                type="checkbox"
                id="FilterOutOfStock"
                className="size-5 rounded border-gray-300"
                />

                <span className="text-sm font-medium text-gray-700"> Out of Stock (10+) </span>
            </label>
            </li>
        </ul>
        </div>
    </div>
  )
}

export default AvailbilityFilter