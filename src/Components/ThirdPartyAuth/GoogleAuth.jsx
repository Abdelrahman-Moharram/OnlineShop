import GoogleLogin from '@leecheuk/react-google-login';
import React from 'react';
import { useGoogleLoginMutation } from '../../redux/features/Auth/authApiSlice';
import { setAuth } from '../../redux/features/Auth/authSlice';
// import GoogleLogin from '@leecheuk/react-google-login';
// or
// import { GoogleLogin } from '@leecheuk/react-google-login';


const GoogleAuth = ()=>{
  const [googleLogin, {data, isError, isLoading}] = useGoogleLoginMutation()
  
  const responseGoogle = (response) => {
    console.log(data, response);
  }
    setAuth(data)
    console.log(data);
    return(
      <GoogleLogin
        clientId={import.meta.env.VITE_GOOGLE_CLIENT_ID}
        onSuccess={r=> {
          googleLogin(r.tokenId)
        }}
        onFailure={responseGoogle}
        cookiePolicy={'single_host_origin'}
      >
        
        <span> Login with Google</span>
    </GoogleLogin>
    )
}

export default GoogleAuth