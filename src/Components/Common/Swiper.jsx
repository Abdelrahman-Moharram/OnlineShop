import React from 'react'

import { Swiper, SwiperSlide } from 'swiper/react';
import Logo from '../../../public/alpha-high-resolution-logo-white.png'

// Import Swiper styles
import 'swiper/css';
import 'swiper/css/pagination';
import 'swiper/css/navigation';


// import required modules
import { Pagination, Navigation } from 'swiper/modules';
import ImageSkeleton from '../Skeletons/ImageSkeleton';


const DefaultSwiper = ({isLoading, images}) => {
  return (
    <Swiper
        slidesPerView={1}
        spaceBetween={30}
        loop={true}
        pagination={{
            clickable: true,
        }}
        navigation={true}
        modules={[Pagination, Navigation]}
        className="mySwiper"        
      >
        {
          isLoading && ! images?.length?
          <ImageSkeleton width='100%' height="440px"  />
          :  
          <>
              <SwiperSlide>
                <img 
                  className='inset-0 w-full object-cover h-[740px]'
                  src={Logo}
                />
              </SwiperSlide>
          
                {
                  images.map(image=>(
                    <SwiperSlide key={image}>
                      <img 
                      className='inset-0 w-full object-cover'
                        src={image.includes('https')?  image :import.meta.env.VITE_BASE_URL+image}
                      />
                    </SwiperSlide>
                  ))
                }
          </>
          
          
        }
        
      </Swiper>


  )
}

export default DefaultSwiper