export interface OfferModel {
  title:string
  imgURL:string
  date: Date
  location:string
  category:Category
  username:string
  description:string
}

export type Category = "I'm looking for"|"Product"|"Service"

