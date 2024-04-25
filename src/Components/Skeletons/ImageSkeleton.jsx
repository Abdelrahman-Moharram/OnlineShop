import React from 'react'

const ImageSkeleton = ({width, height}) => {
    console.log(width, height);
    return (
      <div 
          className={"bg-slate-400 animate-pulse"}
          style={{
            width,
            height
          }}
          >
  
      </div>
    )
  }
export default ImageSkeleton
