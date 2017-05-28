import { Component, AfterViewInit } from '@angular/core';
import { AuthService } from '../Services/AuthService'

@Component({
    selector: 'auth-zero-callback-component',
    templateUrl: 'app/Templates/auth0callback.html'
})
export class AuthZeroCallbackComponent implements AfterViewInit {

    constructor(private _AuthService: AuthService) {
    }

    ngAfterViewInit() {
        this._AuthService.handleAuthethication();
    }
}