import { Component, Input } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import {trigger, state, style, transition, animate } from "@angular/animations";

@
Component({
  selector: 'slide-component',
  templateUrl: 'app/Templates/slide.html',
  animations: [
    trigger('flyInOut', [
      state('Displayed', style({ width: '100%', backgroundImage: '*', opacity: 1 })),
      state('NotDisplayed', style({ width: '0%', backgroundImage: '*', opacity: 1 })),
      transition('Displayed => NotDisplayed',
        [
          style({ width: '100%', backgroundImage: '*', opacity: 1 }),
          animate(1000, style({ width: '0%', backgroundImage: '*', opacity: 1 }))
        ]),
      transition('NotDisplayed => Displayed', [
        style({ width: '0%', backgroundImage: '*', opacity: 0 }),
        animate(1000, style({ width: '100%', backgroundImage: '*', opacity: 1 }))
      ])
    ])
  ]
})
export class SlideComponent {
  constructor(private _sanitizer: DomSanitizer) {
  }
  @Input() public order: number;
  @Input() public state: string;
  @Input() public imgSrc: string;
}