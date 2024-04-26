import React from 'react'

import { Swiper, SwiperSlide } from 'swiper/react';

// Import Swiper styles
import 'swiper/css';
import 'swiper/css/pagination';
import 'swiper/css/navigation';


// import required modules
import { Pagination, Navigation } from 'swiper/modules';

import ProductCard from '../../Cards/ProductCard';
import Divider from '../../Common/Divider';

const ScrollableProductsList = ({data, title}) => {
  return (
    <>
      <Divider title={title} />
      {
            data && data.length > 0
            ?
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
           data.map((prod)=>(
              <SwiperSlide key={prod?.id}>
                <ProductCard product={prod}  />
              </SwiperSlide>
            ))
          }
            
          

          
      </Swiper>
      :null
    }
    </>


  )
}

export default ScrollableProductsList