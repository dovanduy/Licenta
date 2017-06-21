import { Component, ChangeDetectorRef } from '@angular/core';
import Interfaces = require("../Interfaces/Interfaces");
import { ShoppingCartService } from '../Services/ShoppingCartService';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'shopping-cart',
  templateUrl: 'app/Templates/shoppingcart.html'
})
export class ShoppingCartComponent {
  products:Interfaces.IProduct[];
  constructor(private _shoppingCartService: ShoppingCartService, private _sanitizer: DomSanitizer) {
  }

  public clickToMock() {
    this._shoppingCartService.refreshDataWithMocks();
  }

  public cartHasProducts(): boolean {
    return this._shoppingCartService.numberOfProductsInCart > 0;
  }

  public productsInCart(): Interfaces.IProduct[] {
    this.products = this._shoppingCartService.getProductsInCart();
    return this.products;
  }

  public calculateTotalPrice(): number {
    let sum:number = 0;
    this.products.forEach(x => sum = sum + x.total);
    return sum;
  }
}
