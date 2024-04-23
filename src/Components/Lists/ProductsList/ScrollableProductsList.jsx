import React from 'react'

import { Swiper, SwiperSlide } from 'swiper/react';

// Import Swiper styles
import 'swiper/css';
import 'swiper/css/pagination';
import 'swiper/css/navigation';


// import required modules
import { Pagination, Navigation } from 'swiper/modules';

import ProductCard from '../../Cards/ProductCard';

const ScrollableProductsList = ({data}) => {
  return (
    <Swiper
        slidesPerView={3.5}
        breakpoints={{
              480: {
                slidesPerView: 1,
                spaceBetween: 30
              },
            
              640: {
                slidesPerView: 2,
                spaceBetween: 40
              },
              1024:{
                slidesPerView: 3,
                spaceBetween: 40
              }
              ,
              1280:{
                slidesPerView: 4,
                spaceBetween: 40
              }
              
        }}
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
          data && data.length > 0
          ?
          data.map((prod, idx)=>(
            <SwiperSlide key={idx}>
              <ProductCard product={prod}  />
            </SwiperSlide>
          ))
          
        :null
        }

        
      </Swiper>


  )
}

export default ScrollableProductsList