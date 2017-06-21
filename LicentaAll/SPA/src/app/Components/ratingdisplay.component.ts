import { Component, Input } from '@angular/core';

@Component({
  selector: 'ratingdisplay',
  templateUrl: 'app/Templates/ratingdisplay.html'
})
export class RatingDisplay {
  @Input() Rating: number;
}
