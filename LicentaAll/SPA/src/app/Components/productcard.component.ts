import { Component, Input } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'product-card',
  templateUrl: 'app/Templates/productCard.html'
})
export class ProductCardComponent {
  @Input() Name: string;
  @Input() Description: string;
  @Input() Price: number;
  @Input() Image: string;
  @Input() InStock: boolean;
  @Input() Rating: number;

  constructor(private _sanitizer : DomSanitizer) {
    
  }

  public trust(url:string) {
    return this._sanitizer.bypassSecurityTrustStyle('url(' + url + ')');
  }
}
