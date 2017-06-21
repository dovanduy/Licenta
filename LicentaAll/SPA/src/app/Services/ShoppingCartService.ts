import { Injectable } from '@angular/core';
import Interfaces = require("../Interfaces/Interfaces");
import { ProductService } from "./ProductService";

@Injectable()
export class ShoppingCartService {
  productsIdsInCart: Interfaces.IProductInCart[] = [];
  numberOfProductsInCart: number = 0;

  constructor(private _productService:ProductService) {
  }

  public getProductsInCart(): Interfaces.IProduct[] {
    let productIds: number[] = [];
    this.productsIdsInCart.forEach(x => productIds.push(x.id));

    let products: Interfaces.IProduct[] = this._productService.getProductByIds(productIds);

    products.forEach(product => {
      var quantityInformation = this.productsIdsInCart.filter(x => x.id === product.id);
      product.quantity = quantityInformation[0].quantity;
      product.total = product.price * product.quantity;
    });
    return products;
  }

  public setProductsInCartFromMemory() {
    let items = localStorage.getItem('shopping_cart_items');
    if (items) {
      this.productsIdsInCart = JSON.parse(items);
      this.numberOfProductsInCart = this.productsIdsInCart.length;
      console.log("Shopping cart restored");
    }
    else
      console.log("No prior shopping cart detected.");
  }

  public refreshDataWithMocks() {
    this.productsIdsInCart = [{ id: 1, quantity: 2 }, { id: 2, quantity: 1 }, , { id: 3, quantity: 10 }, , { id: 4, quantity: 5 }];
    this.numberOfProductsInCart = 4;
    this._productService.refreshDataMock();
  }

  public saveProductsInCartInMemory() {
    const products = JSON.stringify(this.productsIdsInCart);
    localStorage.setItem('shopping_cart_items', products);
    console.log("Saved cart items.");
  }

  public setProductsInCartFromServer() {
    //TODO
    this.numberOfProductsInCart = this.productsIdsInCart.length;
    console.log("Shopping cart restored from server.");
  }

  public saveProductsInCartToServer() {
    //TODO
    console.log("Saved cart items to server.");
  }

  public addProductInCart(productId: number) {
    let productAlreadyInCart = this.productsIdsInCart.find(x => x.id === productId);
    if (productAlreadyInCart) {
      this.productsIdsInCart[this.productsIdsInCart.indexOf(productAlreadyInCart)].quantity += 1;
    } else {
      this.productsIdsInCart.push({ id: productId, quantity: 1 });
      this.numberOfProductsInCart = this.numberOfProductsInCart + 1;
    }
  }

  public removeProductFromCart(productId: number) {
    let productAlreadyInCart = this.productsIdsInCart.find(x => x.id === productId);
    if (productAlreadyInCart) {
      let index = this.productsIdsInCart.indexOf(productAlreadyInCart);
      let quantity = this.productsIdsInCart[this.productsIdsInCart.indexOf(productAlreadyInCart)].quantity;

      if (quantity > 1)
        this.productsIdsInCart[index].quantity = quantity - 1;
      else
        this.productsIdsInCart.splice(index, 1);
    }
    else
      console.log("Tried to delete an inexisting product from cart!");
  }
}
