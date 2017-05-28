import { NgModule } from '@angular/core';
import { RouterModule, Router, ɵROUTER_PROVIDERS } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './Components/app.component';
import { WelcomeComponent } from './Components/welcome.component';
import { AuthZeroCallbackComponent } from './Components/authZeroCallback.component';

import { AuthService } from './Services/AuthService';

@NgModule({
    imports: [
        BrowserModule,
        RouterModule.forRoot([
            { path: 'welcome', component: WelcomeComponent },
            { path: 'callback', component: AuthZeroCallbackComponent },
            { path: '**', redirectTo: 'welcome', pathMatch: 'full' }
        ])
    ],
    exports: [RouterModule],
    declarations: [AppComponent, WelcomeComponent, AuthZeroCallbackComponent],
    providers:    [ AuthService ],
    bootstrap:    [ AppComponent ]
})
export class AppModule { }
