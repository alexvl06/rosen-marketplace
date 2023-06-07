import {faker} from '@faker-js/faker'
import {OfferModel} from './offer.model'
import { CategoryModel } from './category.model '

export const generateOneOffer = ():OfferModel=>{
  return{
    title:faker.commerce.productName(),
    pictureUrl: faker.image.url(),
    publishedOn: faker.date.recent(),
    location: faker.location.direction(),
    categoryName: faker.commerce.productAdjective(),
    username: faker.person.firstName(),
    description:faker.commerce.productDescription()
  }
}

export const generateManyOffers = (size = 10):OfferModel[]=>{
  const offers: OfferModel[] = []
  for(let index = 0; index <size; index++){
    offers.push(generateOneOffer())
  }
  return [...offers]
}

export const generateOneCategory = ():CategoryModel=>{
  return{
    id: faker.number.int(),
    name: faker.commerce.productAdjective()
  }
}

export const generateManyCategories = (size=10):CategoryModel[]=>{
  const categories: CategoryModel[] = []
  for(let index=0;index<size;index++){
    categories.push(generateOneCategory())
  }
  return [...categories]
}
