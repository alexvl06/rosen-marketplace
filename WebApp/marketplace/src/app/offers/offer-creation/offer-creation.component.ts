import {Component, Input, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, UntypedFormGroup, Validators} from '@angular/forms';
import { Router } from '@angular/router';
import { MarketplaceApiService } from 'src/app/core/marketplace-api/marketplace-api.service';
import {  OfferModel } from 'src/app/core/marketplace-api/models/offer.model';

@Component({
  selector: 'app-offer-creation',
  templateUrl: './offer-creation.component.html',
  styleUrls: ['./offer-creation.component.scss']
})
export class OfferCreationComponent implements OnInit{

  offerForm: FormGroup = this.formBuilder.group<OfferModel>({
    title: '',
    pictureUrl:'',
    description:'',
    location:'',
    categoryName: "I'm looking for"
  });

  @Input()
  categories: string[] = []


  constructor(
    private formBuilder:FormBuilder,
    private marketplaceService:MarketplaceApiService,
    private router:Router
  ) {

  }
  ngOnInit(): void {
    this.marketplaceService.getCategories().subscribe(categories=>{
      this.categories = categories.map(category=>category.name)
    })
  }

  offerSubmit() {
    const formValues:OfferModel = this.offerForm.getRawValue()
    formValues.username = sessionStorage.getItem('username')
    this.marketplaceService.postOffer(formValues).subscribe({
      next: ()=> {
        this.router.navigate(['/offer/list'])
      },
      error:(e)=>console.error(`Error trying to create new offer: ${e}`)

    })
  }
}
