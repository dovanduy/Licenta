import { Component } from '@angular/core';
import { AuthService } from '../Services/AuthService'

@Component({
  selector: 'my-app',
  templateUrl: 'app/Templates/app.html',
  providers: [AuthService]
})
export class AppComponent
{
    PageTitle = 'Angular';
}
