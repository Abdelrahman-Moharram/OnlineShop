import React from 'react'

import { Swiper, SwiperSlide } from 'swiper/react';

// Import Swiper styles
import 'swiper/css';
import 'swiper/css/effect-flip';
import 'swiper/css/pagination';
import 'swiper/css/navigation';

// import './styles.css';
// import required modules
import { EffectFlip, Pagination, Navigation } from 'swiper/modules';

const ProductImageSwipper = ({isLoading, images}) => {
    return (
        <div className='w-[440px] h-auto'>
            <Swiper
                effect={'flip'}
                grabCursor={true}
                pagination={true}
                loop={true}
                navigation={true}
                modules={[EffectFlip, Pagination, Navigation]}
                className="mySwiper"
            >
                {
                    images?.map((image, idx)=>(
                    <SwiperSlide key={idx}>
                        <img 
                            src={image.includes('https')?  image :import.meta.env.VITE_BASE_URL+image}
                        />
                    </SwiperSlide>
                    ))
                }
            </Swiper>
        </div>
    
    
      )
}

export default ProductImageSwipper