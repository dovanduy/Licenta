import { NgModule } from '@angular/core';
import { RouterModule, Router, ɵROUTER_PROVIDERS } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";

import { AppComponent } from './Components/app.component';
import { NavbarComponent } from './Components/navbar.component';
import { WelcomeComponent } from './Components/welcome.component';
import { AuthZeroCallbackComponent } from './Components/authZeroCallback.component';
import { SlideshowComponent } from './Components/slideshow.component';
import { SlideComponent } from './Components/slide.component';
import { ProductsComponent } from './Components/products.component';
import { ProductCardComponent } from './Components/productcard.component';
import { ProductAddComponent } from './Components/addproduct.component';

import { AuthService } from './Services/AuthService';
import { SlideService } from './Services/SlideService';

@NgModule({
    imports: [
        BrowserAnimationsModule,
        BrowserModule,
        RouterModule.forRoot([
            { path: 'welcome', component: WelcomeComponent },
            { path: 'callback', component: AuthZeroCallbackComponent },
            { path: 'products', component: ProductsComponent },
            { path: 'products/add', component: ProductAddComponent },
            { path: '**', redirectTo: 'welcome', pathMatch: 'full' }
        ])
    ],
    exports: [RouterModule],
    declarations: [AppComponent, WelcomeComponent, AuthZeroCallbackComponent, NavbarComponent, SlideshowComponent, SlideComponent, ProductsComponent, ProductCardComponent, ProductAddComponent],
    providers:    [ AuthService, SlideService ],
    bootstrap:    [ AppComponent ]
})
export class AppModule { }
