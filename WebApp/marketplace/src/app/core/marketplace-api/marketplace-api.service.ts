import {Injectable} from '@angular/core';
import {Observable, of} from 'rxjs';
import {OfferModel} from './models/offer.model';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {map} from 'rxjs/operators'
import { CategoryModel } from './models/category.model ';

@Injectable({
  providedIn: 'root'
})
export class MarketplaceApiService {

    totalPages:number = 0

  constructor(
    private http:HttpClient
  ) { }

  getOffers(page: number, pageSize: number): Observable<OfferModel[]|null> {
    const params = new HttpParams()
    .set('index', page )
    .set('size',pageSize)
    return this.http.get<OfferModel[]>(`${environment.URL}/Offer`, {
      observe:'response',
      params:params
    })
    .pipe(
      map((response:HttpResponse<OfferModel[]>)=>{
        const headers = response.headers;
        this.totalPages = Math.ceil((headers.get('total-offers') as unknown as  number)/pageSize);
        return response.body;
      })
    )
  }

  postOffer(offer:OfferModel) {
    return this.http.post<OfferModel>(`${environment.URL}/Offer`,offer);
  }

  getCategories(): Observable<CategoryModel[]> {
    return this.http.get<CategoryModel[]>(`${environment.URL}/Category`);
  }
}
