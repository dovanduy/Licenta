import { Component } from '@angular/core';
import { ProductService } from '../Services/ProductService';
import Interfaces = require("../Interfaces/Interfaces");

@Component({
  selector: 'products',
  templateUrl: 'app/Templates/products.html'
})
export class ProductsComponent {
  SelectedCategoryName = 'Uncategorized';
  products: Interfaces.IProduct[];
  constructor(private _productService: ProductService) {
  }

  public clickToMock() {
    console.log("Products mocked");
    this._productService.refreshDataMock();
  }

  public productsInCategory(categoryId:number):Interfaces.IProduct[]{
    this.products = this._productService.getProductsForList();
    return this.products;
  }
}
