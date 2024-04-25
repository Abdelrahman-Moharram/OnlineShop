import React from 'react'
import ScrollableCollections from '../../Components/Collections/ScrollableCollections'
import Hero from '../../Components/Sections/Hero'
import { useHomePageQuery } from '../../redux/features/Products/ProductApiSlice'
import ScrollableProductsList from '../../Components/Lists/ProductsList/ScrollableProductsList'

const Index = () => {
  const {data, isLoading} = useHomePageQuery()
  return (
    <div className="">
      {
        data?
        <>
          <Hero isLoading={isLoading} images={data?.bannerImages} />
          {
            data?.productSections?.map((section, idx)=>(
              <ScrollableCollections title={section?.title} key={idx}>
                <ScrollableProductsList data={section?.productsList} />
              </ScrollableCollections>
            ))
          }
        </>
        :null
      }


    </div>
  )
}

export default Index