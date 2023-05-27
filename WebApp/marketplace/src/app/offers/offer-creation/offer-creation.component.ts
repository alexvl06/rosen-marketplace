import {Component, Input} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, UntypedFormGroup, Validators} from '@angular/forms';
import { Router } from '@angular/router';
import { MarketplaceApiService } from 'src/app/core/marketplace-api/marketplace-api.service';
import { Category, OfferModel } from 'src/app/core/marketplace-api/models/offer.model';

@Component({
  selector: 'app-offer-creation',
  templateUrl: './offer-creation.component.html',
  styleUrls: ['./offer-creation.component.scss']
})
export class OfferCreationComponent{

  offerForm: FormGroup = this.formBuilder.group<OfferModel>({
    title: '',
    imgURL:'',
    description:'',
    location:'',
    category: "I'm looking for"
  });

  @Input()
  categories: string[] = ["I'm looking for", "Product", "Service"];


  constructor(
    private formBuilder:FormBuilder,
    private marketplaceService:MarketplaceApiService,
    private router:Router
  ) {

  }

  offerSubmit() {
    const formValues:OfferModel = this.offerForm.getRawValue()
    this.marketplaceService.postOffer(formValues).subscribe({
      next: ()=> {
        formValues.date = new Date()
        formValues.username = sessionStorage.getItem('username')
        this.router.navigate(['/offer/list'])
      }
      //
    })
  }
}
