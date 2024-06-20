import React from 'react'
import { useGetUserCartQuery } from '../../redux/features/Products/cartApiSlice'

const Cart = () => {
    const {data} = useGetUserCartQuery()
    const handleCheckout = () =>{
        
    }
  return (
    <div className='min-h-[calc(100vh-80px)]'>
        <section>
        <div className="mx-auto max-w-screen-xl px-4 py-8 sm:px-6 sm:py-12 lg:px-8">
            <div className="mx-auto max-w-3xl">
            <header className="text-center">
                <h1 className="text-xl font-bold text-gray-900 sm:text-3xl">Your Cart</h1>
            </header>

            <div className="mt-8">
                <ul className="space-y-4">
                    {
                        data?.cartItems?.map((item, idx)=>(
                        <li className="flex items-center gap-4" key={idx}>
                            <img
                                src={item.image}
                                alt={item.productName}
                                className="size-16 rounded object-cover"
                            />

                            <div>
                            <h3 className="text-sm text-gray-900 text-ellipsis">{item.productName}</h3>

                            <dl className="mt-0.5 space-y-px text-[10px] text-gray-600">
                                <div>
                                <dt className="inline">price:</dt>
                                <dd className="inline">{item.price}</dd>
                                </div>
                            </dl>
                            </div>

                            <div className="flex flex-1 items-center justify-end gap-2">
                            <form>
                                <span htmlFor="Line1Qty" className="sr-only"> Quantity </span>

                                <div
                                    id="Line1Qty"
                                    className="px-7 py-2 rounded border-gray-200 bg-gray-50 p-0 text-center text-xs text-gray-600 [-moz-appearance:_textfield] focus:outline-none [&::-webkit-inner-spin-button]:m-0 [&::-webkit-inner-spin-button]:appearance-none [&::-webkit-outer-spin-button]:m-0 [&::-webkit-outer-spin-button]:appearance-none"
                                >{item.quantity}</div>
                            </form>

                            <button className="text-gray-600 transition hover:text-red-600">
                                <span className="sr-only">Remove item</span>

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
                                    d="M14.74 9l-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 01-2.244 2.077H8.084a2.25 2.25 0 01-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 00-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 013.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 00-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 00-7.5 0"
                                />
                                </svg>
                            </button>
                            </div>
                        </li>
                        ))
                    }
                </ul>

                <div className="mt-8 flex justify-end border-t border-gray-100 pt-8">
                <div className="w-screen max-w-lg space-y-4">
                    <dl className="space-y-0.5 text-sm text-gray-700">
                    <div className="flex justify-between">
                        <dt>Subtotal</dt>
                        <dd>{data?.totalPrice}</dd>
                    </div>

                    <div className="flex justify-between">
                        <dt>VAT</dt>
                        <dd>£25</dd>
                    </div>

                    <div className="flex justify-between">
                        <dt>Discount</dt>
                        <dd>-£25</dd>
                    </div>

                    <div className="flex justify-between !text-base font-medium">
                        <dt>Total</dt>
                        <dd>{data?.totalPrice}</dd>
                    </div>
                    </dl>

                    <div className="flex justify-end">
                    <span
                        className="inline-flex items-center justify-center rounded-full bg-indigo-100 px-2.5 py-0.5 text-indigo-700"
                    >
                        <svg
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 24 24"
                        strokeWidth="1.5"
                        stroke="currentColor"
                        className="-ms-1 me-1.5 h-4 w-4"
                        >
                        <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            d="M16.5 6v.75m0 3v.75m0 3v.75m0 3V18m-9-5.25h5.25M7.5 15h3M3.375 5.25c-.621 0-1.125.504-1.125 1.125v3.026a2.999 2.999 0 010 5.198v3.026c0 .621.504 1.125 1.125 1.125h17.25c.621 0 1.125-.504 1.125-1.125v-3.026a2.999 2.999 0 010-5.198V6.375c0-.621-.504-1.125-1.125-1.125H3.375z"
                        />
                        </svg>

                        <p className="whitespace-nowrap text-xs">2 Discounts Applied</p>
                    </span>
                    </div>

                    <div className="flex justify-end">
                    <button
                        onClick={handleCheckout}
                        className="block rounded bg-gray-700 px-5 py-3 text-sm text-gray-100 transition hover:bg-gray-600"
                    >
                        Checkout
                    </button>
                    </div>
                </div>
                </div>
            </div>
            </div>
        </div>
        </section>
    </div>
  )
}

export default Cart
