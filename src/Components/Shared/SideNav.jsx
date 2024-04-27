import { Link } from "react-router-dom";
import { useTopBrandsQuery, useTopCategoriesQuery } from "../../redux/features/Products/ProductApiSlice";
import { useState } from "react";
import Spinner from "../Common/Spinner";

export default function SideNav({className}) {
    const [categoryToggler, setCategoryToggler] = useState(false)
    const [brandToggler, setBrandToggler] = useState(false)
    const {data: Categories} = useTopCategoriesQuery({},{skip:!categoryToggler})
    const {data: Brands} = useTopBrandsQuery({},{skip:!categoryToggler})
    
    return (
        <div className={className}>
            <div className="flex flex-col justify-between shadow-xl rounded-lg fixed  w-[15%] bg-white max-h-[80%] overflow-y-auto">
                <div className="px-3 py-6">
                    <ul className="">
                        <li>
                            <a
                                href="#"
                                className="block rounded-lg px-4 py-2 text-sm font-medium text-gray-500 hover:bg-gray-100 hover:text-gray-700"
                            >
                            General
                            </a>
                        </li>

                        <li>
                            <details className="group [&_summary::-webkit-details-marker]:hidden">
                            <summary
                                onClick={()=>setCategoryToggler(!categoryToggler)}
                                className="flex cursor-pointer items-center justify-between rounded-lg px-4 py-2 text-gray-500 hover:bg-gray-100 hover:text-gray-700"
                            >
                                <span className="text-sm font-medium"> Top Categories </span>

                                <span className="shrink-0 transition duration-300 group-open:-rotate-180">
                                <svg
                                    xmlns="http://www.w3.org/2000/svg"
                                    className="h-5 w-5"
                                    viewBox="0 0 20 20"
                                    fill="currentColor"
                                >
                                    <path
                                    fillRule="evenodd"
                                    d="M5.293 7.293a1 1 0 011.414 0L10 10.586l3.293-3.293a1 1 0 111.414 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 010-1.414z"
                                    clipRule="evenodd"
                                    />
                                </svg>
                                </span>
                            </summary>

                            <ul className="mt-2 space-y-1 px-4">
                                {
                                    Categories && Categories.length?
                                        Categories.map(cat=>(
                                            <li key={cat?.id}>
                                                <Link
                                                    to={"/categories/"+cat.id}
                                                    className="block rounded-lg px-4 py-2 text-sm font-medium text-gray-500 hover:bg-gray-100 hover:text-gray-700"
                                                >
                                                    <div className="flex justify-between">
                                                        <p>{cat.name}</p>
                                                        <div className="p-1 px-2 rounded-md bg-gray-300 text-black text-xs">{cat.size}</div>
                                                    </div>
                                                </Link>
                                            </li>
                                        ))
                                    :
                                    <Spinner />
                                }
                                
                            </ul>
                            </details>
                        </li>


                        <li>
                            <details className="group [&_summary::-webkit-details-marker]:hidden">
                            <summary
                                onClick={()=>setBrandToggler(!brandToggler)}
                                className="flex cursor-pointer items-center justify-between rounded-lg px-4 py-2 text-gray-500 hover:bg-gray-100 hover:text-gray-700"
                            >
                                <span className="text-sm font-medium"> Top Brands </span>

                                <span className="shrink-0 transition duration-300 group-open:-rotate-180">
                                <svg
                                    xmlns="http://www.w3.org/2000/svg"
                                    className="h-5 w-5"
                                    viewBox="0 0 20 20"
                                    fill="currentColor"
                                >
                                    <path
                                    fillRule="evenodd"
                                    d="M5.293 7.293a1 1 0 011.414 0L10 10.586l3.293-3.293a1 1 0 111.414 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 010-1.414z"
                                    clipRule="evenodd"
                                    />
                                </svg>
                                </span>
                            </summary>

                            <ul className="mt-2 space-y-1 px-4">
                                {
                                    Brands && Brands.length?
                                    Brands.map(brand=>(
                                        <li key={brand?.id}>
                                            <Link
                                                to={"/categories/"+brand.id}
                                                className="block rounded-lg px-4 py-2 text-sm font-medium text-gray-500 hover:bg-gray-100 hover:text-gray-700"
                                            >
                                                <div className="flex justify-between">
                                                    <p>{brand.name}</p>
                                                    <div className="p-1 px-2 rounded-md bg-gray-300 text-black text-xs">{brand.size}</div>
                                                </div>
                                            </Link>
                                        </li>
                                    ))
                                    :
                                    <Spinner />
                                }
                                
                            </ul>
                            </details>
                        </li>

                        

                        <li>
                            <a
                            href="#"
                            className="block rounded-lg px-4 py-2 text-sm font-medium text-gray-500 hover:bg-gray-100 hover:text-gray-700"
                            >
                            Invoices
                            </a>
                        </li>

                        <li>
                            <details className="group [&_summary::-webkit-details-marker]:hidden">
                            <summary
                                className="flex cursor-pointer items-center justify-between rounded-lg px-4 py-2 text-gray-500 hover:bg-gray-100 hover:text-gray-700"
                            >
                                <span className="text-sm font-medium"> Account </span>

                                <span className="shrink-0 transition duration-300 group-open:-rotate-180">
                                <svg
                                    xmlns="http://www.w3.org/2000/svg"
                                    className="h-5 w-5"
                                    viewBox="0 0 20 20"
                                    fill="currentColor"
                                >
                                    <path
                                    fillRule="evenodd"
                                    d="M5.293 7.293a1 1 0 011.414 0L10 10.586l3.293-3.293a1 1 0 111.414 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 010-1.414z"
                                    clipRule="evenodd"
                                    />
                                </svg>
                                </span>
                            </summary>

                            <ul className="mt-2 space-y-1 px-4">
                                <li>
                                <a
                                    href="#"
                                    className="block rounded-lg px-4 py-2 text-sm font-medium text-gray-500 hover:bg-gray-100 hover:text-gray-700"
                                >
                                    Details
                                </a>
                                </li>

                                <li>
                                <a
                                    href="#"
                                    className="block rounded-lg px-4 py-2 text-sm font-medium text-gray-500 hover:bg-gray-100 hover:text-gray-700"
                                >
                                    Security
                                </a>
                                </li>

                                <li>
                                <form action="#">
                                    <button
                                    type="submit"
                                    className="w-full rounded-lg px-4 py-2 text-sm font-medium text-gray-500 [text-align:_inherit] hover:bg-gray-100 hover:text-gray-700"
                                    >
                                    Logout
                                    </button>
                                </form>
                                </li>
                            </ul>
                            </details>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    );
}