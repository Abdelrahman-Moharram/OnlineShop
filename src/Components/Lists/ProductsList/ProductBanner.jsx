import React from 'react'
import ImageSkeleton from '../../Skeletons/ImageSkeleton'

const ProductBanner = ({images}) => {
  return (
    <div className=' '>
        {
        images?
            <img 
                src={images[0].includes('https')?  images[0] :import.meta.env.VITE_BASE_URL+images[0]}
                alt='banner'
                sizes="100vw"
                style={{ width: '400px', height: 'auto' }}
            />
            
            :
            <ImageSkeleton width={400} height={225} />

        }
    </div>
  )
}

export default ProductBanner
