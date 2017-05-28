﻿/// <reference path="../../../node_modules/@types/auth0-js/index.d.ts" />
import { Injectable, } from '@angular/core';
import { Router } from '@angular/router';
import 'rxjs/add/operator/filter';
import * as auth0 from 'auth0-js';

@Injectable()
export class AuthService {
    auth0 = new auth0.WebAuth({
        clientID: 'GDy2G4JZ8jEXQgW7s2gR0dNNXLsohPEo',
        domain: 'stefanmocanu.eu.auth0.com',
        responseType: 'token id_token',
        audience: 'https://stefanmocanu.eu.auth0.com/userinfo',
        redirectUri: 'http://localhost:3000/callback',
        scope: 'openid profile',
        leeway: 30
    });
    public Profile: auth0.Auth0UserProfile;


    constructor(private router: Router) {
    }

    public login(): void {
        this.auth0.authorize({});
    }

    public handleAuthethication() {
        this.auth0.parseHash({
            hash: window.location.hash
        }, this.authResultCallback.bind(this));
    }

    private authResultCallback(error: any, authResult: any): void {
        if (authResult && authResult.accessToken && authResult.idToken) {
            window.location.hash = '';
            this.setSession(authResult);
            this.getUserInfo();
            this.router.navigate(['welcome']);
        } else if (authResult && authResult.error) {
            alert(`Error: ${authResult.error}`);
        }
    }

    public getUserInfo() {
        const accessToken = localStorage.getItem('access_token');
        if (!accessToken) {
            throw new Error('Access token must exist to fetch profile');
        }
        this.auth0.client.userInfo(accessToken, this.userInfoCallback.bind(this));
    }

    private userInfoCallback(error: any, userProfile: auth0.Auth0UserProfile): void {
        if (userProfile) {
            this.Profile = userProfile;
        }
        else {
            alert(`Error getting user profile: ${error}`);
        }
    }

    private setSession(authResult: any): void {
        // Set the time that the access token will expire at
        const expiresAt = JSON.stringify((authResult.expiresIn * 1000) + new Date().getTime());
        localStorage.setItem('access_token', authResult.accessToken);
        localStorage.setItem('id_token', authResult.idToken);
        localStorage.setItem('expires_at', expiresAt);
    }

    public logout(): void {
        // Remove tokens and expiry time from localStorage
        localStorage.removeItem('access_token');
        localStorage.removeItem('id_token');
        localStorage.removeItem('expires_at');
        // Go back to the home route
        this.router.navigate(['welcome']);
    }

    public isAuthenticated(): boolean {
        // Check whether the current time is past the
        // access token's expiry time
        const expiresAt = JSON.parse(localStorage.getItem('expires_at'));
        return new Date().getTime() < expiresAt;
    }

}