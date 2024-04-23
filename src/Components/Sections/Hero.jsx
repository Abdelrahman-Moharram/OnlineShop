import React from 'react'
import DefaultSwiper from '../Common/Swiper'

const Hero = ({isLoading, images}) => {
  return (
    <div className="relative bg-hero-gradiant ">
        <DefaultSwiper images={images} />
    </div>
  )
}

export default Hero
