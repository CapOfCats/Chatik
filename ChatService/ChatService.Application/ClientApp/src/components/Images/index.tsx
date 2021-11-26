import React, { useCallback, useState } from 'react'
import css from './styles.module.sass'
import { IAttachment } from '../../types'
import Gallery from 'react-photo-gallery'
import Lightbox from 'react-image-lightbox'

interface IProps {
  images: IAttachment[]
}

const imageWidth = 100

export default function Images({ images }: IProps) {
  const [currentImage, setCurrentImage] = useState(0)
  const [viewerIsOpen, setViewerIsOpen] = useState(false)

  const openLightbox = useCallback((index) => {
    setCurrentImage(index)
    setViewerIsOpen(true)
  }, [])

  const closeLightbox = useCallback(() => {
    setCurrentImage(0)
    setViewerIsOpen(false)
  }, [])

  return (
    <div
      className={css.images}
      style={{ width: images.reduce((sum, item) => sum + item.width / item.height, 0) * 100 + (images.length - 1) * 5 }}
    >
      <Gallery
        photos={images.map(item => ({
          src: item.src,
          width: 1,
          height: 1
        }))}
        renderImage={({ photo, index }) => {
          return (
            <img
              key={index}
              src={photo.src}
              onClick={e => openLightbox(index)}
              className={css.thumbnail}
              style={{ width: `${100 / (images[index].width / images[index].height)}` }}
            />
          )
        }}
      />
      {viewerIsOpen && (
        <Lightbox
          mainSrc={images[currentImage].src}
          nextSrc={images[(currentImage + 1) % images.length].src}
          prevSrc={images[(currentImage + images.length - 1) % images.length].src}
          onCloseRequest={closeLightbox}
          onMovePrevRequest={() =>
            setCurrentImage((currentImage + images.length - 1) % images.length)
          }
          onMoveNextRequest={() =>
            setCurrentImage((currentImage + 1) % images.length)
          }
        />
      )}
    </div>
  )
}