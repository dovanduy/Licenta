import { Component, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../Services/AuthService'

@Component({
  selector: 'app-navbar',
  templateUrl: 'app/Templates/navbar.html'
})
export class NavbarComponent implements AfterViewInit {
  constructor(private _AuthService: AuthService, private _router: Router) {
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

  public ProductsClick() {
    this._router.navigate(['products']);
  }

  public AboutClick() {
    this._router.navigate(['about']);
  }

  public HomeClick() {
    this._router.navigate(['products/add']);
  }
}
