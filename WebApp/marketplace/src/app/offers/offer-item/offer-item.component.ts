import {Component, Input} from '@angular/core';
import {OfferModel} from '../../core/marketplace-api/models/offer.model';

@Component({
  selector: 'app-offer-item',
  templateUrl: './offer-item.component.html',
  styleUrls: ['./offer-item.component.scss']
})
export class OfferItemComponent{

  @Input()
  offer!: OfferModel;

  constructor() { }


}
