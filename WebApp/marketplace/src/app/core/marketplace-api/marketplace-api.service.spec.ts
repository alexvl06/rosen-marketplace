import { TestBed } from "@angular/core/testing";
import {
  HttpClientTestingModule,
  HttpTestingController,
} from "@angular/common/http/testing";

import { MarketplaceApiService } from "./marketplace-api.service";
import { OfferModel } from "./models/offer.model";
import { generateManyOffers, generateOneOffer } from "./models/offer.mock";
import { environment } from "src/environments/environment";

fdescribe("MarketplaceApiService", () => {
  let service: MarketplaceApiService;
  let httpController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [MarketplaceApiService],
    });
    service = TestBed.inject(MarketplaceApiService);
    httpController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpController.verify();
  });

  it("should be created", () => {
    expect(service).toBeTruthy();
  });

  describe("tests for get the 6 first offers", () => {
    it("should return a offer list", (doneFn) => {
      const pageSize = 6;
      const index = 1;
      const mockData: OfferModel[] = generateManyOffers(pageSize);
      service.getOffers(index, pageSize).subscribe((data) => {
        expect(data!.length).toEqual(pageSize);
        expect(req.request.method).toBe("GET");
        doneFn();
      });
      const url = `${environment.URL}/Offer?index=${index}&size=${pageSize}`;
      const req = httpController.expectOne(url);
      req.flush(mockData);
      const params = req.request.params;
      expect(params.get("index")).toEqual(`${index}`);
      expect(params.get("size")).toEqual(`${pageSize}`);
    });
  });
  describe("Test for create", () => {
    it("should return a new offer", (doneFn) => {
      const mockData = generateOneOffer()
      const newOffer: OfferModel = {
        title: "New Offer",
        description: "bla bla bla",
        pictureUrl: "https/img.org",
        categoryName: "Service",
        username: "Joe Doe",
        location: "Miami",
      };

      service.postOffer(newOffer).subscribe((data) => {
        expect(data).toEqual(mockData);
        doneFn();
      });

      const URL = `${environment.URL}/Offer`
      const req = httpController.expectOne(URL)
      req.flush(mockData)
      expect(req.request.body).toEqual(newOffer)
      expect(req.request.method).toEqual('POST')
    });
  });
});
