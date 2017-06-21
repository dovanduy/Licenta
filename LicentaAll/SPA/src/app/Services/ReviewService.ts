import { Injectable } from '@angular/core';
import Interfaces = require("../Interfaces/Interfaces");

@Injectable()
export class ReviewService {
  constructor() {
  }

  public getReviewsForProduct(productId:number): Interfaces.IReview[] {
    return [];
  }

  public addReviewToProduct(review:Interfaces.IReview,productId: number) {
    
  }

  public editReview(review: Interfaces.IReview) {
    
  }
}