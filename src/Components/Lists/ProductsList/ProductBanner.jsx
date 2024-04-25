import React from 'react'
import ImageSkeleton from '../../Skeletons/ImageSkeleton'
import ProductImageSwipper from './ProductImageSwipper'

const ProductBanner = ({images, isLoading}) => {
  return (
    <div className=' '>
        {
        images?
            <ProductImageSwipper isLoading={isLoading} images={images}  />
            
            :
            <ImageSkeleton width={400} height={225} />

        }
    </div>
  )
}

export default ProductBanner
