import { Injectable } from '@angular/core';
import Interfaces = require("../Interfaces/Interfaces");
import { ReviewService } from "./ReviewService";

@Injectable()
export class ProductService {
  products: Interfaces.IProduct[];
  constructor(private _reviewService:ReviewService) {
  }

  public getProductsForList(): Interfaces.IProduct[] {
    return [];
  }

  public refreshData() {
  }

  public refreshDataMock() {
    this.products = [];
    this.products.push({
      name: "product 1",
      id: 1,
      aditionalDetails: [],
      imageSrc: "https://www.bhphotovideo.com/images/images2500x2500/nikon_1560_d500_dslr_camera_with_1214162.jpg",
      inStock: true,
      price: 3.2,
      description: "some product",
      row_version: 0,
      rating: 5
    });
    this.products.push({
      name: "product 2",
      id: 2,
      aditionalDetails: [],
      imageSrc: "https://cdn.shopify.com/s/files/1/0362/2465/products/pm-3_01_400x.jpg?v=1484848825",
      inStock: true,
      price: 4.99,
      description: "nice product",
      row_version: 0,
      rating: 7
    });
    this.products.push({
      name: "product 3",
      id: 3,
      aditionalDetails: [],
      imageSrc: "http://www.ikea.com/PIAimages/0256310_PE400186_S5.JPG",
      inStock: true,
      price: 6.7,
      description: "bad product",
      row_version: 0,
      rating: 2
    });
    this.products.push({
      name: "product 4",
      id: 4,
      aditionalDetails: [],
      imageSrc: "http://www.homedepot.com/hdus/en_US/DTCCOMNEW/fetch/Category_Pages/Bath/Toilet_Seats_and_Bidets/shop-by-rough-size-1.jpg",
      inStock: true,
      price: 5.7,
      description: "ok product",
      row_version: 0,
      rating: 6
    });
    this.products.push({
      name: "product 5",
      id: 5,
      aditionalDetails: [],
      imageSrc: "https://vignette1.wikia.nocookie.net/r2da/images/7/7c/Hoe.jpg/revision/latest?cb=20170304010832",
      inStock: false,
      price: 10.4,
      description: "awesome product",
      row_version: 0,
      rating: 9
  });
  }

  public getProductById(id: number): Interfaces.IProduct {
    return null;
  }

  public getProductByIds(ids: number[]): Interfaces.IProduct[] {
    return this.products.filter(x => ids.some(y => y === x.id));
  }

  public addProductToServer() {
  }
}