import {Injectable} from '@angular/core';
import {Observable, of} from 'rxjs';
import {OfferModel} from './models/offer.model';

@Injectable({
  providedIn: 'root'
})
export class MarketplaceApiService {

  private readonly marketplaceApUrl = '';
  offers:OfferModel[] = [{
    title: 'Puff Pera Lona Impermeable Naranja',
    imgURL:'../../../assets/puff.jpg',
    username:'JCUrquijo',
    date: new Date(),
    description:'Tamaño Grande Fácil Limpiezqa Comfort. Elegancia. Estilo Novedoso.',
    location: 'New York',
    category:'Mueble'
  },
  {
    title: 'Sofacama Amoblando George Con Brazos Café',
    imgURL:'../../../assets/sofa.jpg',
    username:'JCUrquijo',
    date: new Date(),
    description:'Medida sentado (Ancho 180cm) (Largo 100cm) (Alto 80cm) (apróx). Medida acostado: (Ancho 180cm) (Largo 1|0cm) (Alto 40cm) (apróx). Sofacama de tres posiciones en su espaldar. Entrega a convenir',
    location: 'New York',
    category:'Mueble'
  },
  {
    title: 'Computador Apple II vintage',
    imgURL:'../../../assets/apple2.jpg',
    username:'JCUrquijo',
    date: new Date(),
    description:'Bla bla bla ...',
    location: 'New York',
    category:'electrónica'
  },
  {
    title: 'Carpooling',
    imgURL:'../../../assets/carpooling.jpg',
    username:'JCUrquijo',
    date: new Date(),
    description:'Bla bla bla ...',
    location: 'New York',
    category:'Transporte'
  },
  {
    title: 'Iphone Galaxy A02',
    imgURL:'../../../assets/iphone.jpg',
    username:'JCUrquijo',
    date: new Date(),
    description:'Bla bla bla ...',
    location: 'New York',
    category:'Electrónica'
  },
  {
    title: 'Linterna de bolsillo',
    imgURL:'../../../assets/lantern.jpg',
    username:'JCUrquijo',
    date: new Date(),
    description:'Bla bla bla ...',
    location: 'New York',
    category:'Electrónica'
  },
  {
    title: 'Casa veraniega',
    imgURL:'../../../assets/summer-house.jpg',
    username:'JCUrquijo',
    date: new Date(),
    description:'Bla bla bla ...',
    location: 'New York',
    category:'Inmueble'
  }
]

  constructor() { }

  getOffers(page: number, pageSize: number): Observable<OfferModel[]> {

    return of(this.offers.slice((page-1)*pageSize,(page)*pageSize))
  }

  postOffer(): Observable<string> {
    // TODO: implement the logic to post a new offer, also validate whatever you consider before posting
    return of('');
  }

  getCategories(): Observable<string[]> {
    // TODO: implement the logic to retrieve the categories from the service
    return of([]);
  }
}
