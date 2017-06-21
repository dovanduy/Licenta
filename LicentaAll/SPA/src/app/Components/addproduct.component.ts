import { Component, ChangeDetectorRef } from '@angular/core';
import Interfaces = require("../Interfaces/Interfaces");

@Component({
  selector: 'add-product',
  templateUrl: 'app/Templates/addproduct.html'
})
export class ProductAddComponent {
  aditionalDetails: Interfaces.IAditionalDetail[] = [];
  detailNumber: number = 0;
  constructor(private _ref: ChangeDetectorRef) {
    
  }

  addAditionalDetail() {
    var aditionalDetail: Interfaces.IAditionalDetail = {
      description: "",
      name: "",
      id: this.detailNumber + 1,
      row_version : 0
  }
    this.aditionalDetails.push(aditionalDetail);
    this.detailNumber = this.detailNumber + 1;
    //this._ref.markForCheck();
  }

  deleteAditionalDetail(detail: Interfaces.IAditionalDetail) {
    var index = this.aditionalDetails.indexOf(detail);
    if (index > -1) {
      this.aditionalDetails.splice(index, 1);
    }
    //this._ref.markForCheck();
  }
}
