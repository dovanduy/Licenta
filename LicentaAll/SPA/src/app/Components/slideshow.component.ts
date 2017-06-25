import { Component, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { SlideService} from "../Services/SlideService";
import Interfaces = require("../Interfaces/Interfaces");
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-slideshow',
  templateUrl: 'app/Templates/slideshow.html'
})
export class SlideshowComponent implements AfterViewInit {
  private currentSlideIndex:number = 0;
  slides: Interfaces.ISlide[] = [];

  constructor(private ref: ChangeDetectorRef, private _SlideService: SlideService, private _sanitizer: DomSanitizer) {
    this.slides = [
      //{
      //  imgSrc: 'http://i.huffpost.com/gen/3866236/images/o-HAPPY-facebook.jpg',
      //  state: 'Displayed'
      //},
      //{
      //  imgSrc: 'http://growcobusiness.com/wp-content/uploads/2013/06/Electronics.jpg',
      //  state: 'NotDisplayed'
      //},
      //{
      //  imgSrc: 'http://www.ncl.ac.uk/media/wwwnclacuk/undergraduate/images/courseprofiles/Countryside-Management-BSc-D455-crop.jpg',
      //  state: 'NotDisplayed'
      //},
      //{
      //  imgSrc: 'http://hhfinance.nl/wp-content/uploads/Geheimen-achter-succes-echte-ondernemers.jpg',
      //  state: 'Displayed'
      //},
      {
        imgSrc: 'https://www.best2wheelelectricscooter.com/wp-content/uploads/2016/10/about.jpg',
        state: 'Displayed'
      }
    ];
    setInterval(() => {
      this.nextSlide();
      this.ref.markForCheck;
    }, 3000);
  }

  ngAfterViewInit(): void {
  }

  public nextSlide() {
    if (this.hasSlides()) {
      if (this.currentSlideIndex + 1 < this.slides.length) {
        this.leave(this.slides[this.currentSlideIndex]);
        this.enter(this.slides[this.currentSlideIndex + 1]);
        this.currentSlideIndex = this.currentSlideIndex + 1;
      } else {
        this.leave(this.slides[this.currentSlideIndex]);
        this.enter(this.slides[0]);
        this.currentSlideIndex = 0;
      }
    }
  }

  public hasSlides() :boolean {
    return this.slides.length > 1;
  }

  public previousSlide() {
    if (this.currentSlideIndex - 1 > -1) {
      this.leave(this.slides[this.currentSlideIndex]);
      this.enter(this.slides[this.currentSlideIndex - 1]);
      this.currentSlideIndex--;
    } else {
      this.leave(this.slides[this.currentSlideIndex]);
      this.enter(this.slides[this.slides.length - 1]);
      this.currentSlideIndex = this.slides.length - 1;
    }
  }

  private enter(slide: Interfaces.ISlide) {
      slide.state = 'Displayed';
  }

  private leave(slide: Interfaces.ISlide) {
    slide.state = "NotDisplayed";
  }
}
