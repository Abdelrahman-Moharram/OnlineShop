import React from 'react'
import 'react-toastify/dist/ReactToastify.css'
import { ToastContainer } from 'react-toastify';

import './index.css'
import { RouterProvider, createBrowserRouter } from 'react-router-dom'
import Layout from './Pages/Shared/Layout.jsx'
import Index from './Pages/Home/Index.jsx'
import { createRoot } from "react-dom/client";
import Login from './Pages/Auth/Login.jsx'
import Register from './Pages/Auth/Register.jsx'
import { Provider } from 'react-redux'
import { store } from './redux/app/store.js'
import Roles from './Pages/Auth/Roles.jsx'
import NotFound from './Pages/NotFound.jsx'
import IsAuthenticated from './Pages/ProtectedRoutes/IsAuthenticated.jsx'
import IsAdmin from './Pages/ProtectedRoutes/IsAdmin.jsx'
import Logout from './Pages/Auth/Logout.jsx';

import ProductDetails from './Pages/Products/ProductDetails.jsx';
import ProductsList from './Pages/Home/ProductsList.jsx';
import Cart from './Pages/Home/Cart.jsx';

const router = createBrowserRouter ([
  {
    path:"/",
    element:<Layout />,
    // errorElement:<NotFound />,
    children:[
      {
        index:true,
        element:<Index />
      },
      {
        path:'/cart',
        element:<Cart />
      },
      {
        path:"/Products",
        children:[
          {
            index:true,
            element:<ProductsList />
          },
          {
            path:":id",
            element:<ProductDetails />
          }
        ]
      },
      {
        path:"/admin/roles",
        element:
        <IsAdmin>
          <Roles />
        </IsAdmin>
      }
    ]
  },
  {
    path:"auth",
    children:[
      {
        path:"login",
        element:<Login />
      },
      {
        path:"register",
        element:<Register />
      },
      {
        path:"logout",
        element:<Logout />
      }
    ],

  }
])
createRoot(document.getElementById("root")).render(
  <Provider  store={store}>
    <RouterProvider router={router} />
    <ToastContainer />
  </Provider>
);
