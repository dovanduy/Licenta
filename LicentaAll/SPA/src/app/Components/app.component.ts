import { Component, AfterViewInit } from '@angular/core';
import { AuthService } from '../Services/AuthService'
import * as _ from 'lodash';

@Component({
  selector: 'my-app',
  templateUrl: 'app/Templates/app.html'
})
export class AppComponent implements AfterViewInit
{
    PageTitle = 'Angular';
    constructor(private _AuthService: AuthService) {
    }
    public IsAuthenticated() {
        return this._AuthService.isAuthenticated();
    }
    public GetProfileName() {
        if (this._AuthService.Profile && this._AuthService.Profile.nickname)
            return this._AuthService.Profile.nickname;
        else
            return "...";
    }
    public Login() {
        this._AuthService.login();
    }
    public Logout() {
        this._AuthService.logout();
    }

    ngAfterViewInit() {
        if (this._AuthService.isAuthenticated())
            this._AuthService.getUserInfo();
    }
}
