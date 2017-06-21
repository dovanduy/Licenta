import { Injectable } from '@angular/core';
import Interfaces = require("../Interfaces/Interfaces");

@Injectable()
export class SlideService {
  slides: Interfaces.ISlide[];

  public getSlides(): Interfaces.ISlide[] {
    return this.slides;
  }

  public setSlides(slides: Interfaces.ISlide[]) {
    this.slides = slides;
  }

  public setSlidesUsingImgSrc(imgSrcs: string[]) {
    this.slides = [];
    for (var src in imgSrcs) {
      var slide: Interfaces.ISlide = {
        imgSrc: src,
        state: 'void'
      };
      this.slides.push(slide);
    }
  }
}
