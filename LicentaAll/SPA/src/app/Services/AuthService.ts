/// <reference path="../../../node_modules/@types/auth0-js/index.d.ts" />
import { Injectable, } from '@angular/core';
import { Router } from '@angular/router';
import 'rxjs/add/operator/filter';
import auth0 = require('auth0-js');

@Injectable()
export class AuthService {
  expiresAt:any = 0;

  auth0 = new auth0.WebAuth({
    clientID: 'GDy2G4JZ8jEXQgW7s2gR0dNNXLsohPEo',
    domain: 'stefanmocanu.eu.auth0.com',
    responseType: 'token id_token',
    audience: 'https://stefanmocanu.eu.auth0.com/userinfo',
    redirectUri: 'http://localhost:3000/callback',
    scope: 'openid%20profile%20email&',
    leeway: 30
  });
  public Profile: auth0.Auth0UserProfile;


  constructor(private router: Router) {
  }

  public login(): void {
    var arg: auth0.AuthorizeOptions = {
      responseType: 'id_token token',
      redirectUri: 'http://localhost:3000/callback'
    };
    this.auth0.authorize(arg);
  }

  public handleAuthethication() {
    console.log("Handling authethication...");
    this.auth0.parseHash({
        hash: window.location.hash
      },
      this.authResultCallback.bind(this));
  }

  private authResultCallback(error: any, authResult: any): void {
    console.log(authResult && authResult.accessToken && authResult.idToken);
    console.log(authResult);
    console.log(authResult.accessToken);
    console.log(authResult.idToken);
    if (authResult && authResult.accessToken && authResult.idToken) {
      console.log("Auth result callback...");
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
    console.log('Getting user info...');
    this.auth0.client.userInfo(accessToken, this.userInfoCallback.bind(this));
  }

  private userInfoCallback(error: any, userProfile: auth0.Auth0UserProfile): void {
    if (userProfile) {
      console.log("User profile:");
      console.log(userProfile);
      this.Profile = userProfile;
    } else {
      console.log(`Error getting user profile: ${error}`);
    }
  }

  private setSession(authResult: any): void {
    // Set the time that the access token will expire at
    console.log("Setting session...");
    const expiresAt = JSON.stringify((authResult.expiresIn * 1000) + new Date().getTime());
    localStorage.setItem('access_token', authResult.accessToken);
    localStorage.setItem('id_token', authResult.idToken);
    localStorage.setItem('expires_at', expiresAt);
    this.expiresAt = JSON.parse(expiresAt);
    console.log("Session set.");
  }

  public logout(): void {
    // Remove tokens and expiry time from localStorage
    localStorage.removeItem('access_token');
    localStorage.removeItem('id_token');
    localStorage.removeItem('expires_at');
    // Go back to the home route
    this.expiresAt = 0;
    this.router.navigate(['welcome']);
  }

  public isAuthenticated(): boolean {
    // Check whether the current time is past the
    // access token's expiry time

    if (this.expiresAt === 0) {
      if (localStorage.getItem('expires_at') === null)
        return false;
      else
        this.expiresAt = JSON.parse(localStorage.getItem('expires_at'));
    }
      
    //console.log("Token expires at: " + expiresAt + " But Date().getTime() is " + new Date().getTime());
    //console.log(expiresAt);
    if (new Date().getTime() < this.expiresAt)
      return true;
    else {
      this.logout();
      return false;
    }
  }

}