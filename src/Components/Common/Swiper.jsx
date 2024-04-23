import React from 'react'

import { Swiper, SwiperSlide } from 'swiper/react';

// Import Swiper styles
import 'swiper/css';
import 'swiper/css/pagination';
import 'swiper/css/navigation';


// import required modules
import { Pagination, Navigation } from 'swiper/modules';


const DefaultSwiper = ({images}) => {
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
          images?
            images.map(image=>(
              <SwiperSlide key={image}>
                <img 
                className='inset-0 w-full  object-cover'
                  src={image.includes('https')?  image :import.meta.env.VITE_BASE_URL+image}
                />
              </SwiperSlide>
            ))
          :null
        }
        
      </Swiper>


  )
}

export default DefaultSwiper