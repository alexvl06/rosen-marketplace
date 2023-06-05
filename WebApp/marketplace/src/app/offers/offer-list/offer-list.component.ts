import { Component, OnInit } from "@angular/core";
import { MarketplaceApiService } from "src/app/core/marketplace-api/marketplace-api.service";
import { OfferModel } from "src/app/core/marketplace-api/models/offer.model";

@Component({
  selector: "app-offer-list",
  templateUrl: "./offer-list.component.html",
  styleUrls: ["./offer-list.component.scss"],
})
export class OfferListComponent implements OnInit {
  pageSize: number = 6;
  totalPages!: number;
  offers: OfferModel[] = [];

  constructor(private marketplaceService: MarketplaceApiService) {}

  ngOnInit(): void {
    this.updatePage(1)
  }

  updatePage(index: number) {
    this.marketplaceService.getOffers(index, this.pageSize).subscribe({
      next: (offers) => {
        if (offers) this.offers = offers;
        this.totalPages = this.marketplaceService.totalPages;
      },
      error: (e) => console.error(`Error trying to get offers: ${e}`),
    });
  }
}
