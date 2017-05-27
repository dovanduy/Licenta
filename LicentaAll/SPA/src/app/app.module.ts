import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './Components/app.component';
import { AuthService } from './Services/AuthService';

@NgModule({
    imports: [
        BrowserModule/*,
        RouterModule.forRoot([
            { path: '/welcome', component: WelcomeComponent },
            { path: '**', redirectTo: '/welcome', pathMatch: 'full' }
        ])*/
    ],
  declarations: [ AppComponent ],
  providers:    [ AuthService ],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }
