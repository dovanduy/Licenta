import { Injectable } from '@angular/core';
import Interfaces = require("../Interfaces/Interfaces");
import { ReviewService } from "./ReviewService";

@Injectable()
export class ProductService {
  products: Interfaces.IProduct[];
  constructor(private _reviewService:ReviewService) {
  }

  public getProductsForList(): Interfaces.IProduct[] {
    return this.products;
  }

  public refreshData() {
  }

  public refreshDataMock() {
    this.products = [];
    this.products.push({
      name: "Canon",
      id: 1,
      aditionalDetails: [],
      imageSrc: "https://www.bhphotovideo.com/images/images2500x2500/nikon_1560_d500_dslr_camera_with_1214162.jpg",
      inStock: true,
      price: 1500,
      description: "No boom",
      row_version: 0,
      rating: 5,
      cateogoryId: 0
    }); 
    this.products.push({
      name: "Cannon",
      id: 6,
      aditionalDetails: [],
      imageSrc: "http://vignette3.wikia.nocookie.net/dragonball/images/d/dc/Cannon-625x415.jpg/revision/latest?cb=20150430182251",
      inStock: true,
      price: 6000,
      description: "Boom",
      row_version: 0,
      rating: 5,
      cateogoryId: 0
    });
    this.products.push({
      name: "Headphones",
      id: 2,
      aditionalDetails: [],
      imageSrc: "https://cdn.shopify.com/s/files/1/0362/2465/products/pm-3_01_400x.jpg?v=1484848825",
      inStock: true,
      price: 499.99,
      description: "The best headphones",
      row_version: 0,
      rating: 7,
      cateogoryId: 0
    });
    this.products.push({
      name: "Table",
      id: 3,
      aditionalDetails: [],
      imageSrc: "http://www.ikea.com/PIAimages/0256310_PE400186_S5.JPG",
      inStock: true,
      price: 567.7,
      description: "For tabling",
      row_version: 0,
      rating: 2,
      cateogoryId: 0
    });
    this.products.push({
      name: "Watergun",
      id: 4,
      aditionalDetails: [],
      imageSrc: "https://images-na.ssl-images-amazon.com/images/I/61IgIqKfWTL._SX355_.jpg",
      inStock: true,
      price: 14.9,
      description: "pew pew",
      row_version: 0,
      rating: 6,
      cateogoryId: 0
    });
    this.products.push({
      name: "Hoe",
      id: 5,
      aditionalDetails: [],
      imageSrc: "https://vignette1.wikia.nocookie.net/r2da/images/7/7c/Hoe.jpg/revision/latest?cb=20170304010832",
      inStock: false,
      price: 10.4,
      description: "awesome hoe",
      row_version: 0,
      rating: 9,
      cateogoryId: 0
    });
    this.products.push({
      name: "Lightbulb",
      id: 7,
      aditionalDetails: [],
      imageSrc: "https://target.scene7.com/is/image/Target/13750011_Alt01?wid=520&hei=520&fmt=pjpeg",
      inStock: false,
      price: 4.99,
      description: "an idea",
      row_version: 0,
      rating: 9,
      cateogoryId: 0
    }); this.products.push({
      name: "Pillow",
      id: 8,
      aditionalDetails: [],
      imageSrc: "https://www.tuftandneedle.com/images/pillow/slides/pillow_slide_1-d2324e61.jpg",
      inStock: false,
      price: 149.99,
      description: "comfy, sleepy",
      row_version: 0,
      rating: 9,
      cateogoryId: 0
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